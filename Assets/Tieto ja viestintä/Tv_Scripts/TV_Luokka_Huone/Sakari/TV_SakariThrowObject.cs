using UnityEngine;

public class TV_SakariThrowObject : MonoBehaviour
{
    [SerializeField] TV_NewSakari sakari;
    [SerializeField] GameObject magicThing;
    public bool isInHands = false;
    public bool isInSakariHands = false;

    public void ShowTheMagic(bool show)
    {
        magicThing.SetActive(show);
    }

    public void CheckIfInHands(bool isIt)
    {
        isInHands = isIt;
        if(isInHands)
        {
            if (sakari.bottlesToThrow.Contains(gameObject))
            {
                sakari.bottlesToThrow.Remove(gameObject);
            }
        }
    }

    public void CheckIfInSakariHands(bool isIt)
    {
        isInSakariHands = isIt;
        if (isInSakariHands)
        {
            if (sakari.bottlesToThrow.Contains(gameObject))
            {
                sakari.bottlesToThrow.Remove(gameObject);
            }
        }
    }
}
