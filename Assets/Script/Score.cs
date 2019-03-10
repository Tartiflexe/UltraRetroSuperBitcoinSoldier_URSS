using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Score : MonoBehaviour {
	float Progress = 0;
public TextMeshPro ScoreText;
	// Use this for initialization
	void Start () {

	}
	public void SetText(int BitValue)
	{
	ScoreText.text = "+" + BitValue.ToString(); 
	}
	// Update is called once per frame
	void Update () {
		Progress += Time.deltaTime;
		if(Progress >= 1.5f)
		{
			Destroy(this.gameObject);
		}
	}
}
