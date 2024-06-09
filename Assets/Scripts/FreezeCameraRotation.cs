using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeCameraRotation : MonoBehaviour
{
    private Transform currentTransform;
    public bool freeze = false;

    void Start()
    {
        currentTransform = transform;
    }

    void Update()
    {
        if(freeze){
            transform.position = currentTransform.position;
            transform.rotation = currentTransform.rotation;
        }else {
            currentTransform = transform;
        }
    }

    public void SetFreeze(bool freeze){
        this.freeze = freeze;
    }
}
