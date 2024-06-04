using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] DietType dietType = DietType.OMNIVORE;
    public int satiety = 50;
}
