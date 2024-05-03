using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_Laptop : MonoBehaviour
{
    [SerializeField] bool isFliped = false;
    [SerializeField] GameObject closed, opened;

    void Start()
    {
        bool isOpen = Random.Range(0, 4) != 0; // 75% chance of being open

        if (isOpen)
        {
            closed.SetActive(false);
            opened.SetActive(true);
        }
        else
        {
            closed.SetActive(true);
            opened.SetActive(false);
        }
        if (isFliped)
        {
            float rotationToDo = Random.Range(170f, 190f);
            gameObject.transform.localRotation = Quaternion.Euler(0f, rotationToDo, 0f);
        }
        else
        {
            float rotationToDoIfFlpid = Random.Range(-10f, 10f);
            gameObject.transform.localRotation = Quaternion.Euler(0f, rotationToDoIfFlpid, 0f);
        }
    }
}
