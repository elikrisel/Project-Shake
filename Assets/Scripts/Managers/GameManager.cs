using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using JetBrains.Annotations;
using UnityEngine;
using System.Text;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            if (!instance) Instance = Instantiate(Resources.Load<GameObject>("Managers/GameManager").GetComponent<GameManager>());
            return instance;
        }
        set
        {
            if(instance && instance != value)
            {
                Destroy(instance.gameObject);
                instance = value;
            }
            else if(instance != value)
            {
                instance = value;
            }
        }
    }
    private static GameManager instance;


    public delegate void SettingChanged();

    public static SettingChanged Settingchanged;

    private bool isPlaying => Time.timeScale != 0;

     
    

    private void Awake()
    {

        if (!instance) Instance = this;
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        
        
    }



}

