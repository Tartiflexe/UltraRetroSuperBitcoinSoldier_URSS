using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bitcoin : MonoBehaviour 
{
	public But P1But;
	public bool GrowUpStock = false;
	public bool GrowUpDouble = false;
	Vector3 InitialScaleTransform;
	public AnimationCurve bumpCurv;
	public Animator stockP1;
	public Animator stockP2;
	public But P2But;
	public int Bitvalue;
	public int Startvalue;
	float BitSpeed;
	int LastPlayer;

	public float hitCount = 0;
	Vector2 Velocity;
	Rigidbody2D rigidBody;

	float Progress = 0;
	public float Duration = 1;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
		InitialScaleTransform = transform.localScale;
		Startvalue =Bitvalue;
		Spawn();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log(rigidBody.velocity);
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
			 Spawn();
			}
			transform.localScale = Vector3.Lerp(InitialScaleTransform, InitialScaleTransform * 2, bumpCurv.Evaluate(Progress * 1/Duration));
		}
		if(GrowUpDouble == true)
		{
			float _currSpeed = rigidBody.velocity.magnitude;
			if(Mathf.Abs(_currSpeed)>=100)
			{
				Progress += Time.deltaTime;
				if(Progress >= 1 )//fin anim jump
				{
					Progress = 0;			 
					rigidBody.velocity = Velocity;// * hitCount;
					GrowUpDouble = false;
				}
				transform.localScale = Vector3.Lerp(InitialScaleTransform, InitialScaleTransform * 2, bumpCurv.Evaluate(Progress * 1/Duration));
			}
			else
			{
				rigidBody.velocity = Velocity * hitCount;
				GrowUpDouble = false;
			}
			
		}
	
	}
	public void Spawn()
	{
		hitCount = 0;
		if(LastPlayer != 0)
		{
			transform.position = new Vector3 (0,15,0);
			if(LastPlayer == 1) rigidBody.velocity = new Vector2(1,-1);
			if(LastPlayer == 2) rigidBody.velocity = new Vector2(-1,1);
		}
		transform.position = new Vector3 (0,0,0);
	}
	public void Stock(int Playerindex)
	{
		rigidBody.velocity = new Vector2 (0,0);
		if(Playerindex == 1)
		{
			stockP1.SetTrigger("Stock");
			LastPlayer = Playerindex;
			P1But.Nstock ++;
			P1But.ButSizeUpdate();
			P1But.AddBitcoinToBank();	
			GrowUpStock = true;
		}
		else if(Playerindex == 2)
		{
			stockP2.SetTrigger("Stock");
			LastPlayer = Playerindex;
			P2But.Nstock ++;
			P2But.ButSizeUpdate();
			P2But.AddBitcoinToBank();
			GrowUpStock = true;
		}

	}
	public void Double(int Playerindex, Vector2 aim)
	{
		Debug.Log("DOUBLE!!!");
		Velocity = aim;
		rigidBody.velocity = new Vector2 (0,0);
		LastPlayer = Playerindex;
		Bitvalue = Bitvalue * 2;
		hitCount += 0.5f;
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
