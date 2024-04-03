using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_OilController : MonoBehaviour, IHotObject
{
    public ParticleSystem boilEffect;
    private bool isOn = false;
    private float currentEmissionRate = 0f;
    private const float emissionChangeSpeed = 20f;
    public List<GameObject> objectsInOil = new List<GameObject>();

    public MC_DeepFrierTimer timer;
    private void OnEnable()
    {
        HotObjectManager.RegisterHotObject(this);
    }

    private void OnDisable()
    {
        HotObjectManager.UnregisterHotObject(this);
    }

    void Start()
    {
        timer.DeepFryTimerComplete.AddListener(UpdateCookedStatus);
    }

    public void SetHot(bool isHot)
    {
        isOn = isHot;

        if (isOn && objectsInOil.Count != 0)
        {
            UpdateEmissionRate(80);
            StartTimer();
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
        VegetableController vegetableController = other.GetComponent<VegetableController>();
        if (vegetableController != null)
        {
            VegetableData vegetableData = vegetableController.GetVegetableData();
            if (vegetableData.vegetableName == "Potato" && IsHot())
            {
                StartTimer();
            }
        }
        if (objectsInOil.Count == 0 && vegetableController != null && IsHot())
        {
            objectsInOil.Add(other.gameObject);
            UpdateEmissionRate(80);
        }
        else if(vegetableController != null && !objectsInOil.Contains(other.gameObject))
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
                timer.StopTimer();
                timer.gameObject.SetActive(false);
                UpdateEmissionRate(0);
            }
        }
    }

    private void StartTimer()
    {
        timer.gameObject.SetActive(true);
        timer.StartTimer(10);
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

    private void UpdateCookedStatus()
    {
        timer.gameObject.SetActive(false);
        foreach (GameObject obj in objectsInOil)
        {
            VegetableController vegetableController = obj.GetComponent<VegetableController>();
            if (vegetableController != null)
            {
                VegetableData vegetableData = vegetableController.GetVegetableData();
                if (vegetableData.vegetableName == "Potato")
                {
                    vegetableController.HandleBoiledEvent(vegetableController);
                }
            }
        }
    }

}
