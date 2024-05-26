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
    public GameObject holdedFood;

    void Start()
    {
        // Subscribe to selectEntered events
        if (rightHandInteractor != null)
        {
            rightHandInteractor.selectEntered.AddListener(OnSelectEnteredRightHand);
            rightHandInteractor.selectExited.AddListener(OnSelectExitedRightHand);
        }

        if (leftHandInteractor != null)
        {
            leftHandInteractor.selectEntered.AddListener(OnSelectEnteredLeftHand);
            leftHandInteractor.selectExited.AddListener(OnSelectExitedLeftHand);
        }
    }

    void Update()
    {
        holdingFood = false; // Reset holdingFood state each frame
    }

    private void OnSelectEnteredRightHand(SelectEnterEventArgs args)
    {
        GameObject selected = rightHandInteractor.GetOldestInteractableSelected().transform.gameObject;
        if(IsFood(selected)){
            holdingFood = true;
            holdedFood = selected;
        }
    }

    private void OnSelectExitedRightHand(SelectExitEventArgs args){
        holdingFood = false;
        holdedFood = null;
    }

    private void OnSelectEnteredLeftHand(SelectEnterEventArgs args)
    {
        GameObject selected = leftHandInteractor.GetOldestInteractableSelected().transform.gameObject;
        if(IsFood(selected)){
            holdingFood = true;
            holdedFood = selected;
        }
    }

    private void OnSelectExitedLeftHand(SelectExitEventArgs args){
        holdingFood = false;
        holdedFood = null;
    }

    // Function to check if the grabbed object has a "Food" tag (adjust as needed)
    bool IsFood(GameObject grabbedObject)
    {
        return grabbedObject.CompareTag("Food"); // Replace "Food" with your actual tag
    }
}
