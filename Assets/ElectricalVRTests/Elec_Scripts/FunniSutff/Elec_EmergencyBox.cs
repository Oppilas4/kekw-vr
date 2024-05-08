using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Elec_EmergencyBox : MonoBehaviour
{
    public int Screws = 4;
    public UnityEvent OnOpen;
    private void Start()
    {
        StartCoroutine(WaittillScrewsGone());
    }
    IEnumerator WaittillScrewsGone()
    {
        yield return new WaitUntil(() => Screws == 0);
        OnOpen?.Invoke();
    }
}
