using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrGate : Bloc{
    public override InpData UpdateInput(InpData inp){
        if(inp.orientation == orientation || inp.orientation == (orientation + 2) % 4){
            return new InpData(inp.orientation,0,0,0, false, true);
        }
        //on ajoute a l'historique
        inp_history.Add(inp);

        if(inp_history.Count == 1){
            return new InpData(orientation,255,255,255, false, false);
        }
        return new InpData(inp.orientation,0,0,0, false, true);
    }
}
