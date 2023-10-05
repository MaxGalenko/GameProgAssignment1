using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float Score = 0;
    public float CurrentScore = 0;

    void Start()
    {
        CurrentScore = Score;
    }

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

    public void IncrementScore()
    {
        Score += 50;
    }
}
