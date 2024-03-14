using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_CustomerAniHelper : MonoBehaviour
{
    public MC_CustomerAI customerAI;
    public void TriggerFoodTransform()
    {
        customerAI.ChangeFoodTransform();
    }

    public void TriggerFoodDestroy()
    {
        customerAI.DestroyFoodItem();
    }
}
