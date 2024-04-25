using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tv_SakariTrigger : MonoBehaviour
{
    public Tv_Sakari sakariBehaviour;
    bool canCollect = true;
    private IEnumerator ResetCanCollect()
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds
        canCollect = true; // Allow collection again after 5 seconds
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("tv_pullo") && canCollect)
        {
            sakariBehaviour.bottle = other.gameObject;
            sakariBehaviour.isBottle = true;
            canCollect = false;
            StartCoroutine(ResetCanCollect());
        }
    }
}
