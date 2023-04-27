using Kekw.Interaction;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

namespace Kekw.Manager
{
    /// <summary>
    /// Scene switching with transition animations.
    /// </summary>
    class SceneSwitcher: MonoBehaviour
    {



        [SerializeField]
        [Tooltip("Animation speed")]
        float _speed = 1f;

        [SerializeField]
        [Tooltip("plane in front of camera")]
        GameObject _camBlackPlane;
        
        Material _camBlackMaterial;

        [SerializeField]
        [Tooltip("Default jump position in scenes")]
        Vector3 _defaultJumpPosition;

        Vector3 _sceneJumpPosition;
        string _sceneToLoad;

        AsyncOperation sceneLoadOperation;
        Coroutine _runningFadeAnimation;

        private void Awake()
        {
            // Get material from plane
            _camBlackMaterial = _camBlackPlane.GetComponent<MeshRenderer>().material;
        }

        private void Start()
        {
            _runningFadeAnimation = StartCoroutine(AnimateMaterial(0, _camBlackMaterial));
        }

        private void Update()
        {
#if UNITY_EDITOR
            // inputs for testing
            if (Input.GetKeyUp(KeyCode.X))
            {
                SwitchScene("World_Dev");
            }

            if (Input.GetKeyUp(KeyCode.Z))
            {
                SetSceneJumpPosition(new Vector3(0f,.1f, 0f));
                SwitchScene("Auditorio", false);
            }

            if (Input.GetKeyUp(KeyCode.C))
            {
                SetSceneJumpPosition(new Vector3(8.215f, -.641f, -3.18f));
                SwitchScene("JoonaksenLuokka", false);
            }
#endif
        }

        /// <summary>
        /// Set player position in new scene. Set BEFORE scene change.
        /// </summary>
        /// <param name="position"></param>
        public void SetSceneJumpPosition(Vector3 position)
        {
            this._sceneJumpPosition = position;
        }

        /// <summary>
        /// Switch scene after playing post processing animations.
        /// </summary>
        /// <param name="sceneName">Scene name</param>
        public void SwitchScene(string sceneName, bool useDefaultPosition = true)
        {
            if (useDefaultPosition)
            {
                _sceneJumpPosition = _defaultJumpPosition;
            }
            this._sceneToLoad = sceneName;
            // stop previous coroutine if present.
            if(_runningFadeAnimation != null)
            {
                StopCoroutine(_runningFadeAnimation);
            }
            _camBlackPlane.SetActive(true);
            _runningFadeAnimation = StartCoroutine(AnimateMaterial(1f, _camBlackMaterial));
        }

        /// <summary>
        /// Change scene after transition complete.
        /// </summary>
        /// <param name="obj"></param>
        private void DoSceneSwitchAsync()
        {
            // Asynchronous scene change.
            sceneLoadOperation = SceneManager.LoadSceneAsync(_sceneToLoad);
            sceneLoadOperation.completed += OnSceneLoadComplete;
        }
        
        /// <summary>
        /// Scene load async operation complete.
        /// </summary>
        /// <param name="operation"></param>
        private void OnSceneLoadComplete(AsyncOperation operation)
        {
            if (operation.isDone && sceneLoadOperation != null)
            {
                // Update player to new position in scene while screen is black.
                PlayerSingleton.Instance.gameObject.GetComponentInChildren<CharacterController>().gameObject.transform.position = _sceneJumpPosition;
                // TODO maybe set rotation
                sceneLoadOperation.completed -= OnSceneLoadComplete;
                sceneLoadOperation = null;
                if (_runningFadeAnimation != null)
                {
                    StopCoroutine(_runningFadeAnimation);
                }
                _runningFadeAnimation = StartCoroutine(AnimateMaterial(0f, _camBlackMaterial));
            }
        }

        IEnumerator AnimateMaterial(float target, Material material)
        {
            if (target != 0 && target != 1) throw new System.Exception("Invalid target value, value has to be 0 or 1");
            // Animate plane to black
            if (target == 1)
            {
                // while alpha is transparent
                while (material.color.a < .95f)
                {
                    // increment alpha
                    IncrementColorAlpha(material, target);
                    yield return null;
                }
                // alpha opacity has reached high enough value to do scene change
                material.color = new Color(material.color.r, material.color.g, material.color.b, target);
                DoSceneSwitchAsync();
            }
            // Animate to hidden
            if (target == 0)
            {
                // while alpha is transparent
                while (material.color.a > .05f)
                {
                    // increment alpha
                    IncrementColorAlpha(material, target);
                    yield return null;
                }
                // alpha opacity has reached high enough value to do scene change
                material.color = new Color(material.color.r, material.color.g, material.color.b, target);
                _camBlackPlane.SetActive(false);
            }
            // after coroutine finishes set tracked routine to null.
            _runningFadeAnimation = null;
        }

        /// <summary>
        /// Increments color alpha value by time and speed
        /// </summary>
        /// <param name="material"></param>
        /// <param name="target"></param>
        private void IncrementColorAlpha(Material material, float target)
        {
            // increment alpha
            float currentAlpha = Mathf.Lerp(material.color.a, target, Time.deltaTime * _speed);
            material.color = new Color(material.color.r, material.color.g, material.color.b, currentAlpha);
        }
    }
}
