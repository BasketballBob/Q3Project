using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

    //Reference Variables
    Transform trans;
    physicsObject po;
    boxCollider bc;

    public GameObject gunReference;
    public GameObject bulletReference;
    GameObject gun;

    //Player Variables
    float moveSpeed = 6f;
    float aimAngle = 0;
    float bulletSpeed = 15f;
    float bulletAlarm = 0;
    float bulletTime = 1/3;

    //Dashing Variables
    float dashSpeed = 25f;
    float dashAlarm = 0;
    float dashTime = 1/15f;
    float dashDelayAlarm = 0;
    float dashDelayTime = 2f;


    //Input Variables
    float vAxis = 0; // Input.GetAxis("VerticalJoy"); 
    float hAxis = 0; // Input.GetAxis("HorizontalJoy");
    float aimV = 1; // Input.GetAxis("VerticalJoy2");
    float aimH = 1; // Input.GetAxis("VerticalJoy");
    float shoot = 0;
    float dash = 0;

    //Define Reference Variables
    private void OnEnable()
    {
        trans = GetComponent<Transform>();
        po = GetComponent<physicsObject>();
        bc = GetComponent<boxCollider>();
    }
    // Use this for initialization
    void Start () {

        //Create Player Gun
        gun = Instantiate(gunReference); 
	}
	
	// Update is called once per frame
	void Update () {

        //Player Input
        if (dashAlarm == 0) //Don't Allow Player To Change Direction While Dashing
        {
            vAxis = Input.GetAxis("VerticalJoy"); //Input.GetAxis("Vertical");
            hAxis = Input.GetAxis("HorizontalJoy"); //Input.GetAxis("Horizontal");
        }

        if (Mathf.Abs(Input.GetAxis("HorizontalJoy2")) + Mathf.Abs(Input.GetAxis("VerticalJoy2")) != 0) //(Only Take Directional Input)
        {
            aimV = Input.GetAxis("VerticalJoy2");
            aimH = Input.GetAxis("HorizontalJoy2");
        }
        shoot = Input.GetAxis("RightTrigger");
        dash = Input.GetAxis("LeftTrigger");

        if (shoot != 0) Debug.Log(shoot);
        if (dash != 0) Debug.Log(dash);

        //Player Movement 
        if(dashAlarm > 0)
        {
            po.vSpeed = dashSpeed * vAxis;
            po.hSpeed = dashSpeed * hAxis;
        }
        else
        {
            po.vSpeed = moveSpeed * vAxis;
            po.hSpeed = moveSpeed * hAxis;
        }

        //Manage Bullet Alarm
        if (bulletAlarm - Time.deltaTime > 0)
        {
            bulletAlarm -= Time.deltaTime;
        }
        else bulletAlarm = 0;

        //Player Shooting
        if(bulletAlarm <= 0 && shoot == 1)
        {
            //Create Bullet 
            GameObject tvBullet = Instantiate(bulletReference);
            tvBullet.GetComponent<physicsObject>().hSpeed = bulletSpeed * (aimH / (Mathf.Abs(aimH) + Mathf.Abs(aimV)));
            tvBullet.GetComponent<physicsObject>().vSpeed = bulletSpeed * (aimV / (Mathf.Abs(aimH) + Mathf.Abs(aimV)));
            tvBullet.GetComponent<Transform>().position = trans.position;

            //Set Bullet Alarm
            bulletAlarm = bulletTime;
        }

        //Player Dashing Duration
        if(dashAlarm > 0)
        {
            //Deduct Alarm
            if (dashAlarm-Time.deltaTime > 0)
            {
                dashAlarm -= Time.deltaTime;
            }
            //Initialize Variables
            else
            {
                dashDelayAlarm = dashDelayTime;
                dashAlarm = 0;          
            }
        }
        //Player Dashing Delay
        else if (dashDelayAlarm > 0)
        {
            //Deduct Alarm
            if(dashDelayAlarm-Time.deltaTime > 0)
            {
                dashDelayAlarm -= Time.deltaTime;
            }
            //Initialize Variables
            else
            {
                dashDelayAlarm = 0;
            }                
        }     
        //Player Dashing Input
        else if (dash == 1)
        {
            dashAlarm = dashTime;
        }

    }

    private void LateUpdate()
    {
        //Player Gun Management 
        Transform gunTrans = gun.GetComponent<Transform>();
        aimAngle = (Mathf.Atan2(aimV, aimH) / Mathf.PI) * 180;
        gunTrans.position = new Vector3(trans.position.x, trans.position.y);
        if (aimV != 0 || aimH != 0)
        {
            gunTrans.eulerAngles = new Vector3(0, 0, aimAngle);
        }
        //Debug.Log(aimAngle);
    }

    private void OnGUI()
    {
        //Test GUI
        int testWidth = 100;
        int testHeight = 20;
        GUI.Box(new Rect(20, 20, 20 + testWidth * (dashDelayAlarm / dashDelayTime), 20 + testHeight), "Dash");

        //Draw Debug Labels
        float labelWidth = 100;
        float labelHeight = 25;
        GUI.Label(new Rect(10, 10, labelWidth, labelHeight), aimV.ToString());
        GUI.Label(new Rect(10, 50, labelWidth, labelHeight), aimH.ToString());        
    }
}
