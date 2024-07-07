using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : GemFlipClickable
{
    public GameBoard board;         // Reference to the GameBoard
    public Color defaultColor;      // Default color of the tile
    private SpriteRenderer renderer;// Reference to the SpriteRenderer component
    public Sprite tileSprite; // Assign your tile texture here

    void Start()
    {
          renderer  = GetComponent<SpriteRenderer>();                 
    }

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


    public void intializeTile(GameBoard gb )
    {

    }
            
    override public void onClick()
    {
        Debug.Log("Tile Clicked");
    }

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
