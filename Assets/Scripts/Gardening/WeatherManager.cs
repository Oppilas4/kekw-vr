using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public static WeatherManager instance;

    public enum SkyboxWeather
    {
        Sunset,
        Rainy,
    }
    [System.Serializable]
    private struct AssociatedSkybox
    {
        public SkyboxWeather weather;
        public Material material;
    }
    [SerializeField]
    private AssociatedSkybox[] _skyboxes;
    private Dictionary<SkyboxWeather, Material> _skyboxMap = new();

    [SerializeField]
    private SkyboxWeather _defaultSkybox;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        foreach (var association in _skyboxes)
        {
            _skyboxMap.Add(association.weather, association.material);
        }

        SetSkybox(_defaultSkybox);
    }

    public void SetSkybox(SkyboxWeather weather)
    {
        RenderSettings.skybox = _skyboxMap[weather];
    }
}
