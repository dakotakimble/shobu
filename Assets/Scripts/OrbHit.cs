using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbHit : MonoBehaviour
{


    //When player runs over gun, they pick it up
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player")
        {

        }
    }
}
