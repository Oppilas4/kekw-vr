using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MC_SpiceBottle : MonoBehaviour
{
    public ParticleSystem _particleSystem;
    public AudioSource _shakeSound;
    private XRGrabInteractable grabInteractable;
    private bool _shaking = false;
    private float shakeThreshold = 0.75f; // Adjust this value based on your needs
    private float shakeVelocityThreshold = 0.1f; // Adjust this value based on your needs
    private Rigidbody _rigidbody;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _shakeSound = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (grabInteractable.isSelected)
        {
            float dotProduct = Vector3.Dot(transform.up, Vector3.down);
            if (dotProduct > shakeThreshold && !_shaking && _rigidbody.velocity.y < -shakeVelocityThreshold)
            {
                StartShaking();
            }
            else if (dotProduct < shakeThreshold && _shaking)
            {
                StopShaking();
            }
        }
        else if (_shaking) // If not selected but still shaking, stop shaking
        {
            StopShaking();
        }
    }

    void StartShaking()
    {
        _particleSystem.Play();
        _shakeSound.Play();
        _shaking = true;
    }

    void StopShaking()
    {
        _particleSystem.Stop();
        _shaking = false;
    }
}
