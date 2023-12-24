using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adder : Bloc{
    public override InpData UpdateInput(InpData inp){
        if(inp.orientation == (orientation + 2) % 4){
            //si le laser passe par la sortie, on le detruit
            return new InpData(inp.orientation, inp.r, inp.g, inp.b, false,true);
        }
        //etape -1 : on ajoute l'entrée à l'historique
        inp_history.Add(inp);
        //etape 0 - on recupere l'historique des lasers et on fait une somme (avec un clamp a 0 et 255)
        int r = 0;
        int g = 0;
        int b = 0;
        foreach(InpData inp_data in inp_history){
            r += inp_data.r;
            g += inp_data.g;
            b += inp_data.b;
        }
        r = Mathf.Clamp(r, 0, 255);
        g = Mathf.Clamp(g, 0, 255);
        b = Mathf.Clamp(b, 0, 255);

        InpData new_inp = new InpData(orientation, r,g,b, false);
        return new_inp;
    }
}
