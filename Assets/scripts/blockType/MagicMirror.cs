using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMirror : Bloc{

     public override InpData UpdateInput(InpData inp){
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

        InpData new_inp = new InpData(inp.orientation, r,g,b, false);
        //on ajoute 2 laser qui partent dans la direction de l'orientation +1 pour l'un et -1 pour l'autre
        new_inp.InitGenerator(2);
        new_inp.AddGenerated(0, new InpData((inp.orientation + 1) %4, r,g,b, false));
        new_inp.AddGenerated(1, new InpData((inp.orientation + 3) %4, r, g, b, false));
        return new_inp;
    }
}
