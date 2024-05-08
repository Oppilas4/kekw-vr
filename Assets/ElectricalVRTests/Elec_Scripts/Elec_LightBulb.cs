using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_LightBulb : MonoBehaviour
{
    public Material EmissionGreen;
    public Material Glass;
    MeshRenderer LightMesh;
    AudioSource AudioSource;
    public Material Nothing;
    public bool Sandbox;
    public float NeededVoltage = 5;
    bool Broken = false;
    public int TimeToDestroy = 5;
    public ParticleSystem shards;

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        if (Sandbox)
        {
            XRBaseInteractable Interactable = GetComponent<XRBaseInteractable>();
            Interactable.onSelectExited.AddListener(DisableBulbXR);          
        }
        LightMesh = GetComponent<MeshRenderer>();
        AudioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
    }
    public void BulbEnablee()
    {
        if(!Broken) 
        {
            LightMesh.material = EmissionGreen;
        }
        
    }
    public void BulbDisable()
    {
        if(!Broken && Glass != null && LightMesh != null)
        {
            LightMesh.material = Glass;
        }
        
    }
    public void CheckVoltage(XRBaseInteractor Interactor)
    {
 
    }
    void DisableBulbXR(XRBaseInteractor Interactor)
    {
        if(!Sandbox)  BulbDisable();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" && !Broken)
        {
            LightMesh.material = Nothing;
            Broken = true;
            AudioSource.Play();
            shards.Play();
            StartCoroutine(DestroyAfterTime(TimeToDestroy));
            Elec_Tero_AI.Instance.Say(Elec_Tero_AI.dialoguetype.LIGHTBULB);
        }
    }
    IEnumerator DestroyAfterTime(int time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
