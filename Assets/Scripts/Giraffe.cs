using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android.LowLevel;

public class Giraffe : MonoBehaviour
{
    Animation anim;
    AnimalState animalState = AnimalState.IDLE;
    Transform lookAt;
    Transform moveTo;
    public int hunger = 80;
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
    float walkToIdleDuration = 20f;
    float walkRandomTimer = 0f;
    float walkRandomDuration = 10f; // Time to walk randomly (seconds)
    float stopDistance = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
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
                if (lookAt != null)
                {
                    RotateToTarget(lookAt);
                }
                if (moveTo != null)
                {
                    updateAnimalState(AnimalState.WALKING);
                }

                if (idleTimer >= idleDuration)
                {
                    idleTimer = 0f;
                    updateAnimalState(AnimalState.WALKING);
                }
                break;
            case AnimalState.WALKING:
                if (lookAt != null)
                {
                    RotateToTarget(lookAt);
                }
                if (moveTo != null)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, moveTo.position);
                    if (distanceToTarget > stopDistance)
                    {
                        MoveToTarget(moveTo); // Call the MoveToTarget function
                    }
                }
                else
                {
                    if (walkRandomTimer >= walkRandomDuration)
                    {
                        walkRandomTimer = 0f;
                        Vector3 randomPosition = GetRandomWalkPosition();
                        moveTo = new GameObject().transform; // Create a temporary transform for random walk
                        moveTo.position = randomPosition;
                    }
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

    Vector3 GetRandomWalkPosition()
    {
        // Generate a random position within the walk radius
        Vector3 randomOffset = Random.insideUnitSphere * randomWalkRadius;
        return transform.position + randomOffset;
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
            anim.Play();
        }
        else if(animalState == AnimalState.IDLE){
            anim.Stop();
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
