using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationButton : GemFlipButton
{
    public Quaternion arrowRotation;
    // Start is called before the first frame update
    void Start()
    {
        arrowRotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void onClick()
    {
        Quaternion currentRotation = gameObject.transform.Find("Arrow").GetComponent<SpriteRenderer>().transform.rotation;
        arrowRotation = gameObject.transform.Find("Arrow").GetComponent<SpriteRenderer>().transform.rotation *= Quaternion.Euler(0,0,90f);

    }
}
