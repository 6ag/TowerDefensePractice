using System;
using UnityEngine;

namespace AssemblyCSharp
{
    public class LaserTurret : BaseTurret
    {

        public LineRenderer laserRenderer;
        public GameObject laserEffect;
        public override void Start()
        {
            base.Start();
            damageRate = 70;
            attackRateTime = 1;
        }
        public override void StopAttack()
        {
            laserRenderer.enabled = false;
            laserEffect.SetActive(false);
        }

        public override void Attack()
        {
            try
            {
                MoveHead(); 
                if (laserRenderer.enabled == false)
                {
                    laserRenderer.enabled = true;
                    laserEffect.SetActive(true);
                }
                laserRenderer.SetPositions(new Vector3[] { firePosition.position, enemies[0].transform.position });
                laserEffect.transform.position = enemies[0].transform.position;
                laserEffect.transform.LookAt(new Vector3(transform.position.x, enemies[0].transform.position.y, transform.position.z));
                enemies[0].GetComponent<Enemy>().TakeDamage(damageRate * Time.deltaTime);
            }
            catch (MissingReferenceException e)
            {
               // Debug.Log(state.GetType());
                StopAttack();
                UpdateEnemys();
            }
              catch ( ArgumentOutOfRangeException e)
            {
          //      Debug.Log(state.GetType());
                StopAttack();
                UpdateEnemys();
            }
            }
    }
}

   

	

