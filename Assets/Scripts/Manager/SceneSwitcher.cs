using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using System.Collections;

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


        /// <summary>
        /// Switch scene after playing post processing animations.
        /// </summary>
        /// <param name="sceneName">Scene name</param>
        public void SwitchScene(string sceneName)
        {
            this._sceneToLoad = sceneName;
            _ppDirector.Play(_inTransition);
            _ppDirector.played += DoSceneSwitch;
        }
        
        /// <summary>
        /// Change scene after transition complete.
        /// </summary>
        /// <param name="obj"></param>
        private void DoSceneSwitch(PlayableDirector director)
        {
            AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync(_sceneToLoad);
            sceneLoadOperation.completed += OnSceneLoadComplete;
        }
        
        /// <summary>
        /// Scene load async operation complete.
        /// </summary>
        /// <param name="operation"></param>
        private void OnSceneLoadComplete(AsyncOperation operation)
        {
            if (operation.isDone)
            {
                _ppDirector.Play(_outTransition);
            }
        }

        IEnumerator Test()
        {
            yield return new WaitForSeconds(5);
            SwitchScene("Empty");
        }
        
    }
}
