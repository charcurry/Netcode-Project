using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GoalNet : NetworkBehaviour
{
    public GoalManager goalManager;

    public bool player1;

    public void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (IsServer)
            {
                goalManager.GoalScoredServerRpc(player1);
            }
        }
    }
}
