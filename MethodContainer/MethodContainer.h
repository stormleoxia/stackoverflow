// MethodContainer.h

#pragma once

using namespace System;

namespace MethodContainer {

	static int __stdcall SomeFunction(void* someObject, void*  someParam)
	{
		CSomeClass* o = (CSomeClass*)someObject;
		return o->MemberFunction(someParam);
	}

	public ref class Container
	{
	
	public:
		static IntPtr GetCppFunc()
		{			
			return System::IntPtr(SomeFunction);
		}
		static IntPtr GetInstance()
		{
			CSomeClass* o = new CSomeClass();
			return System::IntPtr(o);
		}
		static IntPtr GetParameter()
		{
			void* p = 0;
			return System::IntPtr(p);
		}
	};
}
