using System;
using UnityEngine;

namespace Gardening
{
    [Serializable]
    public struct AudioObject
    {
        public bool IsMusic;
        public string audioName;
        public AudioClip audioClip;
    }
}
