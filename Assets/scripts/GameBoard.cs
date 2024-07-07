using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameBoard : MonoBehaviour
{ 
    //public Sprite tileSprite;
    public int rows;
    public int columns;
    public float tileSize;
    public float tileSizeModifier;
    public int boardsize;
    public GameObject board;


    void Start()
    {   
        //boardsize should be read from user settings later on
        boardsize = 7;
        rows = columns = boardsize;
        tileSizeModifier = (7.0f / boardsize);//assuming that we have 7 x 7 tiles by default, the size of tiles needs to be adjusted based on given borad size to get the same size board
        tileSize = 1.0f * tileSizeModifier; //redundant if tilesize base value is 1.0f world units, but kept it so it can be changed
        GameObjectFactory.sizeModifier = tileSizeModifier;
        GenerateBoard();
    }

    void GenerateBoard()
    {
        Debug.Log("generating board");
        board = new GameObject("Board");

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                
                //Debug.Log("RETURNED " + x + " " + y);
                GameObject tile = new GameObject($"Tile_{row}_{column}");

                tile.transform.SetParent(board.transform);

                SpriteRenderer renderer = tile.AddComponent<SpriteRenderer>();
                renderer.sprite = Resources.Load<Sprite>("Sprites/tile");

                BoxCollider2D collider = tile.AddComponent<BoxCollider2D>();
                collider.size = new Vector2(1.0f, 1.0f);//this is being auto-scaled by transform.localScale, it's absolutely beautiful

                //start assembling board 3 world units below and left of pivot, so board is in the center
                float posX = column * tileSize - 3.0f;
                float posY = row * tileSize  - 3.0f;
                tile.transform.position = new Vector3(posX, posY, 0);
                tile.transform.localScale = new Vector3(tileSize, tileSize, 1.0f);

                //add actual tile-specific behaviors
                Tile tileComponent = tile.AddComponent<Tile>();
                tileComponent.board = this;
                tileComponent.defaultColor = renderer.color;
                tileComponent.column = column;
                tileComponent.row = row;

                //place gems on the central diagonal
                if(row > 1 && row < boardsize - 2 && column == row)
                {
                    GameObject newgem = GameObjectFactory.CreateGem(0, row, column);
                    newgem.transform.SetParent(tile.transform);
                    newgem.transform.localPosition = new Vector3(0, 0, -1);
                    
                }
            }
        }
        /**
        GameObject t = getTile(4, 4).transform.Find("Gem").gameObject;

        moveGameObjectToTile(t, 5, 5);

        t = getTile(2, 2).transform.Find("Gem").gameObject;

        moveGameObjectToTile(t, 5, 5);

        t = getTile(3, 3).transform.Find("Gem").gameObject;

        moveGameObjectToTile(t, 5, 5);**/

    }

    //these additional methods just make the code look somewhat cleaner
    //because then I don't have to do it all in one place
    void generateButtons()
    {
        GameObject barrierButton = new GameObject("CommitButton");
        GameObject etherButton = new GameObject("EtherButton");
        GameObject commitButton = new GameObject("BarrierButton");
        GameObject rotationButton = new GameObject("RotationButton");


    }

    void makeButton(GameObject btn) 
    {
        switch (btn.name)
        {
            case "BarrierButton":
                break;
        }
    }

    //must preserve z coord when moving objects, so theyre still on the same "layer"
    //after that, check all objects with the same z coord to adjust their scales and arrangements
    //so they all remain visible and clickable
    public void moveGameObjectToTile(GameObject obj, int row, int column) 
    {
        GameObject targetTile = getTile(row, column);
        obj.transform.SetParent(targetTile.transform);
        obj.transform.localPosition = new Vector3(0,0, obj.transform.localPosition.z);

        List<Transform> matchingChildren = new List<Transform>();

        //find all children of the new parent that have the same z coord as the child we just added
        foreach(Transform c in obj.transform.parent)
        {   
            //the coords are floats which can have slight errors, so we use .Approximately to see if they're equal
            if(Mathf.Approximately(c.localPosition.z, obj.transform.localPosition.z))
                matchingChildren.Add(c);
        }

        int len = matchingChildren.Count;
        float scaleAdjust = (1.0f);

        if (len > 1)
            scaleAdjust = scaleAdjust / 2;

        //adjust local scale and position for all children with same z as the one added
        for (int i = 0; i < len; i++)
        {
            matchingChildren[i].transform.localScale = new Vector3(scaleAdjust, scaleAdjust, 1.0f);
            float placementStep = 1.0f/(len + 1);
            placementStep = i * placementStep - len/2 * placementStep;
            matchingChildren[i].localPosition = new Vector3(placementStep, matchingChildren[i].localPosition.y, matchingChildren[i].localPosition.z);

        }
    }

    public GameObject getTile(int row, int column) 
    {
        return board.transform.Find($"Tile_{row}_{column}").gameObject;
    }

}