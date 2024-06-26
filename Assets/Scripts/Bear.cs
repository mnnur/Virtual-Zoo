using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Animal
{
    // Start is called before the first frame update
    protected override void updateAnimalState(AnimalState animalState)
    {
        this.animalState = animalState;
        if(animalState == AnimalState.WALKING){
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", false);
            Debug.Log("Tiger walk");
        }
        else if(animalState == AnimalState.IDLE){
            Debug.Log("Tiger idle");
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
        }
        else if(animalState == AnimalState.RUNNING){
            anim.SetBool("isRunning", true);
            anim.SetBool("isWalking", false);
        }
    }
}
