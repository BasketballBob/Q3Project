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
    float bulletSpeed = 10f;
    float bulletAlarm = 0;
    float bulletTime = 1;


    //Input Variables
    float vAxis = 0; // Input.GetAxis("VerticalJoy"); 
    float hAxis = 0; // Input.GetAxis("HorizontalJoy");
    float aimV = 0; // Input.GetAxis("VerticalJoy2");
    float aimH = 0; // Input.GetAxis("VerticalJoy");
    float shoot = 0;

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
        vAxis = Input.GetAxis("VerticalJoy"); //Input.GetAxis("Vertical");
        hAxis = Input.GetAxis("HorizontalJoy"); //Input.GetAxis("Horizontal");
        aimV = Input.GetAxis("VerticalJoy2");
        aimH = Input.GetAxis("HorizontalJoy2");
        shoot = Input.GetAxis("RightTrigger");

        if (shoot != 0) Debug.Log(shoot);

        //Player Movement 
        po.vSpeed = moveSpeed * vAxis;
        po.hSpeed = moveSpeed * hAxis;

        //Player Shooting
        if(bulletAlarm <= 0 && shoot == 1)
        {
            //Create Bullet 
            GameObject tvBullet = Instantiate(bulletReference);
            tvBullet.GetComponent<physicsObject>().hSpeed = aimH / (Mathf.Abs(aimH) + Mathf.Abs(aimV));
            tvBullet.GetComponent<physicsObject>().vSpeed = aimV / (Mathf.Abs(aimH) + Mathf.Abs(aimV));
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
        //Draw Debug Labels
        float labelWidth = 100;
        float labelHeight = 25;
        GUI.Label(new Rect(10, 10, labelWidth, labelHeight), aimV.ToString());
        GUI.Label(new Rect(10, 50, labelWidth, labelHeight), aimH.ToString());
    }
}
