using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//import audio manager
using UnityEngine.Audio;
using TMPro;

public class AudioManager : MonoBehaviour{
    public static AudioManager instance;

    public TextMeshProUGUI textButton;
    bool isMute = false;

    public GameObject objectSource;
    //list of all the sounds
    public List<AudioClip> sounds;

    void Awake(){
        instance = this;
        //for each sound in the list
        foreach(AudioClip sound in sounds){
            //append Audio source to existing object
            AudioSource source = objectSource.AddComponent<AudioSource>();
            //set the clip to the sound
            source.clip = sound;
            
        }
    }

    public void Play(int id){
        //play the sound at the id
        if(isMute){
            return;
        }
        objectSource.GetComponents<AudioSource>()[id].Play();
    }

    public void ToggleMute(){
        //toggle mute
        isMute = !isMute;
        //set the text of the button
        textButton.text = isMute ? "Unmute" : "Mute";
        //set the mute of the audio source
        objectSource.GetComponent<AudioSource>().mute = isMute;
    }
}
