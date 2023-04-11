 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketGun : MonoBehaviour
{
    [SerializeReference] private int price;
    [SerializeReference] private string name;
    [SerializeReference] private int maxAmmo;
    [SerializeReference] private int loadedAmmo;
    [SerializeReference] private float fireRate;
    [SerializeReference] private float shotWidth;
    [SerializeReference] private int damage;
    [SerializeReference] private bool auto; 
    [SerializeReference] private AudioClip fireSound;
    [SerializeReference] private AudioClip emptySound;
    // Start is called before the first frame update
    public void Hit()
    {
        if (PlayerPrefs.HasKey("Score") && PlayerPrefs.GetInt("Score") >= price)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerGun>().SetGun(new Gun(name, maxAmmo, loadedAmmo,
                fireRate, shotWidth, damage, auto, fireSound, emptySound));
        }
    }

}
