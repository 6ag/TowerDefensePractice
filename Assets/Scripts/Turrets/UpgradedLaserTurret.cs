using AssemblyCSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradedLaserTurret : LaserTurret
{
    public UpgradedLaserTurret()
    {
    }
    public override void Start()
    {
        base.Start();
        damageRate = 90;
    }
    



}
