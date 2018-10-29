﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 10f;
	public Vector2 maxVelocity = new Vector2(3,5);
	public Vector3 rollSpeed = new Vector3(0,0,30f);
	public bool standing;
	public float jetSpeed = 15f;
	public float airSpeedMultiplier = .3f;

	private PlayerController controller;
	private Animator animator;

	void Start(){
		controller = GetComponent<PlayerController> ();
		animator = GetComponent<Animator> ();
	}
	// Update is called once per frame
	void Update () {
		var forceX = 0f;
		var forceY = 0f;
		var forceZ = 10f;

		var absVelX = Mathf.Abs (GetComponent<Rigidbody2D>().velocity.x);
		var absVelY = Mathf.Abs (GetComponent<Rigidbody2D>().velocity.y);

		if (absVelY < .2f) {
			standing = true;
		} else {
			standing = false;
		}

		if (controller.moving.x != 0) {
			if (absVelX < maxVelocity.x) {

				forceX = standing ? speed * controller.moving.x : (speed * controller.moving.x * airSpeedMultiplier);

				transform.localScale = new Vector3 (forceX > 0 ? 1 : -1, 1, 1);	
			}	
			
			animator.SetInteger ("AnimState", 1);
		} else {
			animator.SetInteger("AnimState", 0);
		}
		
		//Rotate player back to upright after roll
		if (controller.rolling != true)
		{
			if (transform.rotation.z != 0)
			{
				if (transform.localScale.x == 1)
				{
					GetComponent<Rigidbody2D>().fixedAngle = false;
					//transform.Rotate(-rollSpeed * Time.deltaTime);
					transform.Rotate(0,0,0);
				}
				else
				{
					Debug.Log("Not rolling but has none zero rot.z");
					GetComponent<Rigidbody2D>().fixedAngle = false;
					//transform.Rotate(rollSpeed * Time.deltaTime);
					transform.Rotate(0,0,0);
				}
			}
			else if (transform.rotation.z == 0)
			{
				GetComponent<Rigidbody2D>().fixedAngle = true;
				Debug.Log("Reached rot.z, fixedAngle = true now");
			}
		}

		if (controller.moving.y > 0) {
				if (absVelY < maxVelocity.y)
						forceY = jetSpeed * controller.moving.y;

				animator.SetInteger ("AnimState", 2);
		} else if (absVelY > 0) {
			animator.SetInteger("AnimState", 3);
		}

		GetComponent<Rigidbody2D>().AddForce (new Vector2 (forceX, forceY));

	}
}
