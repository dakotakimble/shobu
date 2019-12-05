using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_WallRun : MonoBehaviour {

    void Update () {

        CharacterController controller = GetComponent<CharacterController>();
        if ((controller.collisionFlags & CollisionFlags.CollidedSides) != 0)
        {
            Physics.Raycast(transform.position, -transform.up * 5);
            
            StartCoroutine(afterRun());
        }
    }

    void WallRun()
    {
        StartCoroutine(afterRun());
    }

    IEnumerator afterRun()
    {
        yield return new WaitForSeconds(1.0f);
        //rb.useGravity = true;
    }
}
