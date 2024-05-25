using System.Collections.Generic; //Necessary for Lists.
using UnityEngine;
 
public class InteractableAnimalDistanceList : MonoBehaviour
{
    public List<InteractableAnimal> interactables = new List<InteractableAnimal>();
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
        if (other.tag == ("Animal"))
        {
            interactables.Add(other.GetComponent<InteractableAnimal>());
        }
    }
 
    void OnTriggerExit(Collider other)
    {
        if (other.tag == ("Animal"))
        {
            interactables.Remove(other.GetComponent<InteractableAnimal>());
        }
    }
}