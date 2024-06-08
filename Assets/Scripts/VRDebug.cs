using UnityEngine;
using UnityEngine.InputSystem;

public class VRDebug : MonoBehaviour
{
    public float debounceTime = 1.0f;
    private float lastButtonClickTime = 0.0f;
    public GameObject UI;
    public GameObject UIAnchor;
    private bool UIActive;
    [SerializeField] private InputActionReference inputAction;

    void Start(){
        UI.SetActive(false);
        UIActive = false;
    }

    void Update(){
        if(inputAction.action.IsPressed() && Time.time >= lastButtonClickTime + debounceTime){
            UIActive = !UIActive;
            UI.SetActive(UIActive);
            lastButtonClickTime = Time.time;
        }
        if(UIActive){
            UI.transform.position = UIAnchor.transform.position;
            UI.transform.eulerAngles = new Vector3(UIAnchor.transform.eulerAngles.x, UIAnchor.transform.eulerAngles.y, 0);
        }
    }
}
