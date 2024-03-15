using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private float _growthSpeed;
    [SerializeField] private int _growthTime;
    private Transform _flowerTransform;
    private bool _grown;

    private Vector3 _flowerGrownScale;
    private float _lerpedValue;  // Value of the Mathf.Lerp()
    private float _timeElapsed;  // 3rd, "t" parameter of the Mathf.Lerp()



    /// <summary>
    /// Sets flower to the default values
    /// </summary>
    public void PlantThePlant()
    {
        Debug.Log("Planted");
        _flowerTransform = GetComponent<Transform>();
        _flowerGrownScale = _flowerTransform.localScale;
        Debug.Log("Flower grown scale: " + _flowerGrownScale);

        _grown = false;
        _growthSpeed = _growthSpeed / 10;

        GameObject gameObject = Selection.activeGameObject;
        Undo.RecordObject(gameObject.transform, "Set scale to sprout");
        _flowerTransform.localScale = new Vector3(0, 0, 0); //Setting flower to the sprout scale
    }

    public void GrowFlower()
    {
        if (_grown)
            return;
        _timeElapsed += _growthSpeed * Time.deltaTime;
        _lerpedValue = Mathf.Lerp(0, _flowerGrownScale.x, _timeElapsed);
        if (_lerpedValue >= _flowerGrownScale.x)
            _grown = true;
        _flowerTransform.localScale = new Vector3(_lerpedValue, _lerpedValue, _lerpedValue);;
    }
}             