using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gardening
{
    public class FlowerPot : MonoBehaviour
    {
        public Flower flower;
        private bool _isSeedPlanted;
        private bool _isFilledWithDirt;

        private void OnParticleCollision(GameObject other)
        {
            if (other.CompareTag("Dirt"))
            {

            }

            if (!_isFilledWithDirt)
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

