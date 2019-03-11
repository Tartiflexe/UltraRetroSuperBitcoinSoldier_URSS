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
			aim.x = 5f;
		}
		else if(playerIndex == 2)
		{
			aim.x = -5f;
		}

		/* if(other.GetComponent<Bitcoin>().hitCount == 0)
		{			
			if(playerIndex == 1) 
			{
				aim.x = 10f;
			}
			else if(playerIndex == 2)
			{
				aim.x = -10f;
			}
			other.GetComponent<Rigidbody2D>().velocity = aim;
		}*/

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
