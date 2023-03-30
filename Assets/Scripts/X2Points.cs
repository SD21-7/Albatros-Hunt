using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class X2Points : MonoBehaviour
{
    public PlayerGun playerGun;

    public void x2Points()
    {
        playerGun = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGun>();
        playerGun.x2 = true;
        Destroy(gameObject);
    }
}
