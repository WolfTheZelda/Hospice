using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AudioSource))]
public class EnemyAI : MonoBehaviour
{
    public bool debug = false;

    public float walkSpeed = 3.5f, runSpeed = 7.5f;

    public float eyeDistance = 25f;
    public float followDistance = 15f;
    public float attackDistance = 2.5f;
    private Transform[] waypoints;

    private Transform player;
    private int destiny;
    private bool seePlayer = false, huntPlayer = false, lookAt = false;
    private NavMeshAgent naveMesh;

    public Animator animator;
    private AudioSource audioSource;

    public GameObject mesh;

    // Start is called before the first frame update
    void Start()
    {
        naveMesh = GetComponent<NavMeshAgent>();

        player = GameObject.FindWithTag("Player").transform;

        WaypointAI[] waypointsAI = FindObjectsOfType<WaypointAI>();

        waypoints = new Transform[waypointsAI.Length];

        for(int i = 0; i < waypointsAI.Length; i++)
        {
            if(waypointsAI[i].tag == transform.tag)
            {
                waypoints[i] = waypointsAI[i].gameObject.transform;
            }
        }

        destiny = Random.Range(0, waypoints.Length);

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = Vector3.Distance(player.transform.position, transform.position);
        float waypointDistance = Vector3.Distance(waypoints[destiny].transform.position, transform.position);

        RaycastHit hit;

        if (playerDistance < eyeDistance)
        {
            if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, 1000))
            {
                seePlayer = hit.collider.gameObject.CompareTag("Player");
            }
        }
        else
        {
            huntPlayer = false;
            Walk();
            Debug.Log("Walk 0");
        }

        if (playerDistance <= eyeDistance && playerDistance > followDistance)
        {
            if (seePlayer)
            {
                lookAt = true;
                LookAt();
                Debug.Log("LookAt 0");
                StartCoroutine(resetSeePlayer());
            }
            else
            {
                Walk();
                Debug.Log("Walk 1");
            }
        }
        
        if (playerDistance <= followDistance && playerDistance > attackDistance)
        {
            if (seePlayer)
            {
                huntPlayer = true;
                Run();
                Debug.Log("Run 0");
            }
            else
            {
                huntPlayer = false;
                Walk();
                Debug.Log("Walk 2");
            }
        }
        
        if (playerDistance <= attackDistance)
        {
            Attack();
            Debug.Log("Attack 0");
        }
        
        if(waypointDistance <= 2.5f)
        {
            destiny = Random.Range(0, waypoints.Length);
            Walk();
            Debug.Log("Walk 3");
        }
        else if(!huntPlayer)
        {
            Walk();
            Debug.Log("Walk 4"); 
        }

        if (!lookAt)
        {
            mesh.transform.rotation = Quaternion.LookRotation(naveMesh.velocity, Vector3.up);
        }
    }

    void Walk()
    {
        if (!huntPlayer)
        {
            naveMesh.acceleration = 5f;
            naveMesh.speed = walkSpeed;
            naveMesh.destination = waypoints[destiny].position;
        }

        if (!debug)
        {
            // Animação de andar
            animator.SetInteger("EnemyMode", 1);
        }
    }

    void Run()
    {
        if (huntPlayer)
        {
            naveMesh.acceleration = 10f;
            naveMesh.speed = runSpeed;
            naveMesh.destination = player.position;
        }

        if (!debug)
        {
            // Animação de correr
            animator.SetInteger("EnemyMode", 2);
        }
    }

    void LookAt()
    {
        naveMesh.speed = 0f;
        transform.LookAt(player);

        if (!debug)
        {
            // Animação do inimigo encarando o jogador
            animator.SetInteger("EnemyMode", 3);
        }
    }

    void Attack()
    {
        player.GetComponent<PlayerLife>().life = 0;

        if (!debug)
        {
            // Aqui é possível fazer algum tipo de jumpscare
            animator.SetInteger("EnemyMode", 4);
        }
    }

    IEnumerator resetSeePlayer()
    {
        yield return new WaitForSecondsRealtime(2.5f);

        seePlayer = false;
        lookAt = false;
    }
}
