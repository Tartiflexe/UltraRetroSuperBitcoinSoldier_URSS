using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class But : MonoBehaviour
{
    int bank = 0;
    public int Nstock;
    public GameObject But_prtcl;
    public enum ButState
    {
        State1, State2, State3
    }
    public ButState butState;
    public Bitcoin RefBitcoin; 
	public GameObject Score;
	public int LastBitcoin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {}
    public void ButSizeUpdate()
    {
        Vector2 hight1 = new Vector2(1,2.3f);
        Vector2 hight2 = new Vector2(1,6);
        Vector2 hight3 = new Vector2(1,9);

        if(Nstock == 0) butState = ButState.State1;
        if(Nstock == 1) butState = ButState.State2;
        if(Nstock == 2) butState = ButState.State3;
        
        switch(butState)
        {
            case ButState.State1:
                GetComponent<BoxCollider2D>().size = hight1;
            break;

            case ButState.State2:
              GetComponent<BoxCollider2D>().size = hight2;
            break;

            case ButState.State3:
              GetComponent<BoxCollider2D>().size = hight3;
            break;
        }
    }
    public void Restart()
    {
        Nstock = 0;
        ButSizeUpdate();
    }
	
    public void AddBitcoinToBank()
    {
		LastBitcoin = RefBitcoin.gameObject.GetComponent<Bitcoin>().Bitvalue;
        bank += RefBitcoin.gameObject.GetComponent<Bitcoin>().Bitvalue;
        RefBitcoin.gameObject.GetComponent<Bitcoin>().Bitvalue = RefBitcoin.gameObject.GetComponent<Bitcoin>().Startvalue;
		GameObject newScore = Instantiate(Score, RefBitcoin.transform.position,new Quaternion(0,0,0,1));
		newScore.GetComponent<Score>().SetText(LastBitcoin);
		

    }
    void OnTriggerEnter2D(Collider2D Bitcoin)
    {
        if( Bitcoin.gameObject.CompareTag("Bitcoin"))
        {
            Instantiate(But_prtcl,Bitcoin.gameObject.transform);
            AddBitcoinToBank();
            Restart();
        }
        
    }
}
