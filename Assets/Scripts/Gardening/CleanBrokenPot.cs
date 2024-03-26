using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gardening
{
    public class CleanBrokenPot : MonoBehaviour
    {
        private List<GameObject> _potPieces = new List<GameObject>();
        private float _delayBeforeCleaning = 1.5f;
        private float _minTimeBeforeDeletion = .03f;
        private float _maxTimeBeforeDeletion = .06f;

        private void Start()
        {

            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                _potPieces.Add(gameObject.transform.GetChild(i).gameObject);
            }
            StartCoroutine(Clean());
        }

        private IEnumerator Clean()
        {
            yield return new WaitForSeconds(_delayBeforeCleaning);

            int piecesCounter = 0;
            while (piecesCounter < _potPieces.Count)
            {
                Destroy(_potPieces[piecesCounter]);
                piecesCounter++;

                yield return new WaitForSeconds(Random.Range(_minTimeBeforeDeletion, _maxTimeBeforeDeletion));
            }

            Destroy(gameObject);
        }
    }
}
