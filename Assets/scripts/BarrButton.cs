using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //Toggle button, determinse whether player will try to place
    //a barrier on the tile (can only do if they have a drone on tile)
    public class BarrButton : GemFlipButton
    {
        public bool barrier_active;

        public void Start()
        {
            barrier_active = false;
        }
        public override void onClick()
        {
            barrier_active = !barrier_active;
            if (barrier_active)
            {
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/tile_barrier_on");
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/tile_barrier");
            }
        }
    }


