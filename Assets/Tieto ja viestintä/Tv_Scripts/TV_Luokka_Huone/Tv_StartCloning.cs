using UnityEngine;
using UnityEngine.UI;

public class Tv_StartCloning : MonoBehaviour
{
    public Tv_CloningMachine cloningMachine; // Reference to the CloningMachine script
  
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("LeftHand") || other.CompareTag("RightHand"))
        {
            cloningMachine.CloneObject();
        }
    }
   
}

