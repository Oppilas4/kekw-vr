using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_CheckIfRiseOfWIn : MonoBehaviour
{
    public void SaveData()
    {
        PlayerPrefs.SetInt("TV_RiseOfTheDark", 1);
        PlayerPrefs.Save();
    }
    public void ResetData()
    {
        PlayerPrefs.SetInt("TV_RiseOfTheDark", 0);
        PlayerPrefs.Save();
    }
}
