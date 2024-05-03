using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_CatAI : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform Player;
    public Transform LaserPointerEnd;
    Animator animator;
    float Speed;
    public Transform Paws;
    public GameObject RamiPos, Head,MainCamera,GoTo;

    public Vector3 walkPoint;
    public bool walkPointSet,FelineIncstinctON, RamiOn,RoutineGoing;
    public bool Slepy = true;
    public float walkPointRange;
    public LayerMask whatIsGround;
    public InteractionLayerMask InteractionLayerMask;

    public List<AudioClip> Meows;
    public List<AudioClip> AngyMeow;
    public AudioSource AudioSource;
    public AudioSource Purr;

    XRSocketInteractor socketInteractor;
    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        MainCamera = Camera.main.gameObject;
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("XR Origin").GetComponent<Transform>();
        agent.enabled = false;
        socketInteractor = GetComponentInChildren<XRSocketInteractor>();
        socketInteractor.selectEntered.AddListener(GoodSir);
    }
    //It works so idc how much "if" it uses;
    private void Update()
    {
        if(Vector3.Distance(GoTo.transform.position,transform.position ) < 2)Destroy(gameObject);
        Speed = agent.velocity.magnitude;
        if(Speed < 1 && !AudioSource.isPlaying ) AudioSource.Play();
        animator.SetFloat("Speed",Speed);    
        if (Slepy && Vector3.Distance(Player.position, transform.position) < 4)
        {
            animator.SetTrigger("WakeyWakey");
        }
        if(agent.enabled && !Slepy) 
        {
            if (RamiOn)
            {
                Zoomies();
            }
            else if (!FelineIncstinctON)
            {
                animator.SetBool("CatchBool", false);
                agent.speed = 1.0f;
                agent.stoppingDistance = 2f;
                agent.SetDestination(Player.position);                
            }
            else if (FelineIncstinctON)
            {
                agent.speed = 2f;
                agent.stoppingDistance = 1f;
                agent.SetDestination(LaserPointerEnd.position);
                if (Vector3.Distance(Paws.position, LaserPointerEnd.position) < 0.25)
                {
                    animator.SetBool("CatchBool", true);
                }
            }
        }      
    }
    public void PlayAngyMew()
    {
        AudioSource.PlayOneShot(AngyMeow[Random.Range(0, 2)]);
    }
    void GoodSir(SelectEnterEventArgs pp)
    {
        if (pp.interactableObject.transform.name == "crown")
        {
            animator.SetTrigger("IUsedToRule");
            Slepy = true;
            agent.enabled = false;
        }
    }
    private void Zoomies()
    {
        agent.speed = 3;
        agent.stoppingDistance = 0;
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);

        }                
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        StartCoroutine(DontGetStuck());
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    IEnumerator DontGetStuck()
    {
        if (!RoutineGoing)
        {
            RoutineGoing = true;
            yield return new WaitForSeconds(5);
            walkPointSet = false;
            RoutineGoing = false;
        }       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "ShredEye")
        {
            AudioSource.PlayOneShot(AngyMeow[Random.Range(0, 2)]);
            animator.SetTrigger("RamiOn");
            other.GetComponent<XRBaseInteractable>().enabled = false;
            other.gameObject.transform.parent = RamiPos.transform;
            other.gameObject.transform.position = RamiPos.transform.position;
            other.gameObject.transform.rotation = RamiPos.transform.rotation;
            other.enabled = false;
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.GetComponentInChildren<Animator>().SetTrigger("Ridin");           
            RamiOn = true;
            socketInteractor.enabled = false ;
        }
    }
}
