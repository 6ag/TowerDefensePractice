using AssemblyCSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradedCommonTurret : CommonTurret {
    public UpgradedCommonTurret()
    {
    }
    public override void Start()
    {
        base.Start();
        type = BulletEnum.Type.ImprovedRegularTurret;
        attackRateTime = 0.5f;
        damageRate = 40;
    }
    public override void Attack()
    {
        base.Attack();
    }
}
