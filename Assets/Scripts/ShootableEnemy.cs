using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableEnemy : MonoBehaviour
{
    public ScoreController scoreController;
    public PlayerGun PlayerGun;
    
    [SerializeField] private int health = 100;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private int score;

    private void Start()
    {
        scoreController = GameObject.Find("GameInit").GetComponent<ScoreController>();
        PlayerGun = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGun>();
    }

    public void Hit(int damage)
    {
        health -= damage;
        if (sr != null)
        {
            sr.color = new Color(1, health / 100f, health / 100f);
        }

        if (health <= 0) Death();
    }

    private void Death()
    {
        if (PlayerGun.x2)
        {
            score *= 2;
            gameObject.SendMessage("Died", SendMessageOptions.DontRequireReceiver);
            scoreController.EnemyDied(score);
        }
        else
        {
            gameObject.SendMessage("Died", SendMessageOptions.DontRequireReceiver);
            scoreController.EnemyDied(score);
        }
    }
}