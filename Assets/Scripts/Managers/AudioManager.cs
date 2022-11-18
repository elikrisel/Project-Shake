using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public string songName;
    public Sound[] sounds;
    public List<Coroutine> soundChanges = new List<Coroutine>();

    public static AudioManager instance;



    public void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);


    }




}
