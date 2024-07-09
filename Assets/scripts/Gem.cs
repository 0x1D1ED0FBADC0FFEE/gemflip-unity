using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //gem specific behavior component
    public class Gem : GemFlipClickable
    {
        int color;

        override public void onClick()
        {
            //Debug.Log("Gem Clicked");
        }


        public void Initialize(int color, int row, int column)
        {
            this.color = color;
            this.row = row;
            this.column = column;
        }
    }



