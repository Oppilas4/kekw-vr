using Kekw.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gardening
{
    public class FlowerPot : MonoBehaviour
    {
        public Plant plant { get; private set; }
        [SerializeField] private Transform _plantRootsPosition;
        private bool _isSeedPlanted = false;
        private bool _isFilledWithDirt;

        private void OnParticleCollision(GameObject other)
        {
            //if (other.CompareTag("Dirt"))
            //{

            //}

            //if (!_isFilledWithDirt)
            //    return;

            if (other.CompareTag("Seed") && !_isSeedPlanted)
            {
                if(other.transform.root.TryGetComponent<SeedPacket>(out var seedPacket))
                {
                    Quaternion sproutRotation = Quaternion.identity;
                    sproutRotation.eulerAngles = new Vector3(-90, 0, 0);
                    plant = Instantiate(seedPacket.associatedPlant, _plantRootsPosition.position, sproutRotation);
                    plant.PlantThePlant();
                    _isSeedPlanted = true;
                    Debug.Log("The seed is planted");
                }
            }

            if (!_isSeedPlanted)
                return;

            if (other.tag == "Water")
                plant.GrowFlower();
        }
    }
}

