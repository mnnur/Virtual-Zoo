using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Girrafe_Information : MonoBehaviour
{
    void Start()
    {
        GameObject girrafeTextObject = GameObject.Find("Girrafe Fact");
        if (girrafeTextObject != null) {
            Debug.Log("Aya Kaluar!");
        }
        else {
            Debug.LogError("EWEH Kaluar");
        }

        if (girrafeTextObject != null)
        {
            TextMeshProUGUI girrafeText = girrafeTextObject.GetComponent<TextMeshProUGUI>();

            if (girrafeText != null)
            {
                girrafeText.text = "Giraffe Facts:\n" +
                            "- Jerapah adalah hewan terpanjang di daratan.\n" +
                            "- Mereka memiliki leher yang sangat panjang.\n" +
                            "- Jerapah memakan daun dan buah-buahan.\n" +
                            "- Jeritan mereka bisa terdengar hingga jarak satu mil!\n" +
                            "- Jerapah adalah hewan yang sangat damai dan tidak suka berkelahi.\n" +
                            "- Leher jerapah memiliki sekitar 7 tulang leher, sama dengan jumlah tulang leher manusia.\n" +
                            "- Anak jerapah yang baru lahir biasanya sudah memiliki tinggi sekitar 2 meter, loh!";
                Debug.Log("Text component found and text set successfully.");
            }
            else
            {
                Debug.LogError("Text component not found on Girrafe Fact GameObject.");
            }
        }
        else
        {
            Debug.LogError("Girrafe Fact GameObject not found.");
        }
    }
}
