using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //Barrier specific behavior component for GameObjects
    public class Barrier : GemFlipClickable
    {
        int phase;

        override public void onClick()
        {
            Debug.Log("Barrier Clicked");
        }


        public void Initialize(int phase, int row, int column)
        {
            this.phase = phase;
            this.row = row;
            this.column = column;
        }
    }

