using UnityEngine;

public class Tv_JoonasTrigger : MonoBehaviour
{
    public TV_JoonasBehaviour joonas;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            joonas.StartToMove(joonas.whereToGoStand);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            joonas.StartToMove(joonas.sitLocation);
        }
    }
}
