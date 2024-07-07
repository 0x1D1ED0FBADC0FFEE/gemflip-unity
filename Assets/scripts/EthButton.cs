using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EthButton : GemFlipButton
{
    bool eth_active;
    // Start is called before the first frame update
    void Start()
    {
        eth_active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //toggles state of ether placement
    public override void onClick()
    {
        eth_active = !eth_active;
        if(eth_active)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/tile_eth_on");
        }
        else 
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/tile_eth");
        }
    }
}
