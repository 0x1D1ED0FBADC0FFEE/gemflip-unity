using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//creates GameObjects and decorates with all components

    public static class GameObjectFactory
    {
        public static float sizeModifier { get; private set; }

        public static void Initialize(float sizeMod)
        {
            sizeModifier = sizeMod;
        }

        public static GameObject CreateDrone(int color, int phase, int direction, int row, int column)
        {
            GameObject droneObject = new GameObject("Drone");
            Drone droneComponent = droneObject.AddComponent<Drone>();

            SpriteRenderer renderer = droneObject.AddComponent<SpriteRenderer>();

            //need to know which sprite to pull based on color and phase
            string resourceName = (color == Constants.RED ? "Sprites/drone_red" : "Sprites/drone_blue") 
                                + (phase == Constants.ETHER ? "_eth" : ""); 
           
            renderer.sprite = Resources.Load<Sprite>(resourceName);

            BoxCollider2D collider = droneObject.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(1.0f, 1.0f);

            droneObject.transform.localScale = new Vector3(sizeModifier, sizeModifier, 1.0f);

            droneComponent.Initialize(color, phase, direction, row, column);
            return droneObject;
        }

        public static GameObject CreateBarrier(int phase, int row, int column)
        {
            GameObject barrierObject = new GameObject("Barrier");
            Barrier barrierComponent = barrierObject.AddComponent<Barrier>();
            barrierComponent.Initialize(phase, row, column);
            return barrierObject;
        }

        public static GameObject CreateGem(int color, int row, int column)
        {
            GameObject gemObject = new GameObject("Gem");

            SpriteRenderer renderer = gemObject.AddComponent<SpriteRenderer>();
            renderer.sprite = Resources.Load<Sprite>("Sprites/gem_yellow");

            BoxCollider2D collider = gemObject.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(1.0f, 1.0f);

            gemObject.transform.localScale = new Vector2(sizeModifier, sizeModifier);

            Gem gemComponent = gemObject.AddComponent<Gem>();
            gemComponent.row = row;
            gemComponent.column = column;
            gemComponent.Initialize(color, row, column);

            return gemObject;
        }

        public static GameObject CreateTile(int row, int column)
        {
            GameObject tile = new GameObject($"Tile_{row}_{column}");

            SpriteRenderer renderer = tile.AddComponent<SpriteRenderer>();
            renderer.sprite = Resources.Load<Sprite>("Sprites/tile");

            BoxCollider2D collider = tile.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(1.0f, 1.0f);//this is being auto-scaled by transform.localScale, it's absolutely beautiful

            //start assembling board 3 world units below and left of pivot, so board is in the center
            float posX = column * sizeModifier - 3.0f;
            float posY = row * sizeModifier - 3.0f;
            tile.transform.position = new Vector3(posX, posY, 0);
            tile.transform.localScale = new Vector3(sizeModifier, sizeModifier, 1.0f);

            //add actual tile-specific behaviors
            Tile tileComponent = tile.AddComponent<Tile>();

            //tileComponent.board = this;
            tileComponent.defaultColor = renderer.color;
            tileComponent.column = column;
            tileComponent.row = row;

            return tile;
        }
    }


