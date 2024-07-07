using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : GemFlipClickable
{
    int phase;
    int color;
    int direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void onClick()
    {
        Debug.Log("Drone Clicked");
    }


    public void Initialize(int color, int phase, int direction)
    {
        this.color = color;
        this.phase = phase;
        this.direction = direction;
    }


}
