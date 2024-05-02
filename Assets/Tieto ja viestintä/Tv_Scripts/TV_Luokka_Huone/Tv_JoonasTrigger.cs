using UnityEngine;

public class Tv_JoonasTrigger : MonoBehaviour
{
    public TV_CheckIfCompletedRise check;
    public TV_JoonasBehaviour joonas;
    bool hasTalked;
    [SerializeField] AudioSource joonasAudio;

    private void Start()
    {
        joonasAudio.volume = .1f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            joonasAudio.volume = 1f;

            if (!hasTalked && check != null && check.hasTheNumber != 0)
            {
                check.Congratulations();
                hasTalked = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            joonasAudio.volume = .1f;
        }
    }
}
