using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_VuoksiBeatsSpawner : MonoBehaviour
{
    public GameObject[] cubes;
    public Transform[] points;
    public float beat = 60/105*2;
    private float timer;
    public AudioSource audioSource;

    private bool gameRunning = false;
    public void StartVuoksiBeats()
    {
        gameRunning = true;
        audioSource.Play();
    }

    public void StopVuoksiBeats()
    {
        gameRunning = false;
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameRunning && audioSource.isPlaying)
        {
            if (timer > beat)
            {
                // Choose a random point
                Transform point = points[Random.Range(0, points.Length)];
                // Instantiate the cube at the world position of the point
                GameObject cube = Instantiate(cubes[Random.Range(0, cubes.Length)], point.position, Quaternion.Euler(0, 90, Random.Range(0, 4) * 90));
                timer -= beat;
            }

            timer += Time.deltaTime;
        }
    }
}
