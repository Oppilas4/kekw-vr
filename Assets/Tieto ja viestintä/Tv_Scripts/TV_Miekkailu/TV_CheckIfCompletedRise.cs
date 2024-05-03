using UnityEngine;

public class TV_CheckIfCompletedRise : MonoBehaviour
{
    public Tv_JoonasAudioManager audioManager;
    public int hasTheNumber = 0;
    public AudioClip audioClip;

    private void Start()
    {
        hasTheNumber = PlayerPrefs.GetInt("TV_RiseOfTheDark", 0);

        if(hasTheNumber == 0)
        {
            gameObject.SetActive(false);
        }
        
    }

    public void Congratulations()
    {
        audioManager.PlayVoiceline(audioClip);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("TV_RiseOfTheDark", 0);
        PlayerPrefs.Save();
    }
}
