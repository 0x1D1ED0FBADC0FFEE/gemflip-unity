using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelections
{
    int phase;
    int color;
    int barrier;
    Quaternion direction;

    public PlayerSelections(int phase, int color, int barrier, Quaternion direction) 
    {
        this.phase = phase;
        this.color = color; 
        this.barrier = barrier;
        this.direction = direction;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
