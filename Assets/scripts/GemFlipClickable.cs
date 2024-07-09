using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    //only purpose of this class is to force an implementation
    //for onClick()
    public abstract class GemFlipClickable : MonoBehaviour
    {
        public int row;
        public int column;

        public abstract void onClick();

    }
