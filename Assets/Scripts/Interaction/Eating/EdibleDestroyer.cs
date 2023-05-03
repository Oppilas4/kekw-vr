using UnityEngine;

namespace Kekw.Interaction
{
    /// <summary>
    /// Destroys edible when it hits floor. Very similar to <seealso cref="Destroyer"/>.
    /// </summary>
    public class EdibleDestroyer: MonoBehaviour
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
