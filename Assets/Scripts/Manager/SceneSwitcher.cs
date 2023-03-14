using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

namespace Kekw.Manager
{
    /// <summary>
    /// Scene switching with transition animations.
    /// </summary>
    class SceneSwitcher: MonoBehaviour
    {
        [SerializeField]
        [Tooltip("PP aniator")]
        PlayableDirector _ppDirector;

        [SerializeField]
        [Tooltip("IN playable asset")]
        PlayableAsset _inTransition;

        [SerializeField]
        [Tooltip("OUT playable asset")]
        PlayableAsset _outTransition;

        string _sceneToLoad;

        AsyncOperation sceneLoadOperation;

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.X))
            {
                SwitchScene("Auditorio");
            }

            if (Input.GetKeyUp(KeyCode.Z))
            {
                SwitchScene("World_Dev");
            }
        }

        /// <summary>
        /// Switch scene after playing post processing animations.
        /// </summary>
        /// <param name="sceneName">Scene name</param>
        public void SwitchScene(string sceneName)
        {
            this._sceneToLoad = sceneName;
            _ppDirector.time = 0f;
            _ppDirector.Play(_inTransition);
            // Hook to timeline finished event.
            _ppDirector.stopped += DoSceneSwitchAsync;
        }
        
        /// <summary>
        /// Change scene after transition complete.
        /// </summary>
        /// <param name="obj"></param>
        private void DoSceneSwitchAsync(PlayableDirector director)
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
                _ppDirector.Play(_outTransition);
                sceneLoadOperation.completed -= OnSceneLoadComplete;
                sceneLoadOperation = null;
                _ppDirector.stopped -= DoSceneSwitchAsync;
            }
        }
    }
}
