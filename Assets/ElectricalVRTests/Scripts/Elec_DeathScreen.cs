using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Elec_DeathScreen : MonoBehaviour
{
    GameObject Player;
    public GameObject DeathPosition;
    private void Start()
    {
        Player = GameObject.Find("XrCharacterSetupWithHands(Clone)");
    }
    public void teleportToDeath()
    {
        
        Player.transform.position = DeathPosition.transform.position;
    }
}
