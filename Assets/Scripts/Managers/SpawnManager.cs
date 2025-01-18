using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private TacticGrid grid;



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
        if (state == GameState.Payer)
        {
            Debug.Log("Start Menu");
            SpawnPlayerRandomGRidPosition();
            // GameManager.Instance.UpdateGameState(GameState.Payer);
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnPlayerRandomGRidPosition()
    {
        Vector3 randomPosition = grid.GetRandomGridWorldPosition();
        Instantiate(player, randomPosition, Quaternion.identity);
    }
}
