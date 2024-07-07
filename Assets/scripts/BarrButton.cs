using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrButton : GemFlipButton
{
    bool barrier_active;

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
