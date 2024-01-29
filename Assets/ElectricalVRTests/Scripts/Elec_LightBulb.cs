using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_LightBulb : MonoBehaviour
{
    ParticleSystem LightParticle;
    public Material EmissionGreen;
    Material Glass;
    MeshRenderer LightMesh;
    AudioSource AudioSource;
    public Material Nothing;
    public bool Sandbox;
    public float NeededVoltage = 5;
    bool Broken = false;
    public int TimeToDestroy = 5;
    // Start is called before the first frame update
    void Start()
    {
        if (Sandbox)
        {
            XRBaseInteractable Interactable = GetComponent<XRBaseInteractable>();
            Interactable.onSelectEntered.AddListener(CheckVoltage);
            Interactable.onSelectExited.AddListener(DisableBulbXR);          
        }
        LightMesh = GetComponent<MeshRenderer>();
        LightParticle = GetComponentInChildren<ParticleSystem>();
        Glass = LightMesh.material;
        LightParticle.Stop();
        AudioSource = GetComponent<AudioSource>();
    }
    public void BulbEnablee()
    {
        if(!Broken) 
        {
            LightMesh.material = EmissionGreen;
            LightParticle.Play(); 
        }
        
    }
    public void BulbDisable()
    {
        if(!Broken)
        {
            LightMesh.material = Glass;
            LightParticle.Stop();
        }
        
    }
    public void CheckVoltage(XRBaseInteractor Interactor)
    {
        Elec_SandNode ThisNode = Interactor.GetComponent<Elec_SandNode>();
        if (ThisNode != null && ThisNode.currentVoltage == NeededVoltage ) 
        {
            BulbEnablee();
        }
    }
    void DisableBulbXR(XRBaseInteractor Interactor)
    {
        BulbDisable();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            LightMesh.material = Nothing;
            Broken = true;
            AudioSource.Play();
            StartCoroutine(DestroyAfterTime(TimeToDestroy));
        }
    }
    IEnumerator DestroyAfterTime(int time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
