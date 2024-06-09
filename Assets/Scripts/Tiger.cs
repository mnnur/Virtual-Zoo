using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger : Animal
{
    void updateAnimalState(AnimalState animalState)
    {
        this.animalState = animalState;
        if(animalState == AnimalState.WALKING){
            
        }
        else if(animalState == AnimalState.IDLE){

        }
    }
}
