using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomPlay : MonoBehaviour{
    public TMP_InputField idText;

    public void Play(){
        if(idText.text != ""){
            Keep.instance.PlayCustomCode(idText.text);
        }
    }
    public void Edit(){
        if(idText.text != ""){
            Keep.instance.PlayCustomCodeSandBox(idText.text);
        }
    }
    public void Home(){
        //on load la scene d'indice 0 pour revenir au menu principal
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
