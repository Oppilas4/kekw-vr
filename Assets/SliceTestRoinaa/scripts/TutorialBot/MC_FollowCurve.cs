using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_FollowCurve : MonoBehaviour
{
    public MC_CircularPointsCreator _points;
    private GameObject playerObject;
    [SerializeField]private float speed = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Find the closest point object
        GameObject closestPointObject = _points.FindClosestObject(playerObject.transform.position);

        // Ensure a valid closest point object is found
        if (closestPointObject != null)
        {
            // Get the position of the closest point object
            Vector3 closestPoint = closestPointObject.transform.position;

            // Move towards the closest point object
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, closestPoint, speed * Time.deltaTime);
        }
    }

}
