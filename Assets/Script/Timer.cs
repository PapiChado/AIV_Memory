using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Timer : MonoBehaviour
{
    [SerializeField] GameObject loserUI;
    [SerializeField] TextMeshProUGUI timerText;
    [FormerlySerializedAs("remainingTime")] [SerializeField] public float RemainingTime;

    void Update()
    {
        loserUI.SetActive(false);
        RemainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(RemainingTime / 60);
        int seconds = Mathf.FloorToInt(RemainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if(RemainingTime<= 0)
        {
            RemainingTime = 0;
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
