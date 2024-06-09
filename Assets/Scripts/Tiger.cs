using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger : Animal
{
    new protected void updateAnimalState(AnimalState animalState)
    {
        this.animalState = animalState;
        if(animalState == AnimalState.WALKING){
            anim.SetBool("isWalking", true);
            anim.SetBool("isMakingSound", false);
            anim.SetBool("isRunning", false);
        }
        else if(animalState == AnimalState.IDLE){
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isMakingSound", false);
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
