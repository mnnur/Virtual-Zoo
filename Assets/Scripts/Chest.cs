using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : Interactable
{
    private bool isOpen = false;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    GameObject chestUI;
    [SerializeField] Camera renderUICamera;

    void Awake(){
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        chestUI = transform.GetChild(0).gameObject;
        Canvas chestCanvas = chestUI.GetComponent<Canvas>();
        chestCanvas.worldCamera = renderUICamera;
    }

    override public void Interact() //This function is meant to be overwritten depending on the interactable object.
    {
        Debug.Log("You interacted with " + gameObject.name + "!");
        if(isOpen){
            skinnedMeshRenderer.SetBlendShapeWeight(0, 0f);
            chestUI.SetActive(false);
            isOpen = false;
        }else{
            skinnedMeshRenderer.SetBlendShapeWeight(0, 100f);
            isOpen = true;
            chestUI.SetActive(true);
        }
    }
}