using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//import for field info
using System.Reflection;

public class Laser : MonoBehaviour
{
    public Vector3 color;
    public GameObject light;
    public int layerOrder;
    public float intensity;
    public float fallOff;

    private static FieldInfo m_FalloffField =  typeof( UnityEngine.Rendering.Universal.Light2D ).GetField( "m_FalloffIntensity", BindingFlags.NonPublic | BindingFlags.Instance );


    public void SetColor(int r, int g, int b){
        this.color = new Vector3(r,g,b);
        UpdateSprite();
    }

    public void SetOrder(int order){
        this.layerOrder = order;
        UpdateSprite();
    }

    void UpdateSprite(){
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer == null){
            return;
        }
        spriteRenderer.color = new Color(color.x/255f, color.y/255f, color.z/255f,.7f);
        //on change la couleur du light2D
        UnityEngine.Rendering.Universal.Light2D light2D = light.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        light2D.color = new Color(color.x/255f, color.y/255f, color.z/255f,1f);
        light2D.intensity = intensity;
        m_FalloffField.SetValue( light2D, fallOff );
    }
}
