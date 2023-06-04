using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public float fadeSpeed; // Скорость затемнения

    private Image fadePanel;

    public bool isFading = false;


    private void Start()
    {
        //deadCanvas.enabled = false; 
        fadePanel = GetComponentInChildren<Image>();
    }

    public void StartFade()
    {
        isFading = true;
    }

    private void Update()
    {
        if (isFading)
        {
            fadePanel.color = Color.Lerp(fadePanel.color, Color.black, fadeSpeed * Time.deltaTime);

            // Проверка, когда затемнение достигнет полной непрозрачности (черного цвета)
            if (fadePanel.color.a >= 0.99f)
            {
                isFading = false;
                
            }
        }
    }
}