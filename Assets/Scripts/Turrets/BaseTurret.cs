using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public abstract class BaseTurret : MonoBehaviour
	{
		public Transform head, firePosition;
        public State state;
        public List<GameObject> enemies;
        public float attackRateTime;
        public float timer;
        public BulletEnum.Type type;

        public virtual void StopAttack()
        {
            
        }

        public GameObject bulletPrefab;
        public float damageRate;
        public BaseTurret(float attackRateTime,float damageRate )
		{
            state = new Idle(this);
            enemies = new List<GameObject>();
            timer = 0;
            this.attackRateTime = attackRateTime;
            this.damageRate = damageRate;
		}

        public BaseTurret()
        {
        }

        public void MoveHead()
		{
            if(enemies[0].activeInHierarchy==true)
            {
                Vector3 targetPosition = enemies[0].transform.position;
                targetPosition.y = head.position.y;
                head.LookAt(targetPosition);
            }
        }
		public virtual Boolean CheckForEnemies()
		{
			UpdateEnemys ();
			if (enemies.Count > 0 && enemies[0]!=null) {
                return true;
			}
            return false;
		}
		void Update()
		{
            timer += Time.deltaTime;
            state.Act();
//            Debug.Log(state.GetType().ToString());

        }
		public virtual void Attack()
		{
        //    Debug.Log("Girando cabeca");
          
            try
            {
                MoveHead();
                if (timer >= attackRateTime)
                {

                    // GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
                    GameObject bullet  = BulletPrototype.getProjectile(type, firePosition.position, firePosition.rotation);
                    bullet.GetComponent<Bullet>().SetTarget(enemies[0].transform);
                    //TODO: Inserir campo dano na criação de balas. (USE PROTOTYPE!)
                    //TODO: Use flyweight para um pool de inimigos!
                    //TODO: Use State para controlar idle, atacando, upgrade
                    timer = 0;
                }

            }
            
                catch(MissingReferenceException e)
                {
                    UpdateEnemys();
                }
                catch(ArgumentOutOfRangeException e)
                {
                    state.ChangeState();
                }
			}
         
		
	public virtual void Start()
	{
  
		timer = attackRateTime;
       state = new Idle(this);
       enemies = new List<GameObject>();

        }
	void OnTriggerEnter(Collider other)
	{
            Debug.Log("here");
		if (other.tag == "Enemy")
		{
			enemies.Add(other.gameObject);
		}
	}
	void OnTriggerExit(Collider other)
	{
            if (other.tag == "Enemy")
		{
			enemies.Remove(other.gameObject);
		}
	}
	public void UpdateEnemys()
	{
		List<int> emptyIndexList = new List<int>();
		for (int i = 0; i < enemies.Count; i++)
		{
			if ((enemies[i] == null) || (enemies[i].activeInHierarchy==false))
			{
				emptyIndexList.Add(i);
			}
		}
		for (int i = 0; i < emptyIndexList.Count; i++)
		{
			enemies.RemoveAt(emptyIndexList[i] - i);
		}
	}

	}}
