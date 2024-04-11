using Autodesk.Fbx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_DeathItself : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    public List<Transform> targets;
    Transform Player;
    Transform temp;
    GameObject Camera;
    private void Start()
    {
        Camera = GameObject.Find("Main Camera");
        Player = GameObject.Find("XR Origin").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        RaycastHit Seen;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out Seen, 1000))
        {
            if (Seen.transform.GetComponent<Elec_DeathItself>())
            {
                LookedAtDeath();
            }
        }
        transform.LookAt(new Vector3(Player.position.x,transform.position.y,Player.position.z));
    }
    public void HereComesTheDeath()
    {
        foreach (Transform t in targets) 
        {
            if(temp == null) temp = t;
            if (Vector3.Distance(Player.position, t.transform.position) < Vector3.Distance(Player.position, temp.position) && temp != null)
            {
                temp = t;
            }
        }
        gameObject.transform.position = temp.position;
        audioSource.Play();
    }
    void LookedAtDeath()
    {
        animator.SetTrigger("Death");
    }
}
