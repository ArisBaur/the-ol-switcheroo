using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg_music : MonoBehaviour
{
    // Static instance to ensure only one MusicController exists
    public static bg_music instance;

    void Awake()
    {
        // If the instance doesn't exist, set it to this instance and mark it as DontDestroyOnLoad
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // If the instance already exists, destroy this new one
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
