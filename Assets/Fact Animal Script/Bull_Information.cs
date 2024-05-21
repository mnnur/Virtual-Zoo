using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bull_Information : MonoBehaviour
{
    void Start()
    {
        GameObject bullTextObject = GameObject.Find("Bull Fact");
        if (bullTextObject != null) {
            Debug.Log("Aya Kaluar!");
        }
        else {
            Debug.LogError("EWEH Kaluar");
        }

        if (bullTextObject != null)
        {
            TextMeshProUGUI bullText = bullTextObject.GetComponent<TextMeshProUGUI>();

            if (bullText != null)
            {
                bullText.text = "INI BANTENG PDIP";
                Debug.Log("Text component found and text set successfully.");
            }
            else
            {
                Debug.LogError("Text component not found on Bull Fact GameObject.");
            }
        }
        else
        {
            Debug.LogError("Bull Fact GameObject not found.");
        }
    }
}
