using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{

    XRDirectInteractor xr;
    public GameObject barrel;
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")][SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")][SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")][SerializeField] private float ejectPower = 150f;

    public Jami_XrSocketInteractorTag socket;
    public Magazine magazine;
    public XRBaseInteractor socketInteractor;
    public bool hasSlide = true;
    public bool hasMagazine;
    public bool bulletInChamber;

    public InputActionProperty reload;
    public InputActionProperty laser;

    public Magazine magazineCode;

    public GameObject slideTrigger;

    [Header("Sounds")]
    public AudioSource audioSource;
    public AudioClip fireSound;
    public AudioClip noAmmo;
    public AudioClip reloadSound;

    [System.Obsolete]
    void Start()
    {
        hasMagazine = false;
        reload.action.performed += asdjasd;
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();

        socketInteractor.onSelectEntered.AddListener(AddMagazine);
        socketInteractor.onSelectExited.AddListener(RemoveMagazine);
    }

    void Update()
    {

    }

    public void AddMagazine(XRBaseInteractable interactable)
    {
        magazine = interactable.GetComponent<Magazine>();
        hasSlide = false;
        hasMagazine = true;
        audioSource.PlayOneShot(reloadSound);
    }

    public void RemoveMagazine(XRBaseInteractable interactable)
    {
        magazine.DropMagazine();
        magazine = null;
        hasMagazine = false;
    }

    public void Slide()
    {

        if (hasMagazine && !bulletInChamber && !magazine.magEmpty)
        {
            magazine.MagShoot();
            bulletInChamber = true;
        }
        else if (bulletInChamber && hasMagazine && !magazine.magEmpty)
        {
            magazine.MagShoot();
            bulletInChamber = true;
            CasingRelease();
        }
        if (hasMagazine && magazine.numberOfBullets <= 0 && bulletInChamber || !hasMagazine && bulletInChamber)
        {
            bulletInChamber = false;
            CasingRelease();
        }
    }

    public void asdjasd(InputAction.CallbackContext context)
    {
        Debug.Log("Reload");
        if (hasMagazine)
        {
            socket.socketActive = false;
            Invoke("Wait", 0.75f);
        }

    }


    void Wait()
    {
        socket.socketActive = true;
    }
   /* void DisableCollider()
    {
        OnTargetReached.triggerActive = false;
    }
    void ActivateCollider()
    {
        OnTargetReached.triggerActive = true;

    }
   */
    //This function creates the bullet behavior
    void Shoot()
    {
        audioSource.PlayOneShot(fireSound);
        if (magazine != null)
        {
            magazine.MagShoot();

        }
        if (hasMagazine && bulletInChamber && magazine.numberOfBullets < 0 || !hasMagazine && bulletInChamber)
        {
            bulletInChamber = false;
        }

        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }

        // Create a bullet and add force on it in direction of the barrel
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

    }

    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }
    public void PullTheTrigger()
    {
        if (bulletInChamber)
        {
            //Debug.Log(magazine.numberOfBullets);
            gunAnimator.SetTrigger("Fire");
            RaycastHit hit;

            if (Physics.Raycast(barrel.transform.position, barrel.transform.forward, out hit))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.CompareTag("Enemy"))
                {
                    hit.transform.gameObject.BroadcastMessage("Hurt", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        else
        {
            audioSource.PlayOneShot(noAmmo);
        }
    }



}
