using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    public XRDirectInteractor rightHandInteractor; // Reference to the right hand interactor
    public XRDirectInteractor leftHandInteractor;  // Reference to the left hand interactor
    [SerializeField] private InputActionReference interactAction;
    public bool holdingFood = false;
    public GameObject holdedFood;

    public GameObject leftController;
    public GameObject rightController;
    [SerializeField] private InputActionReference rigthThumbstickPress;
    [SerializeField] private InputActionReference leftThumbstickPress;

    private XRRayInteractor leftHandXRRayInteractor;
    private LineRenderer leftHandLineRenderer;
    private XRInteractorLineVisual leftHandXRInteractorLineVisual;
    private SortingGroup leftHandSortingGroup;

    private XRRayInteractor rightHandXRRayInteractor;
    private LineRenderer rightHandLineRenderer;
    private XRInteractorLineVisual rightHandXRInteractorLineVisual;
    private SortingGroup rightHandSortingGroup;
    private AudioSource audioSource;

    public float debounceTime = 1.0f;
    private float lastButtonClickTime = 0.0f;
     private Vector3 previousPosition;

     public float playTime = 0.5f;
public float pauseTime = 0.5f;

    void Start()
    {
        leftHandXRRayInteractor = leftController.GetComponent<XRRayInteractor>();
        leftHandLineRenderer = leftController.GetComponent<LineRenderer>();
        leftHandXRInteractorLineVisual = leftController.GetComponent<XRInteractorLineVisual>();
        leftHandSortingGroup = leftController.GetComponent<SortingGroup>();

        rightHandXRRayInteractor = rightController.GetComponent<XRRayInteractor>();
        rightHandLineRenderer = rightController.GetComponent<LineRenderer>();
        rightHandXRInteractorLineVisual = rightController.GetComponent<XRInteractorLineVisual>();
        rightHandSortingGroup = rightController.GetComponent<SortingGroup>();

        audioSource = GetComponent<AudioSource>();

        ToggleRightRay();
        ToggleLeftRay();

        previousPosition = transform.position;
    }

    void Update()
    {
        FoodInteraction();
        LineInteractor();
    }

     void PlayMovementSound()
    {
        if (transform.position != previousPosition) // Check for position change
        {
             StartCoroutine("PlayPauseCoroutine");
            previousPosition = transform.position; // Update previous position
        }
    }

    IEnumerator PlayPauseCoroutine()
    {
        while(true)
        {
            audioSource.Play();
            yield return new WaitForSeconds(playTime);
            audioSource.Pause(); // or beep.Stop()
            yield return new WaitForSeconds(pauseTime);
        }
    }

    void ToggleLeftRay()
    {
        leftHandXRRayInteractor.enabled = !leftHandXRRayInteractor.enabled;
        leftHandLineRenderer.enabled = !leftHandLineRenderer.enabled;
        leftHandXRInteractorLineVisual.enabled = !leftHandXRInteractorLineVisual.enabled;
        leftHandSortingGroup.enabled = !leftHandSortingGroup.enabled;
    }

    void ToggleRightRay()
    {
        rightHandXRRayInteractor.enabled = !rightHandXRRayInteractor.enabled;
        rightHandLineRenderer.enabled = !rightHandLineRenderer.enabled;
        rightHandXRInteractorLineVisual.enabled = !rightHandXRInteractorLineVisual.enabled;
        rightHandSortingGroup.enabled = !rightHandSortingGroup.enabled;
    }

    void LineInteractor()
    {
        if (rigthThumbstickPress.action.IsPressed() && Time.time >= lastButtonClickTime + debounceTime)
        {
            Debug.Log("Right thumbstick clicked");
            ToggleRightRay();
            lastButtonClickTime = Time.time;
        }
        if (leftThumbstickPress.action.IsPressed() && Time.time >= lastButtonClickTime + debounceTime)
        {
            Debug.Log("Left thumbstick clicked");
            ToggleLeftRay();
            lastButtonClickTime = Time.time;
        }
    }

    void FoodInteraction()
    {
        if (rightHandInteractor.selectTarget != null || leftHandInteractor.selectTarget != null)
        {
            if (rightHandInteractor.selectTarget.tag == "Food")
            {
                holdingFood = true;
                holdedFood = rightHandInteractor.selectTarget.transform.gameObject;
            }
            if (leftHandInteractor.selectTarget.tag == "Food")
            {
                holdingFood = true;
                holdedFood = leftHandInteractor.selectTarget.transform.gameObject;
            }
        }
        else
        {
            holdingFood = false;
            holdedFood = null;
        }
    }
}
