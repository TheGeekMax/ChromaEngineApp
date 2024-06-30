using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour{
    void Start(){
        KeepManager.instance.Initialize();
        LevelbuttonManager.instance.Initialize();
    }
}
