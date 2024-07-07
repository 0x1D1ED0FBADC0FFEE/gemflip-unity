using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//creates the GameObject and attaches all components
public static class GameObjectFactory
{
    public static float sizeModifier;
    public static GameObject CreateDrone(int color, int phase, int direction)
    {
        GameObject droneObject = new GameObject("Drone");
        Drone droneComponent = droneObject.AddComponent<Drone>();
        droneComponent.Initialize(color, phase, direction);
        return droneObject;
    }

    public static GameObject CreateBarrier(int phase)
    {
        GameObject barrierObject = new GameObject("Barrier");
        Barrier barrierComponent = barrierObject.AddComponent<Barrier>();
        barrierComponent.Initialize(phase);
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
        gemComponent.Initialize(color);

        return gemObject;
    }
}
