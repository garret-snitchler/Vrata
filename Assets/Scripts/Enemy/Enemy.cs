using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using Valve.VR.InteractionSystem;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public Transform playerHead;
    public LayerMask groundLayer, playerLayer;
    public float health;
    public float walkPointRange;
    public float timeBetweenAttacks;
    public float sightRange;
    public float attackRange;
    public int damage;
    public Animator animator;
    //public ParticleSystem hitEffect;

    //Liv added
    [SerializeField] AudioClip[] randoEnemyClips;
    AudioSource enemySoundSource;

    private Vector3 walkPoint;
    private bool walkPointSet;
    private bool alreadyAttacked;
    private bool takeDamage;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerHead = GameObject.Find("HeadCollider").transform;
        navAgent = GetComponent<NavMeshAgent>();
        //Liv added
        enemySoundSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        bool playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }
        else if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        else if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }
        else if (!playerInSightRange && takeDamage)
        {
            ChasePlayer();
        }
    }
    private void RandomizeEnemySound()
    {
        AudioClip clip = randoEnemyClips[Random.Range(0, randoEnemyClips.Length)];
        enemySoundSource.PlayOneShot(clip);
    }
    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            navAgent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        animator.SetFloat("Velocity", 0.2f);

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        navAgent.SetDestination(playerHead.position);
        animator.SetFloat("Velocity", 0.6f);
        navAgent.isStopped = false; // Add this line
    }


    private void AttackPlayer()
    {
        navAgent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            transform.LookAt(playerHead.position);
            alreadyAttacked = true;
            animator.SetBool("Attack", true);
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

            Debug.DrawRay(transform.position, transform.forward * attackRange, Color.green, 2.0f);
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, attackRange + 1, playerLayer))
            {
                print(hit.transform.ToString());

                PlayerHUD playerHUD = hit.transform.GetComponentInParent<PlayerHUD>();
                if (playerHUD != null)
                {
                    playerHUD.DamagePlayer(damage);
                }
                else
                {
                    print("PlayerHUD is null");
                }
            }
        }
    }


    private void ResetAttack()
    {
        alreadyAttacked = false;
        animator.SetBool("Attack", false);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        //hitEffect.Play();
        StartCoroutine(TakeDamageCoroutine());

        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    private IEnumerator TakeDamageCoroutine()
    {
        takeDamage = true;
        yield return new WaitForSeconds(2f);
        takeDamage = false;
    }

    private void DestroyEnemy()
    {
        StartCoroutine(DestroyEnemyCoroutine());
    }

    private IEnumerator DestroyEnemyCoroutine()
    {
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(1.8f);
        //Liv Added
        Invoke("RandomizeEnemySound", 0);
        // just that line
        this.gameObject.GetComponent<Enemy>().enabled = false;
        this.gameObject.GetComponent<NavMeshAgent>().speed = 0;
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
