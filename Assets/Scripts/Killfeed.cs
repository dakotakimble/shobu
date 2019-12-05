using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killfeed : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager.instance.onPlayerKilledCallback += OnKill;
	}

    public void OnKill (string player, string source)
    {
        Debug.Log(source + " killed " + player);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
