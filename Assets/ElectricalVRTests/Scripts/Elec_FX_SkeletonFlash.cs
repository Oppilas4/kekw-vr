using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_FX_SkeletonFlash : Monobehaviour
{
    public List<Renderer> NormalRenderers;
    public List<Renderer> SkellyRenderers;
    public int HowManyFlashes = 8;
    public float timeBetweenFlashes = 0.2f;

    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(FlashSkellyHands());
    }

    IEnumerator(FlashSkellyHands())
    {
        for (int i = 0; i < HowManyFlashes; i++)
        {
            foreach(Renderer foundRenderer in NormalRenderers)
            {
                foundRenderer.enabled = false;
            }
            foreach(Renderer foundRenderer in SkellyRenderers)
            {
                foundRenderer.enabled = true;
            }
            yield return null;
            yield return new WaitForSeconds(Mathf.Abs(timeBetweenFlashes));
                        foreach(Renderer foundRenderer in NormalRenderers)
            {
                foundRenderer.enabled = true;
            }
            foreach(Renderer foundRenderer in SkellyRenderers)
            {
                foundRenderer.enabled = false;
            }
            yield return null;
            yield return new WaitForSeconds(Mathf.Abs(timeBetweenFlashes));
        }
    }


}
