using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class But : MonoBehaviour
{
    int bank = 0;
    int Nstock;
    public GameObject But_part;
    public enum ButState
    {
        State1, State2, State3
    }
    public ButState butState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    public void StockUpdate()
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
        StockUpdate();
    }
    void OnTriggerEnter2D(Collider2D Bitcoin)
    {
        if( Bitcoin.gameObject.CompareTag("Bitcoin"))
        {
            Instantiate(But_part,Bitcoin.gameObject.transform);
            bank += Bitcoin.gameObject.GetComponent<Bitcoin>().Bitvalue;
            Bitcoin.gameObject.GetComponent<Bitcoin>().Bitvalue = Bitcoin.gameObject.GetComponent<Bitcoin>().Startvalue;
            Restart();
        }
        
    }
}
