using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ct : MonoBehaviour
{
    //NOTE: This class exists solely to hold the collision type enumerator
    //(placed in class seperate from boxCollider to reduce char count)

    //Global Collision Type Enumerator
    public enum type {wall, player, enemy, bullet};

}


