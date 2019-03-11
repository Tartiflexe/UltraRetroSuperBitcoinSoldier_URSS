using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour {

	public float timer;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer<=0)
		{
			SceneManager.LoadScene(0);
		}
	}
}
