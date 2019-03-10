﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionCode2D.Renderers;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCtrl : MonoBehaviour {
	public enum PlayerNumber
	{
		Player1,
		Player2
	}
	public Animator animator;
	GameObject GetObject;
	
	public PlayerNumber playerNb;
	Vector2 move;
	Rigidbody2D rb;
	public float speed = 4;

	Vector3 posOnStartDash;
	Vector3 posEndDash;
	bool dashing = false;	
	public AnimationCurve easing;
	public SpriteGhostTrailRenderer ghost;
	Vector2 lastBigMove = Vector2.one;

	float timer;
	// Use this for initialization
	
	void Start () 
	{	
		rb = GetComponent<Rigidbody2D>();
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(playerNb)
		{
			case PlayerNumber.Player1:
				InputMoveP1();
			break;

			case PlayerNumber.Player2:
				InputMoveP2();
			break;
		}

			
	}

	void InputMoveP1()
	{	
		float x = Input.GetAxis("HorizontalP1");
		float y = Input.GetAxis("VerticalP1");
			
		move = new Vector2(x, y);

		if (move.magnitude>=0.1f)
		{
			animator.SetBool("Run", true);
		}
		else
		{
			animator.SetBool("Run", false);
		}
		

		rb.velocity = (move * speed * Time.deltaTime);	
		if(Input.GetKeyDown(KeyCode.Joystick1Button0) && !dashing)
		{
			Debug.Log("Hey"+ this.gameObject);
			
			posOnStartDash = transform.position;
			posEndDash = transform.position + (Vector3)move.normalized * 3;
			LayerMask mask = LayerMask.GetMask("Default");
			RaycastHit2D hit = Physics2D.Raycast(transform.position,move,Vector3.Distance(transform.position,posEndDash));
			if(hit)
			{			
				Debug.Log(hit.collider.gameObject);
				posEndDash = hit.point;
			} 
			timer = 0;
			dashing = true;
		}
		if(dashing)
		{
			if(!ghost.isActiveAndEnabled)
				ghost.enabled = true;

			timer+= Time.deltaTime * 5;
			if(timer>=1)
			{
				timer=1;				
				dashing = false;
				ghost.enabled = false;
			}
			
			transform.position = Vector3.Lerp(posOnStartDash,posEndDash,easing.Evaluate(timer));
		}
	}	

	void InputMoveP2()
	{	
		float x = Input.GetAxis("HorizontalP2");
		float y = Input.GetAxis("VerticalP2");
			
		move = new Vector2(x, y);

		if (move.magnitude>=0.1f)
		{
			animator.SetBool("Run", true);
		}
		else
		{
			animator.SetBool("Run", false);
		}
		if (move.magnitude>=0.6f)
		{
			lastBigMove = move;
		}

		rb.velocity = (move * speed * Time.deltaTime);	
		if(Input.GetKeyDown(KeyCode.Joystick2Button0) && !dashing)
		{
			Debug.Log("Hey"+ this.gameObject);
			
			posOnStartDash = transform.position;
			posEndDash = transform.position + (Vector3)move.normalized * 3;
			LayerMask mask = LayerMask.GetMask("Default");
			RaycastHit2D hit = Physics2D.Raycast(transform.position,move,Vector3.Distance(transform.position,posEndDash));
			if(hit)
			{			
				Debug.Log(hit.collider.gameObject);
				posEndDash = hit.point;
			} 
			timer = 0;
			dashing = true;
		}
		if(dashing)
		{
			if(!ghost.isActiveAndEnabled)
				ghost.enabled = true;

			timer+= Time.deltaTime * 5;
			if(timer>=1)
			{
				timer=1;				
				dashing = false;
				ghost.enabled = false;
			}
			
			transform.position = Vector3.Lerp(posOnStartDash,posEndDash,easing.Evaluate(timer));
		}
	}	
}
