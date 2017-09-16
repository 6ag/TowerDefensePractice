using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyweightEnemies {
    public static List<GameObject> free,busy;

    private GameObject prefab;
    public static Vector3 position;
    public static Quaternion identity;

    public FlyweightEnemies(GameObject prefab, Vector3 _position, Quaternion _identity){
        this.prefab = prefab;
        position = _position;
        identity = _identity;
        free = new List<GameObject>();
        busy = new List<GameObject>();
        Initialize();
    }
    public void Initialize()
    {
        for ( int i=0; i< 2; i++)
        {
            free.Add(Instanciate());
        }
    }

    public GameObject Instanciate()
    {
        GameObject g = GameObject.Instantiate(prefab, position, identity);
        g.SetActive(false);
        return g;
    }
    public void OI()
    {
        Debug.Log("OI");
    }

	public void PushBusy(GameObject o)
	{

	
	}
    public void getEnemy()
    {   
        Debug.Log("Cheguei");   
        if (free.Count == 0)
        {
            GameObject o =  Instanciate();
            o.SetActive(true);
            busy.Add(o);
        }
        else
        {
            GameObject o = free[0];
            free.Remove(o);
            busy.Add(o);
            o.SetActive(true);
        }   
    }
    public static void Reuse(GameObject o)
    {
        o.GetComponent<Enemy>().resetar(position);

        o.SetActive(false);
        busy.Remove(o);
        free.Add(o);
    }

}
