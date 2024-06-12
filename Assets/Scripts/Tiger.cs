using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger : Animal
{
    protected override void updateAnimalState(AnimalState animalState)
    {
        this.animalState = animalState;
        if(animalState == AnimalState.WALKING){
            anim.SetBool("isWalking", true);
            anim.SetBool("isMakingSound", false);
            anim.SetBool("isRunning", false);
            Debug.Log("Tiger walk");
        }
        else if(animalState == AnimalState.IDLE){
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isMakingSound", false);
            Debug.Log("Tiger idle");
        }
        else if(animalState == AnimalState.RUNNING){
            anim.SetBool("isRunning", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isMakingSound", false);
        }
        else if(animalState == AnimalState.MAKESOUND){
            anim.SetBool("isMakingSound", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
        }
    }
}
