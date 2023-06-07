using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelState : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeDisplay;
    [SerializeField] private TextMeshProUGUI ballsDisplay;
    [SerializeField] private Transform player;
    [SerializeField] private Image endBackground;
    [SerializeField] private TextMeshProUGUI endTimeDisplay;
    [SerializeField] private float timeToEndScreen;
    [SerializeField] private float fadeTime;
    [SerializeField] private float fadeMaxAlpha;
    void Start()
    {
        Transform endScreen = transform.Find(endScreenName);
        endScreen.gameObject.SetActive(false);
        startTime = Time.time;
        var allBalls = GameObject.FindGameObjectsWithTag("Ball");
        ballsCount = allBalls.Length;
        ballsDisplay.text = ballsCount.ToString();
        StartCoroutine(showEndScreenAfterTime());
    }

    void Update()
    {
        playTime = TimeSpan.FromSeconds(Time.time - startTime);
        timeDisplay.text = playTime.ToString(@"m\:ss\.ff");

        if (isWaitingForRestart && Input.GetButtonDown("Strike"))
            restart();
    }

    public void score()
    {
        ballsCount -= 1;
        ballsDisplay.text = ballsCount.ToString();

        if(ballsCount == 0)
            StartCoroutine(showEndScreenAfterTime());
    }

    public void restart()
    {
        Debug.Log("Restart");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    IEnumerator showEndScreenAfterTime()
    {
        yield return new WaitForSeconds(timeToEndScreen);

        for(int i = 0; i < player.childCount; i++)
            player.GetChild(i).gameObject.SetActive(false);
        player.gameObject.GetComponent<PlayerBehaviour>().enabled = false;
        transform.Find(endScreenName).gameObject.SetActive(true);
        endBackground.gameObject.SetActive(true);
        transform.Find("Stats").gameObject.SetActive(false);
        endTimeDisplay.gameObject.SetActive(true);
        endTimeDisplay.text = "Time: " + playTime.ToString(@"m\:ss\.ff");
        int steps = 20;
        for (float progress = 0f; progress <= 1f; progress += 1.0f/steps)
        {
            Color backgroundColor = endBackground.color;
            backgroundColor.a = progress * fadeMaxAlpha;
            endBackground.color = backgroundColor;

            yield return new WaitForSeconds(fadeTime/steps);
        }
        isWaitingForRestart = true;
        yield return null;
    }

    private int ballsCount;
    private float startTime;
    private TimeSpan playTime;
    private bool isWaitingForRestart = false;

    private const string endScreenName = "EndScreen";
}
