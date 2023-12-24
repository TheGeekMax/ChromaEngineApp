using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulb : Bloc
{   
    public Sprite on;
    public GameObject light;
    
    public override void UpdateSprite(){
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer == null){
            return;
        }
        if(GetComponent<WinObject>().isWin){
            spriteRenderer.sprite = on;
            light.SetActive(true);
        }else{
            //on met celuis de l'or 0;
            spriteRenderer.sprite = or0;
            light.SetActive(false);
        }
    }

    public override InpData UpdateInput(InpData inp){
        GetComponent<WinObject>().isWin = true;
        UpdateSprite();
        return new InpData(inp.orientation, 255, 255, 255, false, true);
    }

    public override void LaserReset(){
        GetComponent<WinObject>().isWin = false;
        UpdateSprite();
    }
}
