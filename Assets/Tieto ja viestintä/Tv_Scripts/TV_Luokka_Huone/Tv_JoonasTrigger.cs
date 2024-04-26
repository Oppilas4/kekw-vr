using UnityEngine;

public class Tv_JoonasTrigger : MonoBehaviour
{
    public TV_CheckIfCompletedRise check;
    public TV_JoonasBehaviour joonas;
    bool hasTalked;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hasTalked && check != null && check.hasTheNumber != 0)
            {
                check.Congratulations();
                hasTalked = true;
            }
        }
    }

}
