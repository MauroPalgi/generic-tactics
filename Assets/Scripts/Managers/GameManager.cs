using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    private void Start()
    {
        UpdateGameState(GameState.StartMenu);
    }
    private void Awake()
    {
        Instance = this;
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.StartMenu:
                break;
            case GameState.Payer:
                break;
            case GameState.Victory:
                break;
            case GameState.Lose:
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);

        }
        OnGameStateChanged?.Invoke(newState);
    }
}


public enum GameState
{
    StartMenu,
    Payer,
    Victory,
    Lose
}
