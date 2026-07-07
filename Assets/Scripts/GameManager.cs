using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Ball / Player")]
    public BallController ballController;
    public Transform xrOrigin;
    public Transform startPosition;

    [Header("Game Settings")]
    public float roundTimeSeconds = 60f;

    [Header("UI (HUD)")]
    public TMP_Text scoreText;
    public TMP_Text timeText;

    [Header("Menu UI")]
    public GameObject menuPanel;          // GameOverPanel / main menu
    public TextMeshProUGUI titleText;     // "Beer Pong" / "You Win" / "You Lose"

    [Header("Cups")]
    public GameObject[] cups;             // cup root objects

    int score = 0;
    float timeRemaining;
    bool isGameRunning = false;

    void Start()
    {
        // Start in menu mode
        timeRemaining = roundTimeSeconds;
        UpdateScoreUI();
        UpdateTimeUI(timeRemaining);

        if (titleText != null)
            titleText.text = "Beer Pong";

        if (menuPanel != null)
            menuPanel.SetActive(true);   // show menu at launch
    }

    void Update()
    {
        if (!isGameRunning) return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            UpdateTimeUI(timeRemaining);
            LoseGame();
            return;
        }

        UpdateTimeUI(timeRemaining);
    }

    // Called by Start button AND Retry button
    public void StartGame()
    {
        Debug.Log("StartGame CALLED");

        // Teleport player to start
        if (xrOrigin != null && startPosition != null)
        {
            xrOrigin.position = startPosition.position;
            xrOrigin.rotation = startPosition.rotation;
        }

        // Reset game state
        score = 0;
        timeRemaining = roundTimeSeconds;
        isGameRunning = true;

        ResetCups();

        if (ballController != null)
            ballController.ResetBall();

        UpdateScoreUI();
        UpdateTimeUI(timeRemaining);

        if (menuPanel != null)
            menuPanel.SetActive(false);  // hide menu
    }

    public void AddScore(int amount)
    {
        if (!isGameRunning) return;

        score += amount;
        UpdateScoreUI();

        // Reset the ball every time you score
        if (ballController != null)
            ballController.ResetBall();

        // Win as soon as all cups have been scored
        // Make sure 'cups' size in the Inspector equals the number of cups (e.g. 6)
        if (score >= cups.Length)
        {
            WinGame();
        }
    }

    // Called by Retry button
    public void RetryGame()
    {
        if (titleText != null)
            titleText.text = "Beer Pong";

        StartGame();
    }

    void WinGame()
    {
        isGameRunning = false;

        if (menuPanel != null)
            menuPanel.SetActive(true);

        if (titleText != null)
            titleText.text = "You Win";
    }

    void LoseGame()
    {
        isGameRunning = false;

        if (menuPanel != null)
            menuPanel.SetActive(true);

        if (titleText != null)
            titleText.text = "You Lose";
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    void UpdateTimeUI(float t)
    {
        if (timeText != null)
            timeText.text = "Time: " + Mathf.CeilToInt(t);
    }

    void ResetCups()
    {
        if (cups == null) return;

        foreach (GameObject cup in cups)
        {
            if (cup != null)
                cup.SetActive(true);
        }
    }

    bool AllCupsCleared()
    {
        if (cups == null || cups.Length == 0)
            return false;

        foreach (GameObject cup in cups)
        {
            if (cup != null && cup.activeSelf)
                return false;
        }

        return true;
    }
}