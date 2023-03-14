using UnityEngine;

namespace Assets.Scripts.Manager
{
    class NonDestructable: MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Root gameobject")]
        GameObject _root;
        
        private void Awake()
        {
            DontDestroyOnLoad(_root);
        }
    }
}
