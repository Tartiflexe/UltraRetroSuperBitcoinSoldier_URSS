using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour {

	public int playerIndex;
	public bool atk;

	public Vector2 aim;
	void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag("Bitcoin")) return;
		
		if(playerIndex == 1) 
		{
			aim.x = 1f;
		}
		else if(playerIndex == 2)
		{
			aim.x = -1f;
		}

		if(other.GetComponent<Bitcoin>().hitCount == 0)
		{			
			other.GetComponent<Rigidbody2D>().velocity = aim * 200;
		}

		if(atk)
		{
			other.GetComponent<Bitcoin>().Double(playerIndex,aim);
		}
		else
		{
			other.GetComponent<Bitcoin>().Stock(playerIndex);
		}
	}
}
