using AssemblyCSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTurret : BaseTurret {
    public MissileTurret()
    {
    }
    public override void Start()
    {
        base.Start();
        type = BulletEnum.Type.MissileTurret;
        attackRateTime = 1;
        damageRate = 80;
    }
    public override void Attack()
    {
        base.Attack();
    }
}
