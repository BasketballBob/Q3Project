using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    //Bullet Variables 
    float lifeAlarm = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Deduct LifeAlarm
        if (lifeAlarm > 0)
        {
            lifeAlarm -= Time.deltaTime;
        }
        //Destroy Bullet
        else
        {
            //Destroy(gameObject);
        }
	}
}
