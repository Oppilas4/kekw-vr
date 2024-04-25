using UnityEngine;

public class TV_NewSakariTrigger : MonoBehaviour
{
    [SerializeField] TV_NewSakari sakari;

    private void OnTriggerEnter(Collider other)
    {
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
        if (other.gameObject.CompareTag("tv_pullo"))
        {
            if (sakari.bottlesToThrow.Contains(other.gameObject))
            {
                sakari.bottlesToThrow.Remove(other.gameObject);
            }
        }
    }
}
