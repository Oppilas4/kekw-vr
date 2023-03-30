using UnityEngine;

namespace Kekw.Interaction
{
    /// <summary>
    /// Destroys edible when it hits floor.
    /// </summary>
    class EdibleDestroyer: MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("KillPlane"))
            {
                GetComponentInChildren<Edible>().DestroyOnFloor();
            }
        }
    }
}
