using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unifier : Bloc{
    public Vector3 outputColor;

     public override InpData UpdateInput(InpData inp){
        InpData new_inp = new InpData(inp.orientation,(int)outputColor.x,(int)outputColor.y,(int)outputColor.z, false);
        return new_inp;
     }
}
