using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColouredBallon : Bloc{
    bool cooldown = false;
    int cooldownTime = 20;

    public Vector3 color;

    void Awake(){
        //on met le sprite en noir
        inp_history = new List<InpData>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer == null){
            return;
        }
        spriteRenderer.color = new Color(color.x/255,color.y/255,color.z/255,.8f);
    }

    void Update(){
        if(cooldown){
            cooldownTime--;
            if(cooldownTime == 0){
                GridManager.instance.RemoveblocWithExplosion(gameObject,new Vector3(color.x/255,color.y/255,color.z/255));
            }
        }
    }

    public override InpData UpdateInput(InpData inp){
        if(inp.r == color.x && inp.g == color.y && inp.b == color.z){
            if(!SandboxManager.instance.sandboxMode){
                cooldown = true;
            }
            return new InpData(inp.orientation,inp.r,inp.g,inp.b, false , true);
        }else{
            if(!SandboxManager.instance.sandboxMode){
                cooldown = false;
                cooldownTime = 20;
            }
            return new InpData(inp.orientation,inp.r,inp.g,inp.b, false , true);
        }
    }
}
