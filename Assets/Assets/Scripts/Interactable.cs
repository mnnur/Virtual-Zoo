using UnityEngine;
using UnityEngine.InputSystem;

//blueprint for all objects in the world that can be interacted with
public class Interactable : MonoBehaviour
{
    // This goes on objects that can be interacted with. The player can interact with objects using the configured VR Interact action.
    [Header("Distance Variables")]
    public float distanceFromPlayer;
    public bool isClosestInteractable;
    public float cooldownTime = 0.5f;
    private float lastInteractionTime = 0.0f;

    bool playerIsCloseEnoughToInteractWithThis;

    [Header("References")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject interactText; //Text that appears on the UI when this is the closest interactable.
    [SerializeField] InputActionReference interactAction; // Reference to the XR Interaction
    InteractableDistanceList intDistList; //Reference to the script on player that keeps track of the lowest distance from player between interactables.


    public virtual void Interact() //This function is meant to be overwritten depending on the interactable object.
    {
        Debug.Log("You interacted with " + gameObject.name + "!");
    }

    void Start()
    {
        intDistList = player.GetComponent<InteractableDistanceList>();
    }

    void Update()
    {
        if (distanceFromPlayer != intDistList.lowestDistance)
        {
            isClosestInteractable = false;
            interactText.SetActive(false);
        }
        if (playerIsCloseEnoughToInteractWithThis) //If the player is close enough to be able to interact with this object...
        {
            distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position); //...we need to calculate how far the interactable is from the player.
            if (intDistList.lowestDistance == 0f) //If the lowest distance among interactables is 0...
            {
                if (!isClosestInteractable) //...and this is not already marked as the closest interactable...
                {
                    isClosestInteractable = true; //...this needs to be marked as the closest interactable.
                    interactText.SetActive(true); //...and the UI text showing that the player can interact needs to appear.
                }
            }
            if (distanceFromPlayer <= intDistList.lowestDistance || isClosestInteractable) //If this interactable's distance from the player is less than or equal to the lowest distance among interactables OR this is already marked as the closest interactable...
            {
                intDistList.lowestDistance = distanceFromPlayer; //...then the lowest distance among interactables is equal to this interactable's distance variable.
                if (!isClosestInteractable) //If this interactable is not already marked as the closest interactable...
                {
                    isClosestInteractable = true; //...then it needs to be marked as the closest.
                    interactText.SetActive(true); //...and the UI text showing that the player can interact needs to appear.
                }
            }
        }
        if (isClosestInteractable && interactAction.action.IsPressed() && Time.time >= lastInteractionTime + cooldownTime)
        {
            Interact();
            lastInteractionTime = Time.time; // Update the last interaction time
        }
    }

    void OnTriggerEnter(Collider other) //This checks if the player collides with this interactable's collider
    {
        if (other.tag == "Player")
        {
            playerIsCloseEnoughToInteractWithThis = true;
            Debug.Log("You can interact with this object using VR controls.");
        }
    }

    void OnTriggerExit(Collider other) //This checks if the player leaves the collider of the interactable.
    {
        if (other.tag == "Player")
        {
            playerIsCloseEnoughToInteractWithThis = false;
            Debug.Log("Out of range to interact with object.");
        }
    }
}
