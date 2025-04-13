using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] GameObject loserUI;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] private float remainingTime;

    void Update()
    {
        loserUI.SetActive(false);
        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if(remainingTime<= 0)
        {
            remainingTime = 0;
            timerText.text = ("00:00");
            timerEnded();
        }
        

    }

    void timerEnded()
    {
        //do your stuff here.
        loserUI.SetActive(true);
    }


}
