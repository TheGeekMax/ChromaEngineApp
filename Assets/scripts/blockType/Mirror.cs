using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : Bloc
{
    public override InpData UpdateInput(InpData inp){
        //on calcul la nouvelle orientation
        int newOr;
        switch(inp.orientation){
            case 2:
                if(orientation == 0||orientation == 2){
                    newOr = 3;
                }else{
                    newOr = 1;
                }
                break;
            case 3:
                if(orientation == 0||orientation == 2){
                    newOr = 2;
                }else{
                    newOr = 0;
                }
                break;
            case 0:
                if(orientation == 0||orientation == 2){
                    newOr = 1;
                }else{
                    newOr = 3;
                }
                break;
            case 1:
                if(orientation == 0||orientation == 2){
                    newOr = 0;
                }else{
                    newOr = 2;
                }
                break;
            default:
                newOr = (inp.orientation + 2) %4;
                break;
        }
        InpData new_inp = new InpData(newOr, inp.r, inp.g, inp.b, true);
        return new_inp;
    }
}
