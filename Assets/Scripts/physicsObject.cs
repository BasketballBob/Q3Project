using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class physicsObject : MonoBehaviour {

    //Reference Variables
    Transform trans;
    boxCollider bc;

    //Physics Object Variables
    public static float minMove = .01f; //.001f
    public float hSpeed = 0;
    public float vSpeed = 0;
    float prevX;
    float prevY;

    //Physics Object Toggleable Variables 
    public bool destroyOnCollide = false;
    public bool bounceOnCollide = false;

    //Define Reference Variables
    private void OnEnable()
    {
        trans = GetComponent<Transform>();
        bc = GetComponent<boxCollider>();
    }

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {


        //Destroy After Movement (Destroys After Collision Is Shown)
        if(destroyOnCollide)
        { 
            //(Check Previous Collision)
            if (bc.placeMeeting(prevX, prevY + minMove * Sign(vSpeed), (int)ct.type.wall)
            || bc.placeMeeting(prevX + minMove * Sign(hSpeed), prevY, (int)ct.type.wall))
            {
                Destroy(gameObject);
            }
        }

        //Vertical Movement
        if(vSpeed != 0)
        {
            if(!bc.placeMeeting(trans.position.x,trans.position.y+vSpeed*Time.deltaTime, (int)ct.type.wall))
            {
                trans.position += new Vector3(0, vSpeed * Time.deltaTime);
            }
            else if(!bc.placeMeeting(trans.position.x,trans.position.y+minMove*Sign(vSpeed), (int)ct.type.wall))
            {
                do
                {
                    trans.position += new Vector3(0, minMove * Sign(vSpeed));
                }
                while (!bc.placeMeeting(trans.position.x, trans.position.y + minMove * Sign(vSpeed), (int)ct.type.wall));
            }

            //On Collision Modifications
            if (bc.placeMeeting(trans.position.x, trans.position.y + minMove * Sign(vSpeed), (int)ct.type.wall))
            {
                //Bounce
                if (bounceOnCollide)
                {
                    vSpeed *= -1;
                }
            }
        }

        //Horizontal Movement
        if(hSpeed != 0)
        {
            if(!bc.placeMeeting(trans.position.x + hSpeed * Time.deltaTime, trans.position.y, (int)ct.type.wall))
            {
                trans.position += new Vector3(hSpeed * Time.deltaTime, 0);
            }
            else if(!bc.placeMeeting(trans.position.x+minMove*Sign(hSpeed), trans.position.y, (int)ct.type.wall))
            {
                float hcount = 0;
                do
                {
                    trans.position += new Vector3(minMove * Sign(hSpeed), 0);
                    hcount += 1;
                    //Debug.Log(hcount);
                }
                while (!bc.placeMeeting(trans.position.x + minMove * Sign(hSpeed), trans.position.y, (int)ct.type.wall));
            }

            //On Collision Modifications
            if (bc.placeMeeting(trans.position.x + minMove * Sign(hSpeed), trans.position.y, (int)ct.type.wall))
            {
                //Bounce
                if(bounceOnCollide)
                {
                    hSpeed *= -1;
                }
            }
        }

        //Set Prev Position Variables
        prevX = trans.position.x;
        prevY = trans.position.y;

    }

    public float Sign(float input)
    {
        if (input > 0) return 1;
        else if (input < 0) return -1;
        else return 0;
    }
}
