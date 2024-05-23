using System.Collections.Generic; //Necessary for Lists.
using UnityEngine;
 
public class InteractableDistanceList : MonoBehaviour
{
    public List<Interactable> interactables = new List<Interactable>();
    public float lowestDistance;
 
    private void Update()
    {
        if (interactables.Count == 0)
        {
            lowestDistance = 0f;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Interactable"))
        {
            interactables.Add(other.GetComponent<Interactable>());
        }
    }
 
    void OnTriggerExit(Collider other)
    {
        if (other.tag == ("Interactable"))
        {
            interactables.Remove(other.GetComponent<Interactable>());
        }
    }
}