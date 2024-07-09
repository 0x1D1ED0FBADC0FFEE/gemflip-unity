using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
    
    //will try to integrate this class with the GameObjectFactory
    //instead of doing things in place later on 
    public class Tile : GemFlipClickable
    {

        public Color defaultColor;      // Default color of the tile
        private SpriteRenderer renderer;// Reference to the SpriteRenderer component
        public Sprite tileSprite; // Assign your tile texture here


        void Start()
        {
            renderer = GetComponent<SpriteRenderer>();
        }
        
        //After implementing custom raycasts, don't have much use for 
        //OnMouseDown() and Up anymore, so will likely remove them shortly
        void OnMouseDown()
        {
            // Change the tile color when it is clicked

            renderer.color = Color.cyan;
        }

        void OnMouseUp()
        {
            // Change the tile color when it is clicked

            renderer.color = defaultColor;
        }


        public void intialize(int row, int column)
        {
            this.row = row;
            this.column = column;
        }

        //current game logic/rules: clicking on a tile only makes sense if the player wants to 
        //place a drone, any other clicks would go to the game objects directly
        //so only check if they can place a drone and what the selected phase is
        override public void onClick()
        {
            //Debug.Log("Tile Clicked");

            try
            {

                PlayerSelections ps = GameBoard.instance.GetPlayerSelections();

                if (ps.phase == Constants.ETHER && GameBoard.instance.ethers_left == 0)
                {
                    Debug.Log("No ether left to make ether drone.");
                    return;
                }

                if (GameBoard.instance.placed_drone)
                {
                    Debug.Log("Can only place one drone per turn.");
                    return;
                }
                int boardsize = GameBoard.instance.boardsize;
                if (row == 0 || row == boardsize - 1 || column == 0 || column == boardsize - 1)
                {
                    GameObject newDrone = GameObjectFactory.CreateDrone(ps.color, ps.phase, ps.direction, row, column);
                    newDrone.transform.SetParent(gameObject.transform);
                    newDrone.transform.localPosition = new Vector3(0, 0, -2);

                    if (ps.phase == Constants.ETHER)
                        GameBoard.instance.ethers_left--;

                    return;
                }
                else
                {
                    Debug.Log("can only place drones at edge of board.");
                }
            }
            catch (Exception e)
            {
                Debug.Log("Error in onClick() of Tile");
                return;
            }
        }
        //kept to see if periodic state changes
        //(based on unity runtime) will integrate
        //with the .NET MAUI based client
        //currently not using this
        void StartFlashing()
        {
            StartCoroutine(Flash());    // Start the Flash coroutine
        }

        System.Collections.IEnumerator Flash()
        {
            while (true)
            {
                // Flash between the default color and the current color
                renderer.color = defaultColor;
                yield return new WaitForSeconds(1.0f);
                renderer.color = Color.red;
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
