using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //Drone GameObject specific behavior
    public class Drone : GemFlipClickable
    {
        public int phase;
        public int color;
        public int direction;

        override public void onClick()
        {
            Debug.Log("Drone Clicked");
        }


        public void Initialize(int color, int phase, int direction, int row, int column)
        {
            this.color = color;
            this.phase = phase;
            this.direction = direction;
            this.row = row;
            this.column = column;
        }


    }


