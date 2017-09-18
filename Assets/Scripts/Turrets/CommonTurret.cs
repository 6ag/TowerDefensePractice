using System;

namespace AssemblyCSharp
{
    public class CommonTurret : BaseTurret
    {
        public CommonTurret()
        {   
        }

        public override void Start()
        {
            base.Start();
            type = BulletEnum.Type.RegularTurret;
            attackRateTime = 0.7f;
            damageRate = 40;    
        }
        public override void Attack()
        {
            base.Attack();  
        }
    }
}

