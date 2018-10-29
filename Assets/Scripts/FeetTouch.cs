using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetTouch : MonoBehaviour {
    public Player playerScript;
    public PlayerController playerControllerScript;

	// Use this for initialization
	void Start () {
        //playerScript = transform.parent.gameObject.GetComponent<Player>(); 
        //playerControllerScript = transform.parent.gameObject.GetComponent<PlayerController>(); 
        playerScript = GameObject.Find("Player").GetComponent <Player> ();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        GetComponent<Collider2D>().attachedRigidbody.SendMessage("OnCollisionEnter2D", col);

        /*
        if(col.gameObject.tag == "Ground")
        {
            playerScript.StopRolling(playerScript.rest);
            Debug.Log("Feet touched Ground!");
        }
        */
    }

    private void OnTriggerEnter2D()
    {
        playerScript.StopRolling(playerScript.rest);
        Debug.Log("Feet touched Ground!");
        
    }
}
