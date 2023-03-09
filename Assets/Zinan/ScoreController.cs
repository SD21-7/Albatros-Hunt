using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int Score { private get; set; }
    [SerializeField] private TextMeshPro text;

    public void EnemyDied(int modifyBy)
    {
        Score += modifyBy; UpdateScore();
    }

    // Update is called once per frame
    void UpdateScore()
    {
        text.text = Score.ToString();
    }
}
