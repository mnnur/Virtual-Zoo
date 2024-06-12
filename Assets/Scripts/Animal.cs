
using UnityEngine;
using UnityEngine.AI;


public class Animal : MonoBehaviour
{
    protected Animator anim;
    public AnimalState animalState = AnimalState.IDLE;

    Player playerScript;
    GameObject player;
    NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer, playerLayer;
    Vector3 destinationPoint;
    bool walkPointSet;
    [SerializeField] float walkRange;

    public int hunger = 40;

    float hungerDecayTimer = 0f;
    [SerializeField] float hungerDecayDuration = 60f;
    [SerializeField] int hungerDecay = 10;

    // Time variables for state transitions
     float idleTimer = 0f;
    [SerializeField] float idleDuration = 5f; // Time to stay idle (seconds)
    float noiseTimer = 0f;
    [SerializeField] float noiseDuration = 90f; 
    float walkToIdleTimer = 0f;
    [SerializeField] float walkToIdleDuration = 30f;

    [SerializeField] float sightRange = 50.0f;
    bool playerInSight;
    [SerializeField] DietType dietType = DietType.OMNIVORE;
    [SerializeField] protected AudioClip animalNoise;
    [SerializeField] public AudioClip animalHappy;
    public AudioSource animalAudio;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
        animalAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(animalAudio != null && animalNoise != null){
            playNoise();
        }
        mechanicHunger();
        stateMachine();
    }

    protected void mechanicHunger(){
        hungerDecayTimer += Time.deltaTime;
        if(hungerDecayTimer >= hungerDecayDuration){
            hungerDecayTimer = 0;
            hunger -= hungerDecay;
        }
    }

    protected void playNoise(){
        noiseTimer += Time.deltaTime;
        AnimalState prevAnimalState = animalState;
        if(noiseTimer >= noiseDuration){
            updateAnimalState(AnimalState.MAKESOUND);
            noiseTimer = 0;
            animalAudio.Stop();
            animalAudio.PlayOneShot(animalNoise);
            updateAnimalState(prevAnimalState);
        }   
    }

    protected void stateMachine(){
        idleTimer += Time.deltaTime;
        walkToIdleTimer += Time.deltaTime;
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
            case AnimalState.MAKESOUND:
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
        float z = Random.Range(-walkRange, walkRange);
        float x = Random.Range(-walkRange, walkRange);

        destinationPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if(Physics.Raycast(destinationPoint, Vector3.down, groundLayer)){
            walkPointSet = true;
        }
    }

    void chase(){
        agent.SetDestination(player.transform.position);
    }


    protected virtual void updateAnimalState(AnimalState animalState)
    {
        this.animalState = animalState;
        if(animalState == AnimalState.WALKING){

        }
        else if(animalState == AnimalState.IDLE){

        }
    }
}
