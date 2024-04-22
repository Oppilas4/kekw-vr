using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public void RestartAllSpools()
    {
        foreach (var Stapler in FindObjectsOfType<Elec_MegaTool>().ToList())
        {
            Stapler.ResetWireList();
        }
    }
}
