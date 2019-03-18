using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class boxCollider : MonoBehaviour {

    //Reference Variables
    Transform trans;
    SpriteRenderer sr;

    //Box Collider Variables
    public int collisionType;
    public float height;
    public float width;
    static List<boxCollider> boxList = new List<boxCollider>();
    static boxCollider[] removeArray = new boxCollider[16];

    //Define Reference Variables
    private void OnEnable()
    {
        trans = GetComponent<Transform>();
    }

    // Use this for initialization
    void Start() {

        //Add Self Instance To Array
        boxList.Add(this);

        //Set Scale
        if (GetComponent<SpriteRenderer>() != null)
        {
            //Set Reference Var
            sr = GetComponent<SpriteRenderer>();

            //Set Scale 
            height = sr.bounds.size.y;// * trans.localScale.y;
            width = sr.bounds.size.x;// * trans.localScale.x;

            //sr.bounds
            //Debug.Log(height);
            //Debug.Log(width);
            //Debug.Log(sr.bounds.size.x);
        }
    }

    // Update is called once per frame
    void Update() {

    }

    //Destroy List Reference
    void OnDestroy()
    {
        boxList.Remove(this);
    }

    //Check For Collision 
    public bool placeMeeting(float x, float y, int type) //, boxCollider box)
    {
        //Check All Box Colliders Instances
        foreach (boxCollider element in boxList)
        {
            //Check That Instance To Be Tested Isn't "This"
            if (element != this && element != null)
            {
                //Check For Collision (Min Right > Max Left) (Min Up > Max Down)
                if (Mathf.Min(x + width / 2, element.trans.position.x + element.width / 2) > Mathf.Max(x - width / 2, element.trans.position.x - element.width / 2)
                && Mathf.Min(y + height / 2, element.trans.position.y + element.height / 2) > Mathf.Max(y - height / 2, element.trans.position.y - element.height / 2))
                {
                    //Default Collisions
                    if (element.collisionType == type)
                    {
                        return true;
                    }

                    /*int enumCount = System.Enum.GetValues(typeof(ct.type)).Length;
                    for (int i = 0;i < enumCount;i++)
                    {
                        if(i == type && )
                        {
                            return true;
                        }
                    }*/

                    //Return Nothing If Nothing Is Found
                    return false;
                }
            }

            //Destroy Null Elements In List
            /*else if (element == null)
            {
                for (int i = 0; i < removeArray.Length; i++)
                {
                    //Fill In First Null Element
                    if (removeArray[i] == null)
                    {
                        removeArray[i] = element;
                        break;
                    }
                }
            }*/
        }

        //Remove All Null Elements From Remove Array
        /*for(int i = 0;i < removeArray.Length;i++)
        {
            if (removeArray[i] != null && boxList.Contains(removeArray[i]))
            {
                boxList.Remove(removeArray[i]);
                removeArray[i] = null;
            }

            Debug.Log("element removed");
        }*/

        //Return Nothing If No Collision Is Detected
        return false;
    }

    
}
