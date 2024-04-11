using System.Collections.Generic;
using UnityEngine;

namespace Gardening
{
    public class WeatherManager : MonoBehaviour
    {
        public static WeatherManager instance;

        public enum SkyboxWeather
        {
            Sunset,
            Rainy,
        }

        [System.Serializable]
        private struct WeatherData
        {
            public Material skybox;
            public bool rainy;
        }

        // Workaround to show dictionary in inspector
        [System.Serializable]
        private struct AssociatedSkybox
        {
            public SkyboxWeather weather;
            public WeatherData data;
        }

        [Header("Skybox settings")]
        [SerializeField]
        private AssociatedSkybox[] _skyboxes;
        private Dictionary<SkyboxWeather, WeatherData> _skyboxMap = new();

        [SerializeField]
        private SkyboxWeather _defaultSkybox;

        [Header("Rain settings")]
        [SerializeField]
        private Transform _playerTransform;
        [SerializeField]
        private GameObject _rainPrefab;
        [SerializeField]
        private float _rainHeight;
        private GameObject _rainInstance;

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
            _playerTransform = GameObject.FindWithTag("Player").transform;

            foreach (var association in _skyboxes)
            {
                _skyboxMap.Add(association.weather, association.data);
            }

            SetWeather(_defaultSkybox);
        }

        public void SetWeather(SkyboxWeather weather)
        {
            RenderSettings.skybox = _skyboxMap[weather].skybox;
            if (_skyboxMap[weather].rainy)
            {
                EnableRain();
            }
            else
            {
                DisableRain();
            }
        }

        public void EnableRain()
        {
            if (_rainInstance == null)
            {
                _rainInstance = Instantiate(_rainPrefab, _playerTransform);
                _rainInstance.transform.position += new Vector3(0, _rainHeight, 0);
            }
            _rainInstance.SetActive(true);
        }

        public void DisableRain()
        {
            if (_rainInstance == null) return;
            _rainInstance.SetActive(false);
        }
    }
}
