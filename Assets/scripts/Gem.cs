using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : GemFlipClickable
{
    int color;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      //  
    }

    override public void onClick()
    {
        Debug.Log("Gem Clicked");
    }


    public void Initialize(int color)
    {
        this.color = color;
    }
}
