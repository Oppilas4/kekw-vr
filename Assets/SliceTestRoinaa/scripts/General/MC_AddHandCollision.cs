using System.Collections.Generic;
using UnityEngine;

public class MC_AddHandCollision : MonoBehaviour
{
    public List<GameObject> leftHands = new List<GameObject>();
    public List<GameObject> rightHands = new List<GameObject>();

    void Start()
    {
        foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            if (gameObj.name == "LeftHand ")
            {
                leftHands.Add(gameObj);
            }
            else if (gameObj.name == "RightHand")
            {
                rightHands.Add(gameObj);
            }
        }

        // Check if objects are found
        if (leftHands.Count > 0 && rightHands.Count > 0)
        {
            // Loop through the leftHand list and add MC_HandCollision component
            foreach (var leftHand in leftHands)
            {
                if (leftHand.CompareTag("LeftHand"))
                {
                    leftHand.AddComponent<MC_HandCollision>();
                }
            }

            // Loop through the rightHand list and add MC_HandCollision component
            foreach (var rightHand in rightHands)
            {
                if (rightHand.CompareTag("RightHand"))
                {
                    rightHand.AddComponent<MC_HandCollision>();
                }
                
            }
        }
        else
        {
            Debug.LogError("One or both hands not found!");
        }
    }
}
