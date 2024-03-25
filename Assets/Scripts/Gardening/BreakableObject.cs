using UnityEngine;

namespace Gardening
{
    public class BreakableObject : MonoBehaviour
    {
        [SerializeField] private GameObject _brokenPrefab;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
                return;

            if (collision.relativeVelocity.magnitude > 5f){
                BreakObject();
            }
        }

        private void BreakObject()
        {
            Instantiate(_brokenPrefab, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}
