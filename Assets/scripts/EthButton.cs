using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    //toggle button to determine whether the player
    //wants to place a drone/barrier in solid phase or ether
    //(this determines collision behavior, will document game rules shortly)
    public class EthButton : GemFlipButton
    {
        public bool eth_active;
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
            if (eth_active)
            {
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/tile_eth_on");
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/tile_eth");
            }
        }
    }

