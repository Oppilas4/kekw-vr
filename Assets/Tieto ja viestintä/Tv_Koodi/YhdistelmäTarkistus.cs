using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Yhdistelmätt
{
    public GameObject particle1;
    public GameObject particle2;
    public GameObject finishedParticle;
}

public class YhdistelmäTarkistus : MonoBehaviour
{
    public Yhdistelmätt[] yhdistelmät;
    public GameObject[] allAloneParticle;

    public void TarkistaOnkoYhdistelmää()
    {
        foreach (var combo in yhdistelmät)
        {
            if (IsActiveAndAlone(combo.particle1) && IsActiveAndAlone(combo.particle2))
            {
                Debug.Log("Molemmat oli oikein ja loivat VastausaParticle");
                combo.particle1.SetActive(false);
                combo.particle2.SetActive(false);
                combo.finishedParticle.SetActive(true);
            }

            Debug.Log("Molemmat oli väärin ja eivä't loivat VastausaParticle");
        }
    }

    bool IsActiveAndAlone(GameObject particle)
    {
        // Check if the particle is active
        if (particle.activeSelf)
        {
            // Check if the particle is not part of any combination
            foreach (var combo in yhdistelmät)
            {
                if (particle == combo.particle1 || particle == combo.particle2)
                {
                    Debug.Log("Oli yhdietlmä partice");
                    return true;
                }
                if (particle != combo.particle1 || particle != combo.particle2)
                {
                    Debug.Log("Ei ollut partice");
                    return false;
                }
            }
        }
        return false;
    }

}
