using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfMagicMirror : Bloc{

     public override InpData UpdateInput(InpData inp){
        //on verifie si l'entrée est bien dans la bonne direction (si on fonce pas dans orient +2)
        if(inp.orientation == orientation){
            return new InpData(inp.orientation, 0, 0, 0, false, true);
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

        bool[] whoToActivate = {true, true, true, true};
        whoToActivate[(orientation+2)%4] = false;
        //celui ou le laser arrive
        whoToActivate[(inp.orientation+2) %4] = false;

        int or1 = 0, or2 = 0;
        bool switcher = false;
        for(int i = 0; i < 4; i++){
            if(whoToActivate[i]){
                if(switcher){
                    or2 = i;
                    switcher = false;
                }else{
                    or1 = i;
                    switcher = true;
                }
            }
        }

        InpData new_inp = new InpData(or1, r,g,b, false);
        //on ajoute 2 laser qui partent dans la direction de l'orientation +1 pour l'un et -1 pour l'autre
        new_inp.InitGenerator(1);
        new_inp.AddGenerated(0, new InpData(or2, r, g, b, false));
        return new_inp;
    }
}