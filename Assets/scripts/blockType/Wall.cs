using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Bloc{

    public override InpData UpdateInput(InpData inp){
        return new InpData(inp.orientation, 0, 0, 0, false, true);
    }
}
