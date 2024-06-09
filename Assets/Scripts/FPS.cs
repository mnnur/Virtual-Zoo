using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    TextMeshProUGUI fpsText;
    public int refreshRate = 10;
    int frameCounter;
    float totalTime;
    // Start is called before the first frame update
    void Start()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
        frameCounter = 0;
        totalTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(frameCounter == refreshRate){
            float averagefps = (1.0f / (totalTime / refreshRate));
            fpsText.text = averagefps.ToString("F1");
            frameCounter = 0;
            totalTime = 0;
        }else {
            totalTime += Time.deltaTime;
            frameCounter++;
        }
    }
}
