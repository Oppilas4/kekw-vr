using UnityEngine;

public class TV_SakariThrowObject : MonoBehaviour
{
    [SerializeField] GameObject magicThing;

    public void ShowTheMagic(bool show)
    {
        magicThing.SetActive(show);
    }
}
