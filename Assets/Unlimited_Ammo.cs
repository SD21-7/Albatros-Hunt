using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlimited_Ammo : MonoBehaviour
{
    public PlayerGun playerGun;
    
    public void UnlimitedAmmo()
    {
        playerGun = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGun>();
        playerGun.UnAmmo = true;
        Destroy(gameObject);
    }
}
