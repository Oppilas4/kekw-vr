using UnityEngine;

namespace Assets.Scripts.Interaction
{
    /// <summary>
    /// Football stick behaviour
    /// </summary>
    class FootballStick: MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("RightHand") || collision.gameObject.CompareTag("LeftHand"))
            {

            }
        }
    }
}
