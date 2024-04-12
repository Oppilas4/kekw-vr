using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Elec_PuzzleCompletitionManager : MonoBehaviour
{
    public static int Pointz = 0;
    public UnityEvent OnAllComplete;
    void Start()
    {
        StartCoroutine(WaitTillAllCompleted());
    }
    IEnumerator WaitTillAllCompleted()
    {
        yield return new WaitUntil(() => Pointz >= 3);
    }
}
