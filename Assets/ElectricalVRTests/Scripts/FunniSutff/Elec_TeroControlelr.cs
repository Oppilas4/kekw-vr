using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elec_TeroControlelr : MonoBehaviour
{
    public static Elec_TeroControlelr instance;
    static public int BulbsBroken = 0;
    bool bulbs;
    Elec_Tero_AI TeroHead;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        TeroHead = GetComponent<Elec_Tero_AI>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void Update()
    {
        if (bulbs && BulbsBroken >= 10)
        {
            bulbs = false;
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex != 0) 
        {
            Destroy(gameObject);
        }
    }
}
