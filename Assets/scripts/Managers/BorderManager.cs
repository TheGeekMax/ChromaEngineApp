using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BorderManager : MonoBehaviour{

    //utilise une tilemap comme affichage
    public Tilemap tilemapRenderer;
    //puis le tile a utiliser
    public Tile tile;


    bool[,] limitsTable;
    int gridwidth;
    SizeData sizeData;

    public void Init(){
        //attendre que le grid manager soit pret

        sizeData = GetComponent<GridManager>().sizeData;
        switch (sizeData)
        {
            case SizeData.Small_4x4:
                gridwidth = 4;
                break;
            case SizeData.Medium_6x6:
                gridwidth = 6;
                break;
            case SizeData.Large_8x8:
                gridwidth = 8;
                break;
        }
        //on decale le tilemap pour le centrer
        tilemapRenderer.transform.position = new Vector3(-gridwidth/2f-.5f,gridwidth/2f-.5f,0);
           
        //def du tableau de limites
        limitsTable = new bool[gridwidth,gridwidth];
        for(int i = 0; i < gridwidth; i++){
            for(int j = 0; j < gridwidth; j++){
                limitsTable[i,j] = false;
            }
        }
    }

    public void AddBlocToBorder(int x, int y){
        if(limitsTable[x,y]){
            return;
        }
        //etape 2 : on ajoute le bloc aux limites
        limitsTable[x,y] = true;
        //etape 3 : on ajoute le bloc a la tilemap
        tilemapRenderer.SetTile(new Vector3Int(x,-y,0), tile);
    }

    public bool IsBlocInBorder(Vector2 pos){
        return limitsTable[(int)pos.x,(int)pos.y];
    }

    public void ToggleBorder(int x, int y){
        if(limitsTable[x,y]){
            limitsTable[x,y] = false;
            tilemapRenderer.SetTile(new Vector3Int(x,-y,0), null);
        }else{
            limitsTable[x,y] = true;
            tilemapRenderer.SetTile(new Vector3Int(x,-y,0), tile);
        }
    }
}
