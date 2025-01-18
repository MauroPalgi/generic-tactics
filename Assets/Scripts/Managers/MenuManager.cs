using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        GameManager.OnGameStateChanged += handleOnGameSateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= handleOnGameSateChanged;

    }

    private void handleOnGameSateChanged(GameState state)
    {
        if (state == GameState.StartMenu)
        {
            Debug.Log("Start Menu");
            GameManager.Instance.UpdateGameState(GameState.Payer);
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
