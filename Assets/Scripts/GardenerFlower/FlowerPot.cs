using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gardening
{
    public class FlowerPot : MonoBehaviour
    {
        public Flower flower;
        private bool _isSeedPlanted;
        private GroundFilling _groundFillerScript;
        private float _timeFillInactive = 0.0f;

        private void Start()
        {
            _groundFillerScript = GetComponent<GroundFilling>();
        }

        private void Update()
        {
            if (_groundFillerScript.isCurrentlyFilling)
            {
                _timeFillInactive += Time.deltaTime;
                if (_timeFillInactive > 0.2f)
                {
                    _groundFillerScript.StopFill();
                }
            }
        }

        private void OnParticleCollision(GameObject other)
        {
            if (other.CompareTag("Dirt"))
            {
                _timeFillInactive = 0f;
                if (!_groundFillerScript.isCurrentlyFilling)
                    _groundFillerScript.StartFill();
            }
            if (!_groundFillerScript.isFilled)
                return;

            if (other.CompareTag("Seed"))
            {

            }

            if (!_isSeedPlanted)
                return;

            if (other.tag == "Water")
                flower.GrowFlower();
        }
    }
}

