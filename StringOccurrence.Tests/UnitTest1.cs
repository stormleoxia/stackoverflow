namespace StringOccurrence.Tests
{
    public class StringOccurrenceTests
    {
        [Theory]
        [InlineData("abcd", "ab", 1)]
        [InlineData("abcd", "cd", 1)]
        [InlineData("abcd", "bc", 1)]
        [InlineData("The way to count things", "the", 0)]
        [InlineData("The way to count things", "th", 1)]
        [InlineData("The way to count things", "t", 3)]
        public void CheckRichard(string input, string needle, int result)
        {
            var instance = new RichardWatson();
            Assert.Equal(result, instance.GetOccurrences(input, needle));
        }

        [Theory]
        [InlineData("abcd", "ab", 1)]
        [InlineData("abcd", "cd", 1)]
        [InlineData("abcd", "bc", 1)]
        [InlineData("The way to count things", "the", 0)]
        [InlineData("The way to count things", "th", 1)]
        [InlineData("The way to count things", "t", 3)]
        public void CheckLuke(string input, string needle, int result)
        {
            var instance = new LukeH();
            Assert.Equal(result, instance.GetOccurrences(input, needle));
        }


        [Theory]
        [InlineData("abcd", "ab", 1)]
        [InlineData("abcd", "cd", 1)]
        [InlineData("abcd", "bc", 1)]
        [InlineData("The way to count things", "the", 0)]
        [InlineData("The way to count things", "th", 1)]
        [InlineData("The way to count things", "t", 3)]
        [InlineData("", "", 0)]
        [InlineData("The way", "", 0)]
        [InlineData("Th", "The", 0)]
        [InlineData("Th", "h", 1)]
        [InlineData("Th", "Th", 1)]
        public void CheckFab(string input, string needle, int result)
        {
            var instance = new Fab();
            Assert.Equal(result, instance.GetOccurrences(input, needle));
        }

    }
}