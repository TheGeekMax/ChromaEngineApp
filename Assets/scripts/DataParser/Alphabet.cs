using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alphabet{
    
    public static char[] default16 = new char[]{
        '0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F'
    };

    public static char[] default64 = new char[]{
        '0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F',
        'G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V',
        'W','X','Y','Z','a','b','c','d','e','f','g','h','i','j','k','l',
        'm','n','o','p','q','r','s','t','u','v','w','x','y','z','+','/'
    };

    public char[] alphabet;
    public int size;

    public Alphabet(char[] alphabet,int size){
        this.alphabet = alphabet;
        this.size = size;
    }

    //fonctions d'obtentions

    public char Get(int index){
        return alphabet[index];
    }

    public int Get(char value){
        for(int i=0;i<alphabet.Length;i++){
            if(alphabet[i] == value){
                return i;
            }
        }
        return -1;
    }

    // binary <-> char

    public int[] tobinary(char value){
        int[] result = new int[size];
        int strId = Get(value);
        for(int i=0;i<size;i++){
            result[i] = strId % 2;
            strId = strId / 2;
        }
        return result;
    }

    public char frombinary(int[] value){
        int result = 0;
        for(int i=0;i<size;i++){
            result += value[i] * ((int)Mathf.Pow(2,i));
        }
        return Get(result);
    }

    //fonctions de transformations de binaires

    public int binaryto10(int[] value){
        int result = 0;
        for(int i=0;i<size;i++){
            result += value[i] * ((int)Mathf.Pow(2,i));
        }
        return result;
    }

    public int[] tenToBinary(int value){
        int[] result = new int[size];
        for(int i=0;i<size;i++){
            result[i] = value % 2;
            value = value / 2;
        }
        return result;
    }

    public Vector2Int binaryToCoors(int[] value){
        int resultx = 0;
        int resulty = 0;
        for(int i=0;i<size/2;i++){
            resulty += value[i] * ((int)Mathf.Pow(2,i));
        }
        for(int i=size/2;i<size;i++){
            resultx += value[i] * ((int)Mathf.Pow(2,i-size/2));
        }
        return new Vector2Int(resultx,resulty);
    }

    public int[] coorsToBinary(Vector2Int value){
        int[] result = new int[size];
        for(int i=0;i<size/2;i++){
            result[i] = value.y % 2;
            value.y = value.y / 2;
        }
        for(int i=size/2;i<size;i++){
            result[i] = value.x % 2;
            value.x = value.x / 2;
        }
        return result;
    }
}
