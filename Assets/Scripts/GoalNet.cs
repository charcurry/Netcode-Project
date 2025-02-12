using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalNet : MonoBehaviour
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
            goalManager.GoalScored(player1);
        }
    }
}
