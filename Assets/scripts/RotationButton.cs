using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //will determine rotation or where a placed drone is facing
    //(game constants use cardinal directions)
    public class RotationButton : GemFlipButton
    {
        public override void onClick()
        {
            //this rotates the arrow upon clicking the button
            gameObject.transform.Find("Arrow").rotation *= Quaternion.Euler(0, 0, 90f);

        }
    }


