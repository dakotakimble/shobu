using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WallRun : MonoBehaviour {

   /* private bool isWallR = false;
    private bool isWallL = false;
    private RaycastHit hitR;
    private RaycastHit hitL;
    private int jumpCount = 0;
    CharacterController cc;
    FirstPersonController fpc;
    //private RigidbodyFirstPersonController cc;
    private Rigidbody rb;

	
	void Start () {

        cc = GetComponent<CharacterController>();
        //cc = GetComponent<RigidbodyFirstPersonController>();
        rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
        if (cc.isGrounded)
        {
            jumpCount = 0;
        }
        

        if (Physics.Raycast(transform.position, transform.right, out hitR, 1))
        {
            if (hitR.transform.tag == "Wall")
            {      
               Debug.Log("Right Wall");
               isWallR = true;
               isWallL = false;
               jumpCount += 1;
               fpc.m_GravityMultiplier = 0;
               Physics.Raycast(transform.position, -transform.up * 5);
               StartCoroutine(afterRun());              
            }
        }
       
        if (Physics.Raycast(transform.position, -transform.right, out hitL, 1))
        {
            if (hitL.transform.tag == "Wall")
            {
                Debug.Log("Left Wall");
                isWallL = true;
                isWallR = false;
                jumpCount += 1;
                rb.useGravity = false;
                rb.isKinematic = false;
                Debug.Log(Physics.gravity);
                StartCoroutine(afterRun());
            }
        }
	}

    IEnumerator afterRun ()
    {
        yield return new WaitForSeconds(5.0f);
        isWallL = false;
        isWallR = false;
        rb.useGravity = true;
        rb.isKinematic = true;
    }*/
}
