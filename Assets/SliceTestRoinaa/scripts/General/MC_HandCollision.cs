using Oculus.Interaction;
using UnityEngine;

public class MC_HandCollision : MonoBehaviour
{
    private MC_HandsEffectManager handEffectController;
    private Renderer rend;
    private void Start()
    {
        handEffectController = FindAnyObjectByType<MC_HandsEffectManager>();

        // Find the renderer based on the tag of the object
        if (gameObject.CompareTag("LeftHand"))
        {
            rend = GameObject.Find("asdMesh.002").GetComponent<Renderer>();
        }
        else if (gameObject.CompareTag("RightHand"))
        {
            rend = GameObject.Find("asdMesh.001").GetComponent<Renderer>();
        }

    }
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the specified tag
        if (collision.gameObject.CompareTag("Pan"))
        {
            IHotObject hotObject = collision.gameObject.GetComponent<IHotObject>();
            if (hotObject != null && HotObjectManager.IsObjectHot(hotObject))
            {
                handEffectController.ChangeStepScale(rend, 0.4f, 1f);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pot") || other.gameObject.CompareTag("Burner") || other.gameObject.CompareTag("Oil"))
        {
            IHotObject hotObject = other.gameObject.GetComponent<IHotObject>();
            if (hotObject != null && HotObjectManager.IsObjectHot(hotObject))
            {
                handEffectController.ChangeStepScale(rend, 0.4f, 1f);
            }
        }

        if (other.gameObject.CompareTag("WaterSource"))
        {
            MC_FaucetControllerHelper mC_FaucetControllerHelper = other.GetComponent<MC_FaucetControllerHelper>();
            if(mC_FaucetControllerHelper.isWaterOn())
            {
                handEffectController.ChangeStepScale(rend, 0f, 1f);
            }
        }
    }
}
