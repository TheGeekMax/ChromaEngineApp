using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class guiData{
    public GameObject Element;
    public string name;
    public bool closable = true;
    public enum direction{
        left,
        right,
        up,
        down
    };
    public direction dir;
}
