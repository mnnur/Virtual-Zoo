using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    public XRDirectInteractor rightHandInteractor; // Reference to the right hand interactor
    public XRDirectInteractor leftHandInteractor;  // Reference to the left hand interactor
    [SerializeField] InputActionReference interactAction;
    public bool holdingFood = false;
    bool animalInRange = false;

    void Start()
    {
        // Subscribe to selectEntered events
        if (rightHandInteractor != null)
        {
            rightHandInteractor.selectEntered.AddListener(OnSelectEnteredRightHand);
        }

        if (leftHandInteractor != null)
        {
            leftHandInteractor.selectEntered.AddListener(OnSelectEnteredLeftHand);
        }
    }

    void Update()
    {
        holdingFood = false; // Reset holdingFood state each frame
    }

    private void OnSelectEnteredRightHand(SelectEnterEventArgs args)
    {
        holdingFood = IsFood(args.interactableObject.transform);
    }

    private void OnSelectEnteredLeftHand(SelectEnterEventArgs args)
    {
        holdingFood = IsFood(args.interactableObject.transform);
    }

    // Function to check if the grabbed object has a "Food" tag (adjust as needed)
    bool IsFood(Transform grabbedObject)
    {
        return grabbedObject.CompareTag("Food"); // Replace "Food" with your actual tag
    }

    void OnTriggerEnter(Collider other) //This checks if the player collides with this interactable's collider
    {
        if (other.tag == "Animal" && holdingFood)
        {
            Debug.Log("You can interact feed this Animal using VR controls.");
            animalInRange = true;
        }
    }

    void OnTriggerExit(Collider other) //This checks if the player leaves the collider of the interactable.
    {
        if (other.tag == "Animal"  && holdingFood)
        {
            Debug.Log("Out of range to feed this Animal.");
            animalInRange = false;
        }
    }
}
