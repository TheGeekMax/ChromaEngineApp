using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndGate : Bloc{
    List<int> inputs = new List<int>();

    void Awake(){
       inp_history = new List<InpData>();
    }

    

    public override void LaserReset(){
        inp_history.Clear();
        inputs.Clear();
    }
    
    public override InpData UpdateInput(InpData inp){
        if(inp.orientation == orientation || inp.orientation == (orientation + 2) % 4){
            return new InpData(inp.orientation,0,0,0, false, true);
        }
        //on ajoute a l'historique
        inp_history.Add(inp);
        //on ajoute a la liste des inputs
        
        if(inp_history.Count == 1 || (inp_history.Count == 2 && inputs.Contains(inp.orientation))){
            return new InpData(inp.orientation,0,0,0, false, true);
        }
        inputs.Add(inp.orientation);
        return new InpData(orientation,255,255,255, false, false);
    }
}
