using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Singleton, game state logic is currently managed by the GameBoard
//may add separate class for that later on

    public class GameBoard : MonoBehaviour
    { 
    //public Sprite tileSprite;
    public int rows {get; private set;}
    public int columns { get; private set;}
    public float tileSize;
    public float tileSizeModifier;
    public int boardsize;
    public static GameBoard instance;
    public int barriers_left;
    public int ethers_left;
    public bool placed_drone;
    public bool placed_barrier;
    public bool placed_ether;

    public void Awake()
    {
        Initialize();
        Debug.Log("gameboard instance made");
    }
     
    private void Initialize()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
           
        else
            Destroy(gameObject);
    }

    public void Start()
    {
        
        placed_barrier = false;
        placed_ether = false;
        placed_drone = false;

        ethers_left = 5;
        barriers_left = 5;

        //boardsize will be read from user settings later on
        boardsize = 7;
        rows = columns = boardsize;
        tileSizeModifier = (7.0f / boardsize);//assuming that we have 7 x 7 tiles by default, the size of tiles needs to be adjusted based on given borad size to get the same size board
        tileSize = 1.0f * tileSizeModifier; //redundant if tilesize base value is 1.0f world units, but kept it so it can be changed

        GameObjectFactory.Initialize(tileSizeModifier);
        GenerateBoard();
        GenerateButtons();
    }

    void GenerateBoard()
    {
        Debug.Log("generating board");
        //board = new GameObject("Board");

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {

                //Debug.Log("RETURNED " + x + " " + y);

                GameObject tile = GameObjectFactory.CreateTile(row, column);
                tile.transform.SetParent(gameObject.transform);

                //place gems on the central diagonal
                if (row > 1 && row < boardsize - 2 && column == row)
                {
                    GameObject newgem = GameObjectFactory.CreateGem(0, row, column);
                    newgem.transform.SetParent(tile.transform);
                    newgem.transform.localPosition = new Vector3(0, 0, -1);//can't set this in factory, needs to be added to parent before

                }
            }
        }
        /** //debugging stuff here
        GameObject t = getTile(4, 4).transform.Find("Gem").gameObject;

        moveGameObjectToTile(t, 5, 5);

        t = getTile(2, 2).transform.Find("Gem").gameObject;

        moveGameObjectToTile(t, 5, 5);

        t = getTile(3, 3).transform.Find("Gem").gameObject;

        moveGameObjectToTile(t, 5, 5);**/

    }

    //these additional methods just make the code look somewhat cleaner
    //because then I don't have to do it all in one place
    void GenerateButtons()
    {
        GameObject barrierButton = new GameObject("BarrierButton");
        makeButton(barrierButton);
        barrierButton.transform.SetParent(gameObject.transform);

        GameObject etherButton = new GameObject("EtherButton");
        makeButton(etherButton);
        etherButton.transform.SetParent(gameObject.transform);

        GameObject commitButton = new GameObject("CommitButton");
        makeButton(commitButton);
        commitButton.transform.SetParent(gameObject.transform);

        GameObject rotationButton = new GameObject("RotationButton");
        makeButton(rotationButton);
        rotationButton.transform.SetParent(gameObject.transform);

    }

    void makeButton(GameObject btn) 
    {

        btn.transform.SetParent(gameObject.transform);
        SpriteRenderer renderer = btn.AddComponent<SpriteRenderer>();      
        BoxCollider2D collider = btn.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(1.0f, 1.0f);

        switch (btn.name)
        {
            case "BarrierButton":
                renderer.sprite = Resources.Load<Sprite>("Sprites/tile_barrier");
                btn.transform.position = new Vector3(5.0f, 3.0f, 0);
                btn.AddComponent<BarrButton>();
                return;

            case "EtherButton":
                renderer.sprite = Resources.Load<Sprite>("Sprites/tile_eth");
                btn.transform.position = new Vector3(6.0f, 3.0f, 0);
                btn.AddComponent<EthButton>();
                return;

            case "CommitButton":
                renderer.sprite = Resources.Load<Sprite>("Sprites/commit_turn");
                btn.transform.position = new Vector3(5.0f, 2.0f, 0);
                btn.AddComponent<CommitButton>();
                return;

            case "RotationButton":
                renderer.sprite = Resources.Load<Sprite>("Sprites/tile");
                btn.transform.position = new Vector3(6.0f, 2.0f, 0);
                btn.AddComponent<RotationButton>();

                GameObject arrow = new GameObject("Arrow");
                SpriteRenderer arrowSprite = arrow.AddComponent<SpriteRenderer>();
                arrowSprite.sprite = Resources.Load<Sprite>("Sprites/south_arrow");

                arrow.transform.SetParent(btn.transform);
                arrow.transform.localPosition = new Vector3(0, 0, -1);

                return;
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

    public PlayerSelections GetPlayerSelections()
    {
            //turning some booleans into ints here, because later on, I will use 
            //the player selections for the single player AI sub-project 
        Debug.Log("get player selections");
        int phase = transform.Find("EtherButton").GetComponent<EthButton>().eth_active ? Constants.ETHER : Constants.SOLID; 
        int barrier = transform.Find("BarrierButton").GetComponent<BarrButton>().barrier_active ? 1 : 0;
        
        //this command pulls the rotation of the arrow on top of the direction button. 
        //was able to find the intuitive set of angle values by using the debugger, it was in localEulerAngles
        int direction = transform.Find("RotationButton").GetComponent<RotationButton>().transform.Find("Arrow").localEulerAngles.z switch
        {
             0f => Constants.SOUTH,
             90f => Constants.WEST,
             180f => Constants.NORTH,
             270f => Constants.EAST
        };

        return new PlayerSelections(phase, Constants.RED, barrier, direction);
    }

    public GameObject getTile(int row, int column) 
    {
        return gameObject.transform.Find($"Tile_{row}_{column}").gameObject;
    }

}
