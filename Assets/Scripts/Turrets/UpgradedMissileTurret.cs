using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradedMissileTurret : MissileTurret {
    public UpgradedMissileTurret()
    {
    }

    public override void Start()
    {
        base.Start();
        type = BulletEnum.Type.ImprovedmissileTurret;
        attackRateTime = 0.8f;
        damageRate = 80;
    }
}
