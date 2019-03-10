using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bitcoin : MonoBehaviour {
public But P1But;
public bool GrowUpStock = false;
public bool GrowUpDouble = false;
Vector3 InitialScaleTransform;
public AnimationCurve FlipCoin;
public But P2But;
public int Bitvalue;
public int Startvalue;
float BitSpeed;
int LastPlayer;
Vector2 Velocity;
Rigidbody2D Rigidbody;

float Progress = 0;
public float Duration = 1;

	// Use this for initialization
	void Start () {
		InitialScaleTransform = transform.localScale;
		Startvalue =Bitvalue;
		Spawn();
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GrowUpStock == true)
		{
			Progress += Time.deltaTime;
			if(transform.localScale.x == 2)//StopAnimTop
			{
				Progress = 1;
				Spawn();
			}
			if(Progress >= 1 )
			{
			 Progress = 0;
			 GrowUpStock = false;
			}
			transform.localScale = Vector3.Lerp(InitialScaleTransform, InitialScaleTransform * 2, FlipCoin.Evaluate(Progress * 1/Duration));
		}
		if(GrowUpDouble == true)
		{
			Progress += Time.deltaTime;
			if(Progress >= 1 )//fin anim jump
			{
			 Progress = 0;
			 GrowUpDouble = false;
			 Rigidbody.velocity = Velocity * -2;
			}
			transform.localScale = Vector3.Lerp(InitialScaleTransform, InitialScaleTransform * 2, FlipCoin.Evaluate(Progress * 1/Duration));
		}
	
	}
	public void Spawn()
	{
		if(LastPlayer != 0)
		{
		transform.position = new Vector3 (0,4,0);
		if(LastPlayer == 1) Rigidbody.velocity = new Vector2(1,-1);
		if(LastPlayer == 2) Rigidbody.velocity = new Vector2(-1,1);
		}
		transform.position = new Vector3 (0,0,0);
	}
	public void Stock(int Playerindex)
	{
		Rigidbody.velocity = new Vector2 (0,0);
		if(Playerindex == 1)
		{
			LastPlayer = Playerindex;
			P1But.Nstock ++;
			P1But.ButSizeUpdate();
			P1But.AddBitcoinToBank();	
			GrowUpStock = true;
		}
		if(Playerindex == 2)
		{
			LastPlayer = Playerindex;
			P2But.Nstock ++;
			P2But.ButSizeUpdate();
			P2But.AddBitcoinToBank();
			GrowUpStock = true;
		}

	}
	public void Double(int Playerindex)
	{
		Velocity = Rigidbody.velocity;
		Rigidbody.velocity = new Vector2 (0,0);
		LastPlayer = Playerindex;
		Bitvalue = Bitvalue * 2;
		GrowUpDouble = true;
	
	}
}
/*using System.Collections;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }
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
        //bank += Bitcoin.gameObject.GetComponent<Bitcoin>().Bitvalue;
        //Bitcoin.gameObject.GetComponent<Bitcoin>().Bitvalue = Bitcoin.gameObject.GetComponent<Bitcoin>().Startvalue;
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
 */
