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

        if (bullTextObject != null)
        {
            TextMeshProUGUI bullText = bullTextObject.GetComponent<TextMeshProUGUI>();

            if (bullText != null)
            {
                bullText.text = "Fakta Banteng:\n" +
                        "- Banteng adalah hewan mamalia herbivora yang mirip dengan sapi.\n" +
                        "- Mereka sering ditemukan di padang rumput dan hutan tropis.\n" +
                        "- Banteng memiliki tanduk yang besar dan kuat.\n" +
                        "- Mereka adalah hewan yang sangat kuat dan tangguh.\n" +
                        "- Keluarga: Bovidae (suku sapi-sapian).";
                Debug.Log("Text component found and text set successfully.");
            }
            else
            {
                Debug.LogError("Text component not found on Bull Fact GameObject.");
            }
        }
    }
}
