using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour{

    [HideInInspector]
    public bool win = false;
    [HideInInspector]
    public bool finished = false;
    public float winDelay = 1f;

    public IEnumerator Win(){
        if(GetComponent<SandboxManager>().sandboxMode){
            //on arrete la fonction
            yield break;
        }
        finished = true;
        yield return new WaitForSeconds(winDelay);
        win = true;
        UpdateData();
    }

    void UpdateData(){
        if(win && !GetComponent<SandboxManager>().sandboxMode){
            GuiManager.instance.Open("win");
            //si le niveau a été finit pour la première fois
            if(!Keep.instance.finished_codes.Contains(Keep.instance.LevelCode)){
                Keep.instance.finished_codes.Add(Keep.instance.LevelCode);
                Keep.instance.starCount += Keep.instance.Stars;
            }
            SaveSystem.SavePlayer(Keep.instance);
        }
    }

    public void IsWin(){
        if(GetComponent<GridManager>().IsWin() && !GetComponent<TouchManager>().isMoving){
            StartCoroutine(Win());
        }
    }

    public void Home(){
        //on reset keep
        GameObject.Find("Keep").GetComponent<Keep>().SandBoxMode = false;
        GameObject.Find("Keep").GetComponent<Keep>().LevelCode = "";
        SceneManager.LoadScene(0);
    }

}
