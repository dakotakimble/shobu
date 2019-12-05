using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponSway : MonoBehaviour {
    public float mouseX;
    public float mouseY;
    public Quaternion rotationSpeed;

    public float Speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
       
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");

            rotationSpeed = Quaternion.Euler(-mouseY, mouseX, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, rotationSpeed, Speed * Time.deltaTime);
        


       
	}
}
