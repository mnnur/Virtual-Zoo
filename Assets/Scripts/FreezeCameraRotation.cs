using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeCameraRotation : MonoBehaviour
{
    // Start is called before the first frame update
    private Quaternion frozenRotation;
    private GameObject mainCamera;

    public void freeze()
    {
        // Find the main camera GameObject
        mainCamera = GameObject.Find("Main Camera");

        if (mainCamera != null)
        {
            Debug.Log("Main camera found!");

            // Initialize the frozen rotation to the current rotation of the camera
            frozenRotation = mainCamera.transform.rotation;
        }
        else
        {
            Debug.LogError("Main camera not found!");
        }
    }

    void Update()
    {
        if (mainCamera != null)
        {
            // Set the rotation of the camera to the frozen rotation every frame
            mainCamera.transform.rotation = frozenRotation;
        }
    }

    public void UnfreezeRotation()
    {
        if (mainCamera != null)
        {
            // Restore the original rotation of the camera
            mainCamera.transform.rotation = frozenRotation;
        }
    }
}