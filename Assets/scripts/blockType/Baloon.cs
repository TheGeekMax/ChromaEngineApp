using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baloon : Bloc{
    bool cooldown = false;
    int cooldownTime = 20;

    void Awake(){
        //on met le sprite en noir
        inp_history = new List<InpData>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer == null){
            return;
        }
        spriteRenderer.color = new Color(.5f,.5f,.5f,.8f);
    }

    void Update(){
        if(cooldown){
            cooldownTime--;
            if(cooldownTime == 0){
                GridManager.instance.RemoveblocWithExplosion(gameObject,new Vector3(.5f,.5f,.5f));
            }
        }
    }

    public override InpData UpdateInput(InpData inp){
        if(!SandboxManager.instance.sandboxMode){
            cooldown = true;
        }
        return new InpData(inp.orientation,inp.r,inp.g,inp.b, false , true);
    }
}
