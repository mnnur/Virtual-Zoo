
using UnityEngine;
using UnityEngine.AI;


public class Giraffe : MonoBehaviour
{
    Animation anim;
    AnimalState animalState = AnimalState.IDLE;

    GameObject player;
    NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer, playerLayer;
    Vector3 destinationPoint;
    bool walkPointSet;
    [SerializeField] float range;

    Transform lookAt;
    Transform moveTo;
    public int hunger = 40;
    int moveSpeed = 10;
    int runSpeed = 20;
    float randomWalkRadius = 5f;

    float hungerDecayTimer = 0f;
    float hungerDecayDuration = 60f;
    int hungerDecay = 10;

    // Time variables for state transitions
    float idleTimer = 0f;
    float idleDuration = 5f; // Time to stay idle (seconds)
    float walkToIdleTimer = 0f;
    float walkToIdleDuration = 30f;
    float walkRandomTimer = 0f;
    float walkRandomDuration = 10f; // Time to walk randomly (seconds)
    float stopDistance = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        idleTimer += Time.deltaTime;
        walkRandomTimer += Time.deltaTime;
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
                RandomWalk();
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


    void RotateToTarget(Transform target)
    {
        // Rotate the giraffe to face the target
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Smoothly rotate over time
    }

    void MoveToTarget(Transform target)
    {
        // Move the giraffe towards the target
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime); // Move based on direction and speed
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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Food" && hunger < 50)
        {
            Player player = other.GetComponent<Player>();
            if (player.holdingFood)
            {
                moveTo = other.transform;
                lookAt = other.transform;
            }
            else {
                moveTo = null;
                lookAt = null;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        moveTo = null;
        lookAt = null;
    }
}
