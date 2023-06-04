using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public float fadeSpeed; // Скорость затемнения

    private Image fadePanel;
    [SerializeField] private Canvas deadCanvas;
    [SerializeField] private Canvas finalCanvas;
    [SerializeField] private Transform vrCamera;
    public bool isFading = false;


    private void Start()
    {
        deadCanvas.enabled = false;
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
                Time.timeScale = 0f;
                deadCanvas.enabled = true;
                deadCanvas.transform.position = vrCamera.position + vrCamera.forward * 1;
                deadCanvas.transform.rotation = Quaternion.LookRotation(vrCamera.forward);
                finalCanvas.transform.position = vrCamera.position + vrCamera.forward * 1;
                finalCanvas.transform.rotation = Quaternion.LookRotation(vrCamera.forward);
            }
        }
    }
}