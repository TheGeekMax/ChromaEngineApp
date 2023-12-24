using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject Camera;
    public GameObject background;

    int gridwidth;
    SizeData sizeData;

    [Header("contour")]
    public GameObject contour_tl;
    public GameObject contour_tr;
    public GameObject contour_bl;
    public GameObject contour_br;

    [Header("bord")]
    public GameObject bord_t;
    public GameObject bord_r;
    public GameObject bord_b;
    public GameObject bord_l;

    public void Init(){
         //etape 1 : on recupere la taille de la grille
        gridwidth = GetComponent<GridManager>().gridWidth;
        sizeData = GetComponent<GridManager>().sizeData;

        //etape 2 : on adapte le background en changeant le tile size puis on le positionne
        //celuis de Sprite renderer
        background.GetComponent<SpriteRenderer>().size = new Vector2(gridwidth,gridwidth);
        background.transform.position = new Vector3(-gridwidth/2,gridwidth/2,0);

        //etape 3 : on adapte la camera en changeant la taille de la zone de rendu puis on la positionne
        Camera.GetComponent<Camera>().orthographicSize = gridwidth+2;
        Camera.transform.position = new Vector3(0,gridwidth/2,-10);

        //etape 4 : placement des contours
        contour_tl.transform.position = new Vector3(-gridwidth/2-.5f,gridwidth/2+.5f,0);
        contour_tr.transform.position = new Vector3(gridwidth/2+.5f,gridwidth/2+.5f,0);
        contour_bl.transform.position = new Vector3(-gridwidth/2-.5f,-gridwidth/2-.5f,0);
        contour_br.transform.position = new Vector3(gridwidth/2+.5f,-gridwidth/2-.5f,0);

        //etape 5 : placement des bords
        bord_t.transform.position = new Vector3(0,gridwidth/2+.5f,0);
        bord_r.transform.position = new Vector3(gridwidth/2+.5f,0,0);
        bord_b.transform.position = new Vector3(0,-gridwidth/2-.5f,0);
        bord_l.transform.position = new Vector3(-gridwidth/2-.5f,0,0);

        //etape 5.bis : on adapte la taille des bords
        bord_t.GetComponent<SpriteRenderer>().size = new Vector2(gridwidth,1);
        bord_r.GetComponent<SpriteRenderer>().size = new Vector2(1,gridwidth);
        bord_b.GetComponent<SpriteRenderer>().size = new Vector2(gridwidth,1);
        bord_l.GetComponent<SpriteRenderer>().size = new Vector2(1,gridwidth);
    }
}
