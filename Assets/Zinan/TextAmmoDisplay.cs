using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAmmoDisplay : MonoBehaviour
{
    [SerializeField] private PlayerGun player;
    [SerializeField] private TextMeshPro text;
    private Gun gun;

    public void UpdateGun()
    {
        gun = player.GetGun();
        text.text = GetText();
    }
    public void Fired()
    {

        text.text = GetText();
    }

    private string GetText() { return gun.LoadedAmmo + "/" + gun.MaxAmmo;}
}
