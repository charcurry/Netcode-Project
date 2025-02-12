using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public int score1;
    public int score2;

    // Start is called before the first frame update
    void Start()
    {
        score1 = 0;
        score2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score1 + " - " + score2;
    }

    public void GoalScored(bool player1)
    {
        if (player1)
        {
            score1++;
        }
        else if (!player1)
        {
            score2++;
        }
    }
}
