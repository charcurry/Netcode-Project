using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.Netcode;

public class GoalManager : NetworkBehaviour
{
    public TextMeshProUGUI scoreText;

    public Canvas canvas;

    public NetworkManager networkManager;

    public GameObject ballPrefab;

    [SerializeField] private GameObject currentBall;

    private Vector3 ballSpawnPosition;

    public NetworkVariable<int> score1 = new NetworkVariable<int>(0);
    public NetworkVariable<int> score2 = new NetworkVariable<int>(0);

    // Start is called before the first frame update
    void Start()
    {
        if (networkManager.IsServer)
        {
            score1.Value = 0;
            score2.Value = 0;
        }

        if (IsOwner)
        {
            canvas.enabled = true;
        }
        else
        {
            canvas.enabled = false;
        }

        currentBall = GameObject.FindGameObjectWithTag("Ball");
        if (currentBall != null)
        {
            ballSpawnPosition = currentBall.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score1.Value + " - " + score2.Value;
    }

    [ServerRpc]
    public void GoalScoredServerRpc(bool player1)
    {
        if (player1)
        {
            score1.Value++;
        }
        else if (!player1)
        {
            score2.Value++;
        }

        RespawnBall();
    }

    [ClientRpc]
    private void GoalScoredClientRpc(bool player1)
    {
        if (player1)
        {
            score1.Value++;
        }
        else if (!player1)
        {
            score2.Value++;
        }
    }

    public void GoalScored(bool player1)
    {
        if (networkManager.IsServer)
        {
            GoalScoredServerRpc(player1);
        }
        else
        {
            GoalScoredClientRpc(player1);
        }
    }

    private void RespawnBall()
    {
        if (currentBall != null)
        {
            Destroy(currentBall);
        }
        currentBall = Instantiate(ballPrefab, ballSpawnPosition, Quaternion.identity);
        currentBall.GetComponent<NetworkObject>().Spawn();
    }
}
