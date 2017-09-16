using System;

namespace AssemblyCSharp
{
	public interface State
	{
		void Act();
        void ChangeState();
	}
}

