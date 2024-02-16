using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    //Drop this script on your XR Origin
    public Transform ResetPositionObject; //You can use that to just make empty object in scene and use it as a reset position
    public Vector3 VectorPosition; //Or you can use this to manually set values for position
    public void ResetPositionMethod()//Call this method to reset position
    {
        if (ResetPositionObject != null) transform.position = VectorPosition;
        else if (VectorPosition != null) transform.position = VectorPosition;
        else Debug.Log("None position is set");
    }
    private void Update()
    {
        if (Input.GetButtonDown("XRI_Right_PrimaryButton")) ResetPositionMethod();
    }
}
