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
    public List<Material> materialsInOil = new List<Material>();
    private AudioSource _audioSource;

    public MC_DeepFrierTimer timer;
    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
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
            if (_audioSource != null && !_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
            UpdateEmissionRate(80);
            StartTimer();
        }
        else if(!isOn)
        {
            if (_audioSource != null)
            {
                _audioSource.Stop();
            }
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
            materialsInOil.AddRange(vegetableController.GetMaterials());
            UpdateEmissionRate(80);
            if (_audioSource != null && !_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
        else if(vegetableController != null && !objectsInOil.Contains(other.gameObject))
        {
            objectsInOil.Add(other.gameObject);
            materialsInOil.AddRange(vegetableController.GetMaterials());
        }
    }

    public void OnTriggerExit(Collider other)
    {
        VegetableController vegetableController = other.GetComponent<VegetableController>();
        if (vegetableController != null)
        {
            objectsInOil.Remove(other.gameObject);

            List<Material> materialsToRemove = vegetableController.GetMaterials();

            // Remove these materials from the materialsInOil list
            foreach (Material material in materialsToRemove)
            {
                materialsInOil.Remove(material);
            }
            if (objectsInOil.Count == 0)
            {
                if (_audioSource != null)
                {
                    _audioSource.Stop();
                }
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
        StartCoroutine(IncrementFloatAndSetNoiseLerp(1f));
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

    private IEnumerator IncrementFloatAndSetNoiseLerp(float targetValue)
    {
        foreach (Material material in materialsInOil)
        {
            material.SetFloat("_isFried", 1); // Assuming _isFried is a float in the shader
        }
        float currentValue = 0f;
        while (currentValue < targetValue)
        {
            currentValue += 0.1f;
            foreach (Material material in materialsInOil)
            {
                material.SetFloat("_NoiseLerp", currentValue);
            }
            yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds before the next increment
        }
    }

}
