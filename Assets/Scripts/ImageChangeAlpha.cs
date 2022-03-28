using UnityEngine;
using UnityEngine.UI;

public class ImageChangeAlpha : MonoBehaviour
{
    private bool _activateEventEnd = true;

    [SerializeField] GameObject canvasImage;
    [SerializeField] private float CunvasImageAlphaColor = 1f;


    void Start()
    {
        canvasImage.SetActive(true);
        canvasImage.GetComponent<Image>().color = new Color(0f, 0f, 0f, CunvasImageAlphaColor);
    }

    void FixedUpdate()
    {
        if (_activateEventEnd)
        {
            CunvasImageAlphaColor = canvasImage.GetComponent<Image>().color.a;
            canvasImage.GetComponent<Image>().color = new Color(0f, 0f, 0f, (CunvasImageAlphaColor -= 0.008f));
        }
        if(CunvasImageAlphaColor <= 0 && _activateEventEnd)
        {
            _activateEventEnd = false;
            canvasImage.SetActive(false);
        }
    }
}
