using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    
    private Gun gun;
    private float fireDown;
    private float reloadDown;

    public Gun GetGun() { return gun; }

    public void SetGun(Gun gun)
    {
        if(gun != null) this.gun = gun;
        BroadcastMessage("UpdateGun");
    }

    // Start is called before the first frame update
    void Start()
    {
        //SetGun(new Gun("M4", 30, 30, 0.1f, 0.1f, 25, true));
        //SetGun(new Gun("Revolver", 6, 6, 1, 0.1f, 100, false));
        SetGun(new Gun("Hunting Rifle", 4, 4, 1.2f, 0.1f, 175, false, null, null));

    }

    // Update is called once per frame
    void Update()
    {
        if (fireDown > 0) fireDown -= Time.deltaTime;
        if (reloadDown > 0) reloadDown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0) && !gun.Auto) Fire();
        if (Input.GetKey(KeyCode.Mouse0) && gun.Auto) Fire();
    }
    
    private void Fire()
    {
        if (fireDown > 0) return;
        if (gun.LoadedAmmo <= 0)
        {
            gun.playEmptySound();
            return;
        }
        
        gun.playFireSound();
        fireDown = gun.FireRate;
        gun.ChangeAmmo(-1);
        BroadcastMessage("Fired");
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit)
        {
            hit.collider.gameObject.SendMessage("Hit", gun.Damage);
        }
    }
}

public class Gun
{
    public string Name { get;}
    public int MaxAmmo { get;}
    public int LoadedAmmo { get; private set; }
    public float FireRate { get;}
    public float ShotWidth { get;}
    public int Damage { get;}
    public bool Auto { get; }
    public AudioSource FireSound { get; }
    public AudioSource EmptySound { get; }
    public Gun(string name, int maxAmmo, int loadedAmmo, float fireRate, float shotWidth, int damage, bool auto, [CanBeNull] AudioSource fireSound, [CanBeNull] AudioSource emptySound)
    {
        Name = name;
        MaxAmmo = maxAmmo;
        LoadedAmmo = loadedAmmo;
        FireRate = fireRate;
        ShotWidth = shotWidth;
        Damage = damage;
        Auto = auto;
        FireSound = fireSound;
        EmptySound = emptySound;
    }

    public void ChangeAmmo(int num)
    {
        LoadedAmmo += num;
        if (LoadedAmmo > MaxAmmo) LoadedAmmo = MaxAmmo;
    }

    public void playFireSound()
    {
        if (FireSound != null)
        {
            FireSound.Play();
        }
    }

    public void playEmptySound()
    {
        Debug.Log("click");
    }
}
