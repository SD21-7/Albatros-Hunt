using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class X2Points : MonoBehaviour
{
    public PlayerGun playerGun;

    public void Hit(float damage)
    {
        playerGun = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGun>();
        playerGun.x2 = true;
        Destroy(gameObject);
    }
}
