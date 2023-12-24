using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Bloc
{
    public Vector3 color;

    public override InpData UpdateInput(InpData inp){
        return new InpData(inp.orientation, (int)inp.r, (int)inp.g, (int)inp.b, false,true);
    }

    void OnDrawGizmos()
    {
        //affichage de l'orientation avec un trais et qui respecte la rotation du bloc
        // 0 - top , 1 - right , 2 - bottom , 3 - left
        Gizmos.color = Color.red;
        Vector3 offset = new Vector3(+.5f, -.5f, 0);
        switch(orientation){
            case 0:
                Gizmos.DrawLine(transform.position + offset, transform.position + offset + new Vector3(0,1,0));
                break;
            case 1:
                Gizmos.DrawLine(transform.position + offset, transform.position + offset + new Vector3(1,0,0));
                break;
            case 2:
                Gizmos.DrawLine(transform.position + offset, transform.position + offset + new Vector3(0,-1,0));
                break;
            case 3:
                Gizmos.DrawLine(transform.position + offset, transform.position + offset + new Vector3(-1,0,0));
                break;
        }
        
       
    }
}
