using System.Collections;
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
	public GameObject HitBox;

	float dashTimer;
	float atkTimer;
	bool attaking = false;
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
				InputAtkP1();
			break;

			case PlayerNumber.Player2:
				InputMoveP2();
				InputAtkP2();
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
			if (move.x<=-0.1f)
			{
				animator.SetBool("Run", false);
				animator.SetBool("Back", true);
			}
			else
			{
				animator.SetBool("Back", false);
			}
			animator.SetBool("Run", true);
		}
		else
		{
			animator.SetBool("Run", false);
			animator.SetBool("Back", false);
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
			dashTimer = 0;
			dashing = true;
		}
		if(dashing)
		{
			if(!ghost.isActiveAndEnabled)
				ghost.enabled = true;

			dashTimer+= Time.deltaTime * 6;
			if(dashTimer>=1)
			{
				dashTimer=1;				
				dashing = false;
				ghost.enabled = false;
			}
			
			transform.position = Vector3.Lerp(posOnStartDash,posEndDash,easing.Evaluate(dashTimer));
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
			dashTimer = 0;
			dashing = true;
		}
		if(dashing)
		{
			if(!ghost.isActiveAndEnabled)
				ghost.enabled = true;

			dashTimer+= Time.deltaTime * 5;
			if(dashTimer>=1)
			{
				dashTimer=1;				
				dashing = false;
				ghost.enabled = false;
			}
			
			transform.position = Vector3.Lerp(posOnStartDash,posEndDash,easing.Evaluate(dashTimer));
		}
	}	

	void InputAtkP1()
	{
		if(Input.GetKeyDown(KeyCode.Joystick1Button2) && !attaking)
		{
			if(!HitBox.activeSelf) HitBox.SetActive(true);
			atkTimer = 0;
			HitBox.GetComponent<Hit>().atk = true;
			HitBox.GetComponent<Hit>().aim = move;
			attaking = true;
			animator.SetTrigger("Doble");			
		}
		if(Input.GetKeyDown(KeyCode.Joystick1Button1) && !attaking)
		{
			if(!HitBox.activeSelf) HitBox.SetActive(true);
			atkTimer = 0;
			HitBox.GetComponent<Hit>().atk = false;			
			attaking = true;
			animator.SetTrigger("Stock");				
		}
		
		if(attaking)
		{
			atkTimer += Time.deltaTime;
			if(atkTimer >=1)
			{
				atkTimer =1;
				HitBox.SetActive(false);
				attaking = false;
			}
		}
	}

	void InputAtkP2()
	{
		if(Input.GetKeyDown(KeyCode.Joystick2Button2) && !attaking)
		{
			if(!HitBox.activeSelf) HitBox.SetActive(true);
			atkTimer = 0;
			HitBox.GetComponent<Hit>().atk = true;
			HitBox.GetComponent<Hit>().aim = move;
			attaking = true;
			animator.SetTrigger("Doble");			
		}
		if(Input.GetKeyDown(KeyCode.Joystick2Button1) && !attaking)
		{
			if(!HitBox.activeSelf) HitBox.SetActive(true);
			atkTimer = 0;
			HitBox.GetComponent<Hit>().atk = false;			
			attaking = true;	
			animator.SetTrigger("Stock");			
		}

		if(attaking)
		{
			atkTimer += Time.deltaTime;
			if(atkTimer >=1)
			{
				atkTimer =1;
				HitBox.SetActive(false);
				attaking = false;
			}
		}
	}
}
