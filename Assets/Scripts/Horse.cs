using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : Animal
{
    protected override void updateAnimalState(AnimalState animalState)
    {
        this.animalState = animalState;
        if(animalState == AnimalState.WALKING){
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", false);
            Debug.Log("Tiger walk");
        }
        else if(animalState == AnimalState.IDLE){
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
            Debug.Log("Tiger idle");
        }
        else if(animalState == AnimalState.RUNNING){
            anim.SetBool("isRunning", true);
            anim.SetBool("isWalking", false);
        }
    }
}
