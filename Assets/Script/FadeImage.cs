using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeImage : MonoBehaviour
{
    [SerializeField] private Image canvas;
    [SerializeField] Timer referenceTimer;

    void Start()
    {
        if (canvas == null)
        {
            Debug.LogError("L'immagine da sbiadire non è stata assegnata!");
            enabled = false;
            return;
        }
        
        Color c = canvas.color;
        c.a = 0;
        canvas.color = c;

        if (referenceTimer.RemainingTime <= 30.0f)
        {
            StartCoroutine(FadeSequence());    
        }
    }

    IEnumerator FadeSequence()
    {
        while (true)
        {
            if (canvas.color.a <= 0f)
            {
                Color c = canvas.color;
                c.a += Time.deltaTime;
                canvas.color = c;
            }
            else if (canvas.color.a >= 1f)
            {
                Color c = canvas.color;
                c.a -= Time.deltaTime;
                canvas.color = c;
            }
        }
    }
}