using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class Led : Bloc{
    public GameObject on;
    public GameObject light;
    public Vector3 color;
    
    public override void UpdateSprite(){
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer == null){
            return;
        }
        if(GetComponent<WinObject>().isWin){
            on.GetComponent<SpriteRenderer>().color = new Color(color.x/255f, color.y/255f, color.z/255f,1f);
            light.SetActive(true);
            UnityEngine.Rendering.Universal.Light2D light2D = light.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
            light2D.color = new Color(color.x/255f, color.y/255f, color.z/255f,1f);
        }else{
            //on met celuis de l'or 0;
            light.SetActive(false);
            on.GetComponent<SpriteRenderer>().color = new Color(color.x/255f, color.y/255f, color.z/255f,.3f);
        }
    }

    public override InpData UpdateInput(InpData inp){
        if(inp.r == (int)color.x && inp.g == (int)color.y && inp.b == (int)color.z){
            GetComponent<WinObject>().isWin = true;
        }else{
            GetComponent<WinObject>().isWin = false;
        }
        UpdateSprite();
        return new InpData(inp.orientation, 255, 255, 255, false, true);
    }

    public override void LaserReset(){
        GetComponent<WinObject>().isWin = false;
        UpdateSprite();
    }
}
