using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //non-Mono class, used as a container to pass the state
    //of all buttons (player selections) between parts of the
    //code
    public class PlayerSelections
    {
        public int phase;
        public int color;
        public int barrier;
        public int direction;

        public PlayerSelections(int phase, int color, int barrier, int direction)
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
