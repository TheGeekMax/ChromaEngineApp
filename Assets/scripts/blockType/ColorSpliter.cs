using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSpliter : Bloc{
    public override InpData UpdateInput(InpData inp){
        if(inp.orientation != (orientation+2)%4){
            return new InpData(inp.orientation, 0, 0, 0, false, true);
        }
        //etape 0 on regarde le nombre de place qu'il nous faut
        int nb = 0;
        if(inp.r > 0){
            //on regarde dans les hist inp si on a pas deja un laser rouge
            bool found = false;
            foreach(InpData inp_data in inp_history){
                if(inp_data.r > 0){
                    found = true;
                }
            }
            if(!found){
                nb++;
                inp_history.Add(new InpData(orientation, 1, 0, 0, false, true));
            }else{
                inp.r = 0;
            }
        }
        if(inp.g > 0){
            //on regarde dans les hist inp si on a pas deja un laser vert
            bool found = false;
            foreach(InpData inp_data in inp_history){
                if(inp_data.g > 0){
                    found = true;
                }
            }
            if(!found){
                nb++;
                inp_history.Add(new InpData(orientation, 0, 1, 0, false, true));
            }else{
                inp.g = 0;
            }
        }
        if(inp.b > 0){
            //on regarde dans les hist inp si on a pas deja un laser bleu
            bool found = false;
            foreach(InpData inp_data in inp_history){
                if(inp_data.b > 0){
                    found = true;
                }
            }
            if(!found){
                nb++;
                inp_history.Add(new InpData(orientation, 0, 0, 1, false, true));
            }else{
                inp.b = 0;
            }
        }

        //on s'occupe d'initier le generateur
        InpData new_inp;
        if(nb == 0){
            new_inp = new InpData(orientation, 0, 0, 0, false,true);
        }else if(nb == 1){
            if(inp.r > 0){
                new_inp = new InpData((orientation+2)%4, inp.r, 0, 0, false);
            }else if(inp.g > 0){
                new_inp = new InpData((orientation+3)%4, 0, inp.g, 0, false);
            }else{
                new_inp = new InpData((orientation+1)%4, 0, 0, inp.b, false);
            }
        }else{
            if(inp.r > 0){
                new_inp = new InpData((orientation+2)%4, inp.r, 0, 0, false);
                new_inp.InitGenerator(nb-1);
                int index = 0;
                if(inp.g > 0){
                    new_inp.AddGenerated(index, new InpData((orientation+3)%4, 0, inp.g, 0, false));
                    index++;
                }
                if(inp.b > 0){
                    new_inp.AddGenerated(index, new InpData((orientation+1)%4, 0, 0, inp.b, false));
                    index++;
                }
            }else if(inp.g > 0){
                new_inp = new InpData((orientation+3)%4, 0, inp.g, 0, false);
                new_inp.InitGenerator(nb-1);
                int index = 0;
                if(inp.r > 0){
                    new_inp.AddGenerated(index, new InpData((orientation+2)%4, inp.r, 0, 0, false));
                    index++;
                }
                if(inp.b > 0){
                    new_inp.AddGenerated(index, new InpData((orientation+1)%4, 0, 0, inp.b, false));
                    index++;
                }
            }else{
                new_inp = new InpData((orientation+1)%4, 0, 0, inp.b, false);
                new_inp.InitGenerator(nb-1);
                int index = 0;
                if(inp.r > 0){
                    new_inp.AddGenerated(index, new InpData((orientation+2)%4, inp.r, 0, 0, false));
                    index++;
                }
                if(inp.g > 0){
                    new_inp.AddGenerated(index, new InpData((orientation+1)%4, 0, inp.g, 0, false));
                    index++;
                }
            }
        }

        return new_inp;
    }
}
