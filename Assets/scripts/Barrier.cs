using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : GemFlipClickable
{
    int phase;
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
        Debug.Log("Barrier Clicked");
    }


    public void Initialize(int phase)
    {
        this.phase = phase;
    }
}
