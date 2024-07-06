using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserColour : MonoBehaviour{
    public GameObject obj;

    public Sprite sprite_or0;
    public Sprite sprite_or1;
    public Sprite sprite_or2;
    public Sprite sprite_or3;

    Generator LaserOfParent;

    void Start(){
        LaserOfParent = GetComponentInParent<Generator>();
    }

    void Update()
    {
        obj.GetComponent<SpriteRenderer>().color = new Color(LaserOfParent.color.x/255f, LaserOfParent.color.y/255f, LaserOfParent.color.z/255f,.7f);
        switch(LaserOfParent.orientation){
            case 0:
                obj.GetComponent<SpriteRenderer>().sprite = sprite_or0;
                break;
            case 1:
                obj.GetComponent<SpriteRenderer>().sprite = sprite_or1;
                break;
            case 2:
                obj.GetComponent<SpriteRenderer>().sprite = sprite_or2;
                break;
            case 3:
                obj.GetComponent<SpriteRenderer>().sprite = sprite_or3;
                break;
        }
    }
}
