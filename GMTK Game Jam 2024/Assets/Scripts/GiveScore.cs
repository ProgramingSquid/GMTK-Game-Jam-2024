using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveScore : MonoBehaviour
{
    public int points = 1;

    public void AddScore()
    {
        ScoreManager.instance.AddScore(points);
    }
}