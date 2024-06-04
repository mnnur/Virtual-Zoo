using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tiger_Information : MonoBehaviour
{
    void Start()
    {
        GameObject tigerTextObject = GameObject.Find("Tiger Fact");
        if (tigerTextObject != null) {
            Debug.Log("Aya Kaluar!");
        }
        else {
            Debug.LogError("EWEH Kaluar");
        }

        if (tigerTextObject != null)
        {
            TextMeshProUGUI tigerText = tigerTextObject.GetComponent<TextMeshProUGUI>();

            if (tigerText != null)
            {
                tigerText.text = "INI HARIMAU RAWR";
                Debug.Log("Text component found and text set successfully.");
            }
            else
            {
                Debug.LogError("Text component not found on Tiger Fact GameObject.");
            }
        }
        else
        {
            Debug.LogError("Tiger Fact GameObject not found.");
        }
    }
}
