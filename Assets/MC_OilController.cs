using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_OilController : MonoBehaviour, IHotObject
{
    public ParticleSystem boilEffect;
    private bool isOn;
    private float currentEmissionRate = 0f;
    private const float emissionChangeSpeed = 20f;
    private List<GameObject> objectsInOil = new List<GameObject>();
    private void OnEnable()
    {
        HotObjectManager.RegisterHotObject(this);
    }

    private void OnDisable()
    {
        HotObjectManager.UnregisterHotObject(this);
    }

    public void SetHot(bool isHot)
    {
        isOn = isHot;

        if (isOn && objectsInOil.Count != 0)
        {
            UpdateEmissionRate(80);
        }
        else if(!isOn)
        {
            UpdateEmissionRate(0);
        }
    }

    public bool IsHot()
    {
        return isOn;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (objectsInOil.Count == 0 && other.GetComponent<VegetableController>() && IsHot())
        {
            objectsInOil.Add(other.gameObject);
            UpdateEmissionRate(80);
        }
        else if(other.GetComponent<VegetableController>())
        {
            objectsInOil.Add(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<VegetableController>())
        {
            objectsInOil.Remove(other.gameObject);
            if (objectsInOil.Count == 0)
            {
                UpdateEmissionRate(0);
            }
        }
    }

    public void UpdateEmissionRate(float targetEmissionRate)
    {
        StartCoroutine(ChangeEmissionRate(targetEmissionRate));
    }

    private IEnumerator ChangeEmissionRate(float targetEmissionRate)
    {
        while (currentEmissionRate != targetEmissionRate)
        {
            currentEmissionRate = Mathf.MoveTowards(currentEmissionRate, targetEmissionRate, emissionChangeSpeed * Time.deltaTime);
            var emission = boilEffect.emission;
            emission.rateOverTime = currentEmissionRate;
            yield return null;
        }
        var emission2 = boilEffect.emission;
        emission2.rateOverTime = targetEmissionRate;
    }
}
