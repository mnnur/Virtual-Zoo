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

    }

    void Update()
    {
        if (rightHandInteractor.selectTarget.tag == "Food")
        {
            holdingFood = true;
            holdedFood = rightHandInteractor.selectTarget.transform.gameObject;
        }
        else if (leftHandInteractor.selectTarget.tag == "Food")
        {
            holdingFood = true;
            holdedFood = leftHandInteractor.selectTarget.transform.gameObject;
        }
        else
        {
            if (rightHandInteractor.selectTarget.tag != "Food" && leftHandInteractor.selectTarget.tag != "Food")
            {
                holdingFood = false;
                holdedFood = null;
            }
        }
    }
}
