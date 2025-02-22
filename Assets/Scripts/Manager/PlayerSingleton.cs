﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Linq;

namespace Kekw.Manager
{
    /// <summary>
    /// Simple singleton that survives scene changes.
    /// </summary>
    public class PlayerSingleton : MonoBehaviour
    {
        /// <summary>
        /// Teleportation provider.
        /// </summary>
        [SerializeField]
        [Tooltip("XR origin teleportation provider")]
        TeleportationProvider _teleportationProvider;

        public static GameObject Instance { get => _playerInstance; }
        private static GameObject _playerInstance;

        private void Awake()
        {
            if (_playerInstance && _playerInstance != this.gameObject)
            {
                Destroy(this.gameObject);
            }
            else if(_playerInstance == null)
            {
                _playerInstance = this.gameObject;
                DontDestroyOnLoad(this.gameObject);
                SetTeleportationAreas();
            }
        }

        /// <summary>
        /// Set interaction manager to all teleport areas
        /// </summary>
        private void SetTeleportationAreas()
        {
            TeleportationArea[] teleportationAreas = FindObjectsOfType<TeleportationArea>();
            foreach (TeleportationArea area in teleportationAreas)
            {
                area.teleportationProvider = _teleportationProvider;
            }
        }
    }
}
