using UnityEngine;

public class MC_LookAt : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        // Find the player by tag
        player = GameObject.Find("XR Origin");
    }

    void Update()
    {
        // Check if the player was found
        if (player != null)
        {
            // Calculate the rotation to look at the player
            Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);

            // Only take the Y-axis rotation
            float yRotation = targetRotation.eulerAngles.y;

            // Set the object's rotation with only the Y-axis rotation
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        }
    }
}
