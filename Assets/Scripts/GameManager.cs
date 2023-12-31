using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float Score = 0;
    public float CurrentScore = 0;
    public bool PowerUp = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(Instance);
    }

    public void LevelCompleted()
    {
        CurrentScore = Score;
    }

    public void IncrementScore()
    {
        Score += 50;
    }

    public void ResetLevelScore()
    {
        Score = CurrentScore;
    }

    public void ResetGameScore()
    {
        Score = 0;
        CurrentScore = 0;
    }

    public void JumpPowerUpOn()
    {
        PowerUp = true;
    }

    public void JumpPowerUpOff()
    {
        PowerUp = false;
    }
}
