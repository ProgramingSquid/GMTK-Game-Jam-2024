using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnDeath : MonoBehaviour
{
    public void RestGame(float delay)
    {
        FindObjectOfType<ScoreDisplay>().PlayerDeath(delay);
    }
}
