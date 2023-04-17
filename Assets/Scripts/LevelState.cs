using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelState : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeDisplay;
    [SerializeField] private TextMeshProUGUI ballsDisplay;
    void Start()
    {
        startTime = Time.time;
        var allBalls = GameObject.FindGameObjectsWithTag("Ball");
        ballsCount = allBalls.Length;
        ballsDisplay.text = ballsCount.ToString();
    }

    void Update()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(Time.time - startTime);
        timeDisplay.text = timeSpan.ToString(@"m\:ss\.ff");
    }

    public void score()
    {
        ballsCount -= 1;
        ballsDisplay.text = ballsCount.ToString();
    }

    private int ballsCount;
    private float startTime;
}
