using UnityEngine;
using System.Collections;
using UnityEngine.VFX;
using System.Data;
using Oculus.Interaction;

public class StoveController : MonoBehaviour, IHotObject
{
    public string steakTag = "Steak";
    private SteakController currentSteak;
    public bool isPanHot = false;
    private float panHeat = 5f;
    private bool isCoroutineRunning = false; // Flag to check if the coroutine is running
    private MC_BurnerHelper currentBurnerHelper;

    public GameObject oilObject;
    private Renderer oilRend;
    public ParticleSystem fryingEffect;
    private MC_FryableObjectHelper fryableObject;

    public MC_PourController pourController;
    public VisualEffect pourEffect;
    private bool isPouring;
    private AudioSource _audioSource;
    public AudioClip _fryingSound;
    public AudioClip _waterSound;
    

    private void OnEnable()
    {
        HotObjectManager.RegisterHotObject(this);
    }

    private void OnDisable()
    {
        HotObjectManager.UnregisterHotObject(this);
    }

    void Start()
    {
        oilRend = oilObject.GetComponent<Renderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (pourController.IsContainerTilted() && oilRend.material.GetFloat("_Fill") > 0)
        {
            if(!isPouring)
            {
                pourController.SetPourPosition(pourEffect);
            }
            float fillLevel = oilRend.material.GetFloat("_Fill");
            if (fillLevel < 0.02f)
            {
                // Get the current Vector2 value of _RemapOut
                Vector2 currentRemapOut = oilRend.material.GetVector("_RemapOut");

                // Set the x value to -0.1777 and keep the y value the same
                Vector2 newRemapOut = new Vector2(-0.1777f, currentRemapOut.y);

                // Set the new Vector2 value for _RemapOut
                oilRend.material.SetVector("_RemapOut", newRemapOut);
            }

            pourController.PourLiquid(pourEffect, oilRend, fillLevel);
            isPouring = true;
        }
        else
        {
            pourEffect.SendEvent("Stop");
            isPouring = false;
        }
    }

    public void SetHot(bool isHot)
    {
        // Implement logic to set the hot state of the pot
    }

    public bool IsHot()
    {
        return isPanHot;
    }

    IEnumerator panHeatCoroutine()
    {
        isCoroutineRunning = true;

        float startTime = Time.time;

        while (panHeat > 0)
        {
            float elapsedTime = Time.time - startTime;
            panHeat = Mathf.Clamp(5f - elapsedTime, 0f, 5f);

            yield return null;
        }

        isPanHot = false;
        _audioSource.Stop();

        isCoroutineRunning = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(steakTag))
        {
            currentSteak = collision.gameObject.GetComponent<SteakController>();
            if (currentSteak != null && isPanHot)
            {
                if (!_audioSource.isPlaying)
                {
                    _audioSource.clip = _fryingSound;
                    _audioSource.loop = true;
                    _audioSource.Play();
                }
                currentSteak.StartCooking();
            }
        }

        fryableObject = collision.gameObject.GetComponent<MC_FryableObjectHelper>();

        if (fryableObject != null && oilRend.material.GetFloat("_Fill") > 0 && IsHot())
        {
            // Enable the frying particle effect
            fryingEffect.Play();
            // Start frying if not already frying
            if (!fryableObject.IsFrying)
            {
                fryableObject.StartFrying();
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(steakTag))
        {
            SteakController steakController = collision.gameObject.GetComponent<SteakController>();
            if (steakController != null)
            {
                steakController.StopCooking();
                currentSteak = null;
                _audioSource.Stop();
            }
        }

        if (fryableObject != null && fryableObject.IsFrying)
        {
            // Disable the frying particle effect
            fryingEffect.Stop();
            // Stop frying if it was started
            fryableObject.StopFrying();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Burner"))
        {
            currentBurnerHelper = other.GetComponent<MC_BurnerHelper>();
            if (currentBurnerHelper != null && currentBurnerHelper.isBurnerOn())
            {
                isPanHot = true;

                if (currentSteak != null)
                {
                    currentSteak.StartCooking();
                }
            }
        }

        if (other.gameObject.CompareTag("WaterSource"))
        {
            if (other.GetComponent<MC_FaucetControllerHelper>().isWaterOn() && IsHot())
            {
                _audioSource.clip = _waterSound;
                _audioSource.loop = false;
                _audioSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Burner"))
        {
            currentBurnerHelper = null;

            if (!isCoroutineRunning)
            {
                panHeat = 5f;
                StartCoroutine(panHeatCoroutine());
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Burner"))
        {
            // Check if the burner is on and the pan is on a cold burner
            if (currentBurnerHelper != null && currentBurnerHelper.isBurnerOn() && !isPanHot)
            {
                isPanHot = true;

                if (currentSteak != null)
                {
                    if (!_audioSource.isPlaying)
                    {
                        _audioSource.clip = _fryingSound;
                        _audioSource.loop = true;
                        _audioSource.Play();
                    }
                    currentSteak.StartCooking();
                }
            }
            else if (currentBurnerHelper != null && !currentBurnerHelper.isBurnerOn() && isPanHot)
            {
                // Burner is off, cool down the pan
                if (!isCoroutineRunning)
                {
                    panHeat = 5f;
                    StartCoroutine(panHeatCoroutine());
                }
            }
        }

        if (other.gameObject.CompareTag("OilBottle"))
        {
            // Get the current Vector2 value of _RemapOut
            Vector2 currentRemapOut = oilRend.material.GetVector("_RemapOut");

            // Set the x value to -0.006 and keep the y value the same
            Vector2 newRemapOut = new Vector2(-0.006f, currentRemapOut.y);

            // Set the new Vector2 value for _RemapOut
            oilRend.material.SetVector("_RemapOut", newRemapOut);

            float currentFill = oilRend.material.GetFloat("_Fill");

            // Increase the fill value slowly from  0 to  1
            currentFill = Mathf.MoveTowards(currentFill, 1f, 0.1f * Time.deltaTime);

            // Set the new fill value
            oilRend.material.SetFloat("_Fill", currentFill);
        }
    }

}
