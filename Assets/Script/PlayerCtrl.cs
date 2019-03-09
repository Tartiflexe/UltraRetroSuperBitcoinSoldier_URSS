using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCtrl : MonoBehaviour {
	
	Vector3 move;
	Rigidbody2D rb;
	public float speed = 4;
	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Move();
	}

		void Move()
	{
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
	
		move = new Vector3 (x, y, 0);
		rb.velocity = (move * speed * Time.deltaTime);
		//transform.position += move * speed * Time.deltaTime;
	}
}
