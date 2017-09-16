using System;
using UnityEngine;

namespace AssemblyCSharp
{
    class Active : State
    {
        BaseTurret b;
        public Active(BaseTurret b)
        {
            this.b = b;
        }

        public void Act()
        {
            b.Attack();
            if (b.CheckForEnemies() == false)
            {
                b.StopAttack();
                ChangeState();
            }
        }

        public void ChangeState()
        {
            b.state = new Idle(b);
        }
    }
}
