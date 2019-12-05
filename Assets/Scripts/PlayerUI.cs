using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour {

    [SerializeField]
    GameObject scoreboard;

	void Start () {
		
	}
	
	
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.Tab)) {
            scoreboard.SetActive(true);
        } else if(Input.GetKeyUp(KeyCode.Tab))
        {
            scoreboard.SetActive(false);
        }
	}
}
