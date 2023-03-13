using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableEnemy : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private int score;
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
        gameObject.SendMessage("Died", SendMessageOptions.DontRequireReceiver);
        GameObject.FindGameObjectWithTag("Player").SendMessage("EnemyDied", score);
    }
}
