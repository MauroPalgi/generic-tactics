using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject obstacle;
    public static ObstacleManager Instance;

    [SerializeField]
    private TacticGrid grid;
    [SerializeField]
    private int amount = 10;

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= handleOnGameSateChanged;

    }


    private void Awake()
    {
        Instance = this;
        GameManager.OnGameStateChanged += handleOnGameSateChanged;
    }
    void Start()
    {

    }
    private void handleOnGameSateChanged(GameState state)
    {
        if (state == GameState.Payer)
        {
            Debug.Log("Start Menu");
            SpawnObstacleRandomGridPosition();
            // GameManager.Instance.UpdateGameState(GameState.Payer);
        }
    }

    private void SpawnObstacleRandomGridPosition()
    {
        for (int i = 0; i < amount; i++)
        {

            Vector3 randomPosition = grid.GetRandomGridWorldPosition();
            Debug.Log($"Obstacle {i} Position: {randomPosition}");
            Instantiate(obstacle, randomPosition, Quaternion.identity);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
