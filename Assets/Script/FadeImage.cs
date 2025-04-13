using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.Serialization;

public class FadeImage : MonoBehaviour
{
    [FormerlySerializedAs("canvas")] [SerializeField] private Image image;
    [SerializeField] Timer referenceTimer;
    private float FadeTime = 0.1f;

    void Start()
    {
        if (image == null)
        {
            Debug.LogError("L'immagine da sbiadire non è stata assegnata!");
            enabled = false;
            return;
        }
        
        Color c = image.color;
        c.a = 0f;
        image.color = c;

        if (referenceTimer.RemainingTime <= 30f)
        {
            StartCoroutine(FadeSequence());    
        }
    }

    IEnumerator FadeSequence()
    {
        while (true)
        {

            if (image.color.a <= 0f)
            {
                Color c = image.color;

                c.a += Time.deltaTime;
                image.color = c;
            }
            else if (image.color.a >= 1f)
            {
                Color c = image.color;

                c.a -= FadeTime * Time.deltaTime;
                image.color = c;
            }
        }
    }
}