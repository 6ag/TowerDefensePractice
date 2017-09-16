using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPrototype{


    private GameObject bullet;
    private GameObject missile;
    private static Dictionary<BulletEnum.Type, GameObject> hash;

    public static GameObject getProjectile(BulletEnum.Type type, Vector3 position, Quaternion rotation)
    {
        GameObject clone =  GameObject.Instantiate(hash[type],position,rotation);
        clone.SetActive(true);
        return clone;
    }

    public BulletPrototype(GameObject bullet, GameObject missile)
    {
       this.bullet = bullet;
       this.missile = missile;
        GameObject regularBullet = GameObject.Instantiate(bullet);  
        regularBullet.GetComponent<Bullet>().damage = 40;
        GameObject improvedBullet= GameObject.Instantiate(bullet);
        improvedBullet.GetComponent<Bullet>().damage = 40;
        GameObject regularMissile = GameObject.Instantiate(missile);
        regularMissile.GetComponent<Bullet>().damage = 80;
        GameObject improvedMissile = GameObject.Instantiate(missile);
        improvedMissile.GetComponent<Bullet>().damage = 80;
        hash = new Dictionary<BulletEnum.Type, GameObject>(); 
        hash.Add(BulletEnum.Type.RegularTurret, regularBullet);
        hash.Add(BulletEnum.Type.ImprovedRegularTurret, improvedBullet);
        hash.Add(BulletEnum.Type.MissileTurret, regularMissile);
        hash.Add(BulletEnum.Type.ImprovedmissileTurret, improvedMissile);
    }

   
}
