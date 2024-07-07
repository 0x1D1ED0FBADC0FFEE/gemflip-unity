using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class ClickManager : MonoBehaviour
{
    void Update()
    {
        // Detect mouse button down event
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }

    void HandleClick()
    {
        // Cast a ray from the camera to the mouse position
        //Vector3 mousepos = Input.mousePosition;
        //mousepos.z = -10;
        Vector2 rayPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(rayPosition, Vector2.zero);

        Debug.Log($"Mouse Position: {Input.mousePosition}");
        Debug.Log($"Number of Hits: {hits.Length}");

        // Process all hits along the ray's path
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                GemFlipClickable clickable = hit.collider.GetComponent<GemFlipClickable>();
    
                    if (IsTransparent(hit))
                    {
                        //skip this hit if pixel was transparent
                        continue;
                    }
                        // Trigger the click event on the first non-transparent object
                        clickable.onClick();
                        return;
            }
        }
        else
        {
            // Handle the case where no clickable object was hit
            Debug.Log("Clicked on empty space");
        }
    }

    //See if clicked pixel is transparent
    bool IsTransparent(RaycastHit2D hit)
    {
        SpriteRenderer renderer = hit.collider.GetComponent<SpriteRenderer>();
        if (renderer != null && renderer.material.HasProperty("_MainTex"))
        {
            Texture2D texture = renderer.sprite.texture;
            Vector2 uv = GetUVCoordinates(hit, texture);
            UnityEngine.Color color = texture.GetPixelBilinear(uv.x, uv.y);
            return color.a < 0.1f; //arbitrary threshold, should work in this context
        }
        return false;
    }

    Vector2 GetUVCoordinates(RaycastHit2D hit, Texture2D texture)
    {
        Bounds bounds = hit.collider.bounds;
        Vector2 localHitPoint = hit.point - (Vector2)bounds.min;
        return new Vector2(localHitPoint.x / bounds.size.x, localHitPoint.y / bounds.size.y);
    }
}
