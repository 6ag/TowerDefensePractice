using System;

namespace AssemblyCSharp
{
	public class Idle : State
	{
        BaseTurret b;

        public Idle(BaseTurret b)
        {
            this.b = b;
        }

        public void Act()
        {
            if (b.CheckForEnemies())
            {
                ChangeState();
            }
        }

        public void ChangeState()
        {
            b.state = new Active(b);
        }
    }
}

