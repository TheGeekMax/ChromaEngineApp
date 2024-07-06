using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GeneratorData{
    public Vector2 position;
    public int orientation;
    public Vector3 color;
    public int lifespan;
    public int layerOrder;
    public static int layerOrderOffset = 6;

    public List<Vector2> path;

    public GeneratorData(Vector2 position, int orientation, Vector3 color){
        this.position = position;
        this.orientation = orientation;
        this.color = color;
        this.lifespan = 50;
        this.layerOrder = layerOrderOffset;
        layerOrderOffset++;
        this.path = new List<Vector2>();
    }

    public InpData ToInpData(){
        return new InpData(orientation, (int)color.x, (int)color.y, (int)color.z);
    }

    public int Count(Vector2 position){
        int count = 0;
        foreach(Vector2 p in path){
            if(p == position){
                count++;
            }
        }
        return count;
    }
}
