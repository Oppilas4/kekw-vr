using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juho_PelaajanSeuranta : MonoBehaviour
{
    public Transform player;
    private void Start()
    {
        player = GameObject.Find("XR Origin").GetComponent<Transform>();
    }

    void Update()
    {
        transform.position = player.position;
    }
}
