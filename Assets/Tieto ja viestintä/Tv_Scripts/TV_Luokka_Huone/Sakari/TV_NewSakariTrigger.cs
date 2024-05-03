using UnityEngine;

public class TV_NewSakariTrigger : MonoBehaviour
{
    [SerializeField] TV_NewSakari sakari;
    [SerializeField] AudioSource sakariAudio;

    private void Start()
    {
        sakariAudio.volume = .1f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            sakariAudio.volume = 1f;
        }

        if (other.gameObject.CompareTag("tv_pullo"))
        {
            TV_SakariThrowObject ThrowObject = other.GetComponent<TV_SakariThrowObject>();
            if (ThrowObject.isInHands == false && ThrowObject.isInSakariHands == false)
            {
                if (!sakari.bottlesToThrow.Contains(other.gameObject))
                {
                    sakari.bottlesToThrow.Add(other.gameObject);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sakariAudio.volume = .1f;
        }
        if (other.gameObject.CompareTag("tv_pullo"))
        {
            if (sakari.bottlesToThrow.Contains(other.gameObject))
            {
                sakari.bottlesToThrow.Remove(other.gameObject);
            }
        }
    }
}
