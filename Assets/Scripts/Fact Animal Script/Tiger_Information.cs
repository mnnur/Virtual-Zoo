using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tiger_Information : MonoBehaviour
{
    public void Start()
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
                tigerText.text = "Fakta Harimau:\n" +
                         "- Harimau adalah hewan karnivora yang menjadi simbol kekuatan dan keanggunan.\n" +
                         "- Mereka adalah pemburu yang tangguh dan biasanya berburu secara soliter.\n" +
                         "- Harimau memiliki garis-garis vertikal hitam di tubuhnya, yang berfungsi sebagai kamoeflas untuk menyembunyikan diri saat berburu.\n" +
                         "- Mereka adalah predator puncak di ekosistem mereka.\n" +
                         "- Harimau adalah hewan yang soliter, kecuali saat berpasangan selama musim kawin.\n" +
                         "- Makanan favorit: Rusa, babi hutan, kijang.\n" +
                         "- Keluarga: Felidae (suku kucing besar).";
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
