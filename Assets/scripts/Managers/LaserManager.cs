using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{

    //private GridManager gridManager = GetComponent<GridManager>();
    public GameObject parent;
    [Header("Prefabs")]
    public GameObject verticalLaser;
    public GameObject horizontalLaser;
    public GameObject corner0to1Laser;
    public GameObject corner1to2Laser;
    public GameObject corner2to3Laser;
    public GameObject corner3to0Laser;
    
    //creation des 4 tableaux pour les 4 directions
    GameObject[,] topLaser;
    GameObject[,] rightLaser;
    GameObject[,] bottomLaser;
    GameObject[,] leftLaser;

    public static LaserManager instance;
    
    void Awake(){
        instance = this;
    }

    public void Init(){
        //get grid width
        int gridWidth = GetComponent<GridManager>().gridWidth;

        topLaser = new GameObject[gridWidth, gridWidth];
        rightLaser = new GameObject[gridWidth, gridWidth];
        bottomLaser = new GameObject[gridWidth, gridWidth];
        leftLaser = new GameObject[gridWidth, gridWidth];
    }

    public void AddLaser(int beginOr,int endOr,int x, int y,Vector3 color,int order){
        //etape 1 : obtention du bon prefab en fonction de l'orientation
        GameObject prefab = null;
             if ((beginOr == 2 && endOr == 1) || (beginOr == 3 && endOr == 0)){
            prefab = corner0to1Laser;
        }
        else if ((beginOr == 3 && endOr == 2) || (beginOr == 0 && endOr == 1)){
            prefab = corner1to2Laser;
        }
        else if ((beginOr == 0 && endOr == 3) || (beginOr == 1 && endOr == 2)){
            prefab = corner2to3Laser;
        }
        else if ((beginOr == 1 && endOr == 0) || (beginOr == 2 && endOr == 3)){
            prefab = corner3to0Laser;
        }
        else if ((beginOr == 0 && endOr == 2) || (beginOr == 2 && endOr == 0) || (beginOr == 0 && endOr == 0) || (beginOr == 2 && endOr == 2)){
            prefab = verticalLaser;
        }
        else if ((beginOr == 1 && endOr == 3) || (beginOr == 3 && endOr == 1) || (beginOr == 1 && endOr == 1) || (beginOr == 3 && endOr == 3)){
            prefab = horizontalLaser;
        }
        else{
            Debug.Log("Erreur : orientation de depart et d'arrivee identiques");
            return;
        }

        //etape 2 : instanciation du prefab
        GameObject newLaser = Instantiate(prefab, new Vector3((x-GetComponent<GridManager>().gridWidth/2), (GetComponent<GridManager>().gridWidth/2)-y, 0), Quaternion.identity);
        newLaser.transform.parent = parent.transform;
        newLaser.GetComponent<Laser>().SetColor((int)color.x, (int)color.y, (int)color.z);
        newLaser.GetComponent<Laser>().SetOrder(order);
        newLaser.GetComponent<SpriteRenderer>().sortingOrder = order;


        //etape 3 : ajout du prefab au tableau correspondant
        if (beginOr == 0){
            //definie sa priorité a 0.1
            if(topLaser[x,y] != null){
                Destroy(topLaser[x,y]);
            }
            topLaser[x, y] = newLaser;
        }
        else if (beginOr == 1){
            //definie sa priorité a 0.2
            if(rightLaser[x,y] != null){
                Destroy(rightLaser[x,y]);
            }
            rightLaser[x, y] = newLaser;
        }
        else if (beginOr == 2){
            //definie sa priorité a 0.3
            
            if(bottomLaser[x,y] != null){
                Destroy(bottomLaser[x,y]);
            }
            bottomLaser[x, y] = newLaser;
        }
        else if (beginOr == 3){
            //definie sa priorité a 0.4
            if(leftLaser[x,y] != null){
                Destroy(leftLaser[x,y]);
            }
            leftLaser[x, y] = newLaser;
        }
    }

    public void DestroyLasers(){
        int gw = GetComponent<GridManager>().gridWidth;
        //etape1 : destruction des lasers
        for (int i = 0; i < gw; i++)
        {
            for (int j = 0; j < gw; j++)
            {
                if (topLaser[i, j] != null){
                    Destroy(topLaser[i, j]);
                }
                if (rightLaser[i, j] != null){
                    Destroy(rightLaser[i, j]);
                }
                if (bottomLaser[i, j] != null){
                    Destroy(bottomLaser[i, j]);
                }
                if (leftLaser[i, j] != null){
                    Destroy(leftLaser[i, j]);
                }

                GameObject current = GetComponent<GridManager>().GetBlocObject(i,j);
                if(current != null && !(current.GetComponent<Bloc>() is Generator)){
                    // Debug.Log("current : " + i + " " + j);
                    // Debug.Log("this is an" + current.GetComponent<Bloc>() );
                    current.GetComponent<Bloc>().LaserReset();
                }
            }
        }
        //etape2 : reinitialisation des tableaux
        topLaser = new GameObject[gw, gw];
        rightLaser = new GameObject[gw, gw];
        bottomLaser = new GameObject[gw, gw];
        leftLaser = new GameObject[gw, gw];

        GeneratorData.layerOrderOffset = 6;
    }

    public void GenerateLasers(){
        //etape -1 : destruction des lasers
        DestroyLasers();

        GridManager gridManager = GetComponent<GridManager>();

        //etape 0 creation des GeneratorData a partir des generateurs
        List<GeneratorData> generatorList = gridManager.GetGeneratorList();

        if(generatorList == null){
            return;
        }

        int dw = gridManager.gridWidth;

        //etape 0 bis on les avances d'une case
        for (int i = 0; i < generatorList.Count; i++){
            switch(generatorList[i].orientation){
                case 0:
                    generatorList[i].position += new Vector2(0, -1);
                    break;
                case 1:
                    generatorList[i].position += new Vector2(1, 0);
                    break;
                case 2:
                    generatorList[i].position += new Vector2(0, 1);
                    break;
                case 3:
                    generatorList[i].position += new Vector2(-1, 0);
                    break;
                default:
                    return;
            }
        }

        while(generatorList.Count > 0){
            //etape 1 on suprime les GeneratorData inutiles (OOB,ou dans une case deja passé)
            for (int i = 0; i < generatorList.Count; i++){
                if (generatorList[i].position.x < 0 || generatorList[i].position.x >= gridManager.gridWidth || generatorList[i].position.y < 0 || generatorList[i].position.y >= gridManager.gridWidth || generatorList[i].Count(generatorList[i].position) >= 2){
                    generatorList.RemoveAt(i);
                    i--;
                }
            }


            List<GeneratorData> added = new List<GeneratorData>();
            for (int i = 0; i < generatorList.Count; i++){
                //etape 2 on place le laser a l'emplacement du node si vide
                if (gridManager.IsEmpty((int)generatorList[i].position.x, (int)generatorList[i].position.y)){
                    AddLaser(generatorList[i].orientation, (generatorList[i].orientation+2)%4, (int)generatorList[i].position.x, (int)generatorList[i].position.y, generatorList[i].color,generatorList[i].layerOrder);
                }else{ //etape 3 si non vide on execute la fonction de l'objet

                    //execute la fonction de l'objet
                    Bloc bloc = gridManager.GetBloc((int)generatorList[i].position.x, (int)generatorList[i].position.y);
                    InpData new_inp = bloc.UpdateInput(generatorList[i].ToInpData());
                    if (!new_inp.destroy){
                        
                        //on actualise la valeur de la node
                        if(new_inp.show){
                        AddLaser(generatorList[i].orientation, new_inp.orientation, (int)generatorList[i].position.x, (int)generatorList[i].position.y, new Vector3(new_inp.r, new_inp.g, new_inp.b),generatorList[i].layerOrder);
                        }

                        generatorList[i].orientation = new_inp.orientation;
                        generatorList[i].color = new Vector3(new_inp.r, new_inp.g, new_inp.b);
                        generatorList[i].lifespan -= 1;

                        //partie generation des nouvelles node des lasers
                        InpData[] toAdd = new_inp.generated;
                        if(toAdd != null){
                            for(int j = 0; j < toAdd.Length; j ++){
                                GeneratorData temp =new GeneratorData( generatorList[i].position, toAdd[j].orientation, new Vector3(toAdd[j].r, toAdd[j].g, toAdd[j].b));
                                temp.lifespan = generatorList[i].lifespan;
                                added.Add(temp);
                            }
                        }
                    }
                    else{
                        generatorList.RemoveAt(i);
                        i--;
                    }
                }
            }
            //on ajoute les elts de added a generatorList
            for (int i = 0; i < added.Count; i++){
                generatorList.Add(added[i]);
            }

            //etape 3 bis : on detruit les GeneratorData qui ont une durée de vie de 0
            for (int i = 0; i < generatorList.Count; i++){
                if (generatorList[i].lifespan <= 0){
                    generatorList.RemoveAt(i);
                    i--;
                }
            }

            //etape 4 on on avance les nodes d'une case
            for (int i = 0; i < generatorList.Count; i++){
                generatorList[i].path.Add(generatorList[i].position);
                switch(generatorList[i].orientation){
                    case 0:
                        generatorList[i].position += new Vector2(0, -1);
                        break;
                    case 1:
                        generatorList[i].position += new Vector2(1, 0);
                        break;
                    case 2:
                        generatorList[i].position += new Vector2(0, 1);
                        break;
                    case 3:
                        generatorList[i].position += new Vector2(-1, 0);
                        break;
                }
            }

            //etape 5 on decremente la durée de vie des nodes
            for (int i = 0; i < generatorList.Count; i++){
                generatorList[i].lifespan--;
            }
        }
        GetComponent<WinManager>().IsWin();
    }
}
