using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents input data for a laser game.
/// </summary>

public class InpData{
    public int orientation;
    public int r,g,b;
    public bool show;

    public InpData[] generated;
    public bool destroy;

    public InpData(int orientation, int r, int g, int b, bool show,bool destroy){
        this.orientation = orientation;
        this.r = r;
        this.g = g;
        this.b = b;
        this.show = show;
        this.destroy = destroy;
    }

    public InpData(int orientation, int r, int g, int b, bool show){
        this.orientation = orientation;
        this.r = r;
        this.g = g;
        this.b = b;
        this.show = show;
        this.destroy = false;
    }

    public void InitGenerator(int count){
        generated = new InpData[count];
    }

    public void AddGenerated(int index, InpData inpData){
        generated[index] = inpData;
    }

    // cas ou osef
    public InpData(int orientation, int r, int g, int b){
        this.orientation = orientation;
        this.r = r;
        this.g = g;
        this.b = b;
        this.show = false;
    }

    public int getOrientation(){
        return orientation;
    }

    public Vector3 getColor(){
        return new Vector3(r,g,b);
    }

    public bool IsShown(){
        return show;
    }
}
