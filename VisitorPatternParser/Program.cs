using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace VisitorPatternParser
{
    public interface IVisitor
    {
        void Visit(MethodToken token);
        void Visit(OperatorToken token);
        void Visit(NameToken token);
    }

    // We define a base class
    // 1. abstract (cannot be instantiated and we can enforce implementation of methods like the Accept()
    // 2. don't store any data not useful (like Name) 
    public abstract class BaseToken : IVisitable
    {
        public string Value { get; set; }

        public List<BaseToken> Children { get; } = new List<BaseToken>();
        public abstract void Accept(IVisitor visitor);
    }

    public class NameToken : BaseToken
    {
        public string Name { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class OperatorToken : BaseToken
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }

    public class MethodToken : BaseToken
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
// Load text in memory
            var text = File.ReadAllText("input.json");
// Get Token instance
            var jsonToken = JObject.Parse(text);
// Get the strong typed tree of token
            var token = CreateToken(jsonToken);
// Create the visitor
            var tokenVisitor = new TokenVisitor();
// Visit the tree with visitor
            token.Accept(tokenVisitor);
// Output the result
            Console.WriteLine(tokenVisitor.Output);
        }

        private static BaseToken CreateToken(JToken jsonToken)
        {
            var typeOfToken = jsonToken["Name"];
            if (typeOfToken == null || typeOfToken.Type != JTokenType.String)
            {
                return null;
            }

            BaseToken result;
            switch (typeOfToken.ToString())
            {
                case "Method":
                {
                    result = jsonToken.ToObject<MethodToken>();
                    break;
                }
                case "Operator":
                {
                    result = jsonToken.ToObject<OperatorToken>();
                    break;
                }
                default:
                {
                    result = jsonToken.ToObject<NameToken>();
                    break;
                }
            }

            var jChildrenToken = jsonToken["Childs"];
            if (result != null &&
                jChildrenToken != null &&
                jChildrenToken.Type == JTokenType.Array)
            {
                var children = jChildrenToken.AsJEnumerable();
                foreach (var child in children)
                {
                    var childToken = CreateToken(child);
                    if (childToken != null)
                    {
                        result.Children.Add(childToken);
                    }
                }
            }

            return result;
        }
    }

    internal class TokenVisitor : IVisitor
    {
        private readonly StringBuilder _builder = new StringBuilder();

        // invert the order of children first
        private int firstIndex = 1;
        private int secondIndex = 0;

        // Keep track of name tokens
        private readonly HashSet<BaseToken> _visitedTokens = new HashSet<BaseToken>();

        public string Output => _builder.ToString();

        public void Visit(MethodToken token)
        {
            // Store local to avoid recursive call;
            var localFirst = firstIndex;
            var localSecond = secondIndex;
            // back to normal order of children
            firstIndex = 0;
            secondIndex = 1;
            RenderChild(token.Children, localFirst);
            _builder.Append(token.Value);
            RenderChild(token.Children, localSecond);
        }

        private void RenderChild(List<BaseToken> children, int index)
        {
            if (children.Count > index)
            {
                _builder.Append("(");
                children[index].Accept(this);
                _builder.Append(")");
            }
        }

        public void Visit(OperatorToken token)
        {
            if (token.Children.Count > 0)
            {
                token.Children[0].Accept(this);
                _builder.Append(" ");
            }

            _builder.Append(token.Value);
            if (token.Children.Count > 0)
            {
                _builder.Append(" ");
                token.Children[0].Accept(this);
            }
        }

        public void Visit(NameToken token)
        {
            if (_visitedTokens.Contains(token))
            {
                _builder.Append(token.Value);
            }
            else
            {
                _visitedTokens.Add(token);
                _builder.Append(token.Name);
            }
        }
    }
}
