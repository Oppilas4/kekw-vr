using Oculus.Interaction.Surfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elec_Tero_AI : MonoBehaviour
{

    public enum dialoguetype
    {
        WELCOME,
        IDLE,
        DEATHBYSCREWDRIVER,
        DEATHBYLIVEWIRES,
        DEATHBYPOWERISON,
        COFFEE,
        LIGHTBULB
    }
    //Kyrylo's stuff
    public enum DeathKind
    {
       WELCOME,
       DEATHBYSCREWDRIVER,
       DEATHBYLIVEWIRES,
       DEATHBYPOWERISON
    }

    public AudioSource ourAudioSource;
    public Elec_Tero_Speaker ourSpeaker;
    public List<AudioClip> Welcome;
    public List<AudioClip> Idle;
    public List<AudioClip> DeathByLiveWire;
    public List<AudioClip> DeathByScrewdriver;
    public List<AudioClip> DeathByPowerIsOn;

    public List<AudioClip> Coffee;
    public List<AudioClip> Lightbulb;

    public Animator ourAnimator;
    public ParticleSystem Bubbleswhiletalk;

    public bool isTalking = false;
    public float DialogueLength = 0.0f;
    private int HowMuchCoffee = 0;
    private int HowMuchLightbulb = 0;
    public DeathKind kindaDed;

    private static Elec_Tero_AI _instance;
    public static Elec_Tero_AI Instance { get { return _instance; } }


    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this);
    }
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex != 5) Destroy(gameObject);
        switch (kindaDed)
        {
            case DeathKind.DEATHBYSCREWDRIVER:
                Say(dialoguetype.DEATHBYSCREWDRIVER);
                print("die Screwdriver");
                break;
            case DeathKind.DEATHBYLIVEWIRES:
                Say(dialoguetype.DEATHBYLIVEWIRES);
                print("die Wire");
                break;
            case DeathKind.DEATHBYPOWERISON:
                Say(dialoguetype.DEATHBYPOWERISON);
                print("die PowerIsON");
                break;
            case DeathKind.WELCOME:
                Say(dialoguetype.WELCOME);
                print("Harro");
                break;
        }
    }
    public void Say(dialoguetype whatToSay)
    {
        if (isTalking) return;
        AudioClip toSay = null;

        ourAnimator.SetFloat(0, Random.Range(0, 2));

        switch(whatToSay)
        {
            case dialoguetype.WELCOME:
                toSay = Welcome[Random.Range(0, Welcome.Count-1)]; 
                break;
            case dialoguetype.IDLE:
                toSay = Idle[Random.Range(0, Idle.Count-1)]; 
                break;
            case dialoguetype.DEATHBYSCREWDRIVER:
                toSay = DeathByScrewdriver[Random.Range(0, DeathByScrewdriver.Count - 1)]; 
                break;
            case dialoguetype.DEATHBYLIVEWIRES:
                toSay = DeathByLiveWire[Random.Range(0, DeathByLiveWire.Count - 1)];
                break;
            case dialoguetype.DEATHBYPOWERISON:
                toSay = DeathByPowerIsOn[Random.Range(0, DeathByPowerIsOn.Count - 1)];
                break;
            case dialoguetype.COFFEE:
                toSay = Coffee[HowMuchCoffee];
                HowMuchCoffee = Mathf.Clamp(HowMuchCoffee += 1,0, Coffee.Count-1);
                break;
            case dialoguetype.LIGHTBULB:
                toSay = Lightbulb[HowMuchLightbulb];
                HowMuchLightbulb = Mathf.Clamp(HowMuchLightbulb += 1, 0, Lightbulb.Count - 1);
                break;
            default:
                print("No VoicELine defineedasdlkj"); //Henri's stroke
                break;
        }
        if (toSay == null) return;
        ourAnimator.SetBool("Talking",true);
        Bubbleswhiletalk.Play();
        isTalking = true;
        ourSpeaker.Talking = true;
        DialogueLength = toSay.length;
        ourAudioSource.PlayOneShot(toSay);
        StartCoroutine(LockUntilDialogueFinished(DialogueLength));
    }

    IEnumerator LockUntilDialogueFinished(float time)
    {
        yield return new WaitForSeconds(time);
        Bubbleswhiletalk.Stop();
        isTalking = false;
        ourSpeaker.Talking = false;
        ourAnimator.SetBool("Talking", false);
    }




    /////////DEBUG STUFFS
    
    [ContextMenu("DebugSayIdle")]
    private void DebugSayIdle()
    {
        Say(dialoguetype.IDLE);
    }
    [ContextMenu("DebugSayCoffee")]
    private void DebugSayCoffee()
    {
        Say(dialoguetype.COFFEE);
    }
    [ContextMenu("DebugSayWelcome")]
    private void DebugSayWelcome()
    {
        Say(dialoguetype.WELCOME);
    }
    [ContextMenu("DebugSayDeathbypowerison")]
    private void DebugSayDeathbypowerison()
    {
        Say(dialoguetype.DEATHBYPOWERISON);
    }
    [ContextMenu("DebugSayDeathbylivewires")]
    private void DebugSayDeathbylivewires()
    {
        Say(dialoguetype.DEATHBYLIVEWIRES);
    }
    [ContextMenu("DebugSayDeathbyscrewdriver")]
    private void DebugSayDeathbyscrewdriver()
    {
        Say(dialoguetype.DEATHBYSCREWDRIVER);
    }
}
