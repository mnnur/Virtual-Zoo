
using UnityEngine;
using UnityEngine.AI;


public class Giraffe : MonoBehaviour
{
    Animation anim;
    AnimalState animalState = AnimalState.IDLE;

    Player playerScript;
    GameObject player;
    NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer, playerLayer;
    Vector3 destinationPoint;
    bool walkPointSet;
    [SerializeField] float range;

    public int hunger = 40;

    float hungerDecayTimer = 0f;
    float hungerDecayDuration = 60f;
    int hungerDecay = 10;

    // Time variables for state transitions
    float idleTimer = 0f;
    float idleDuration = 5f; // Time to stay idle (seconds)
    float walkToIdleTimer = 0f;
    float walkToIdleDuration = 30f;

    [SerializeField] float sightRange = 5.0f;
    bool playerInSight;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        idleTimer += Time.deltaTime;
        walkToIdleTimer += Time.deltaTime;
        hungerDecayTimer += Time.deltaTime;
        if(hungerDecayTimer >= hungerDecayDuration){
            hungerDecayTimer = 0;
            hunger -= hungerDecay;
        }
        switch (animalState)
        {
            case AnimalState.IDLE:
                if (idleTimer >= idleDuration)
                {
                    idleTimer = 0f;
                    updateAnimalState(AnimalState.WALKING);
                }
                break;
            case AnimalState.WALKING:
                playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
                if(playerInSight && hunger <= 50 && playerScript.holdingFood){
                    chase();
                }
                else if (!playerInSight){
                    RandomWalk();
                }
                if (walkToIdleTimer >= walkToIdleDuration)
                {
                    walkToIdleTimer = 0f;
                    updateAnimalState(AnimalState.IDLE);
                }
                break;
            case AnimalState.RUNNING:
                break;
            case AnimalState.SLEEPING:
                break;
            default:
                updateAnimalState(AnimalState.IDLE);
                break;
        }
    }

    void RandomWalk(){
        if(!walkPointSet){
            SearchForDestination();
        }
        if(walkPointSet){
            agent.SetDestination(destinationPoint);
        }
        if(Vector3.Distance(transform.position, destinationPoint) < 10){
            walkPointSet = false;
        }
    }

    void SearchForDestination(){
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);

        destinationPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if(Physics.Raycast(destinationPoint, Vector3.down, groundLayer)){
            walkPointSet = true;
        }
    }

    void chase(){
        agent.SetDestination(player.transform.position);
    }


    void updateAnimalState(AnimalState animalState)
    {
        this.animalState = animalState;
        if(animalState == AnimalState.WALKING){
            anim.Play("animation_walk");
        }
        else if(animalState == AnimalState.IDLE){
            anim.Stop("animation_walk");
        }
    }
}
