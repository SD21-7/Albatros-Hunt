 using System;
 using System.Collections;
using System.Collections.Generic;
 using DefaultNamespace;
 using TMPro;
 using UnityEngine;

public class MarketGun : MonoBehaviour
{
    [SerializeReference] private int price;
    [SerializeReference] private string gunName;
    
    [SerializeReference] private SpriteRenderer sr;
    private bool isBought;

    private TextMeshPro text;
    // Start is called before the first frame update
    public void Hit()
    {
        if (PlayerPrefs.HasKey("Score") && PlayerPrefs.GetInt("Score") >= price && !isBought)
        {
            int score = PlayerPrefs.GetInt("Score");
            score -= price;
            PlayerPrefs.SetInt("Score", score);
            PlayerPrefs.SetString("Gun", gunName);
            GameObject.FindWithTag("Player").BroadcastMessage("UpdateGun");
            isBought = true;
            text.text = "<s>" + text.text + "</s>";
            sr.color = Color.gray;
        }
    }

    private void Start()
    {
        text = GetComponent<TextMeshPro>();
        Gun gun = GunDict.Guns[gunName];
        if (gun == null)
        {
            Debug.LogError("Gun is null!");
            throw new NullReferenceException();
        }
        text.text = gun.Name + "<br> price: " + price;
    }
}
