using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    private SpriteRenderer sr;
    
    private Gun gun;
    private float fireDown;
    private float reloadDown;
    [SerializeField] private float x2Timer;
    [SerializeField] private AudioSource cameraAudioObject;
    [SerializeField] private Sounds sounds;

    [Header("Power Ups")] private X2Points x2Points;
    public bool x2 = false;
    public float x2timer = 5f;
    public GameObject x2Image;

    public Gun GetGun()
    {
        return gun;
    }

    public void SetGun(Gun gun)
    {
        if (gun != null) this.gun = gun;
        this.gun.SetGunAudio(cameraAudioObject);
        BroadcastMessage("UpdateGun", SendMessageOptions.DontRequireReceiver);
    }

    // Start is called before the first frame update
    void Start()
    {
        //SetGun(new Gun("M4", 30, 30, 0.15f, 0.1f, 75, true, sounds.shot, null));
        SetGun(new Gun("Revolver", 9999, 9999, 1, 0.1f, 100, false, sounds.shot, null)); //max and loaded ammo 6
        //SetGun(new Gun("Hunting Rifle", 9999, 9999, 0.05f, 0.1f, 175, true, sounds.shot, null));
        // SetGun(new Gun("Admin Gun", 9999, 9999, 0.01f, 0.1f, 100000, true, sounds.shot, null));
    }

    // Update is called once per frame
    void Update()
    {
        if (fireDown > 0) fireDown -= Time.deltaTime;
        if (reloadDown > 0) reloadDown -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && !gun.Auto) Fire();
        if (Input.GetKey(KeyCode.Mouse0) && gun.Auto) Fire();

        if (x2)
        {
            sr = x2Image.GetComponent<SpriteRenderer>();
            x2timer -= Time.deltaTime;
            sr.enabled = true;
            if (x2timer <= 0)
            {
                x2timer = 5f;
                x2 = false;
                sr.enabled = false;
            }
        }
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
        BroadcastMessage("Fired", SendMessageOptions.DontRequireReceiver);


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit)
        {
            if (hit.collider.gameObject.CompareTag("score_x2"))
            {
                x2Points = hit.collider.gameObject.GetComponent<X2Points>();
                // hit.collider.gameObject.SendMessage("Hit", gun.Damage, SendMessageOptions.DontRequireReceiver);
                hit.collider.gameObject.SendMessage("x2Points", SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                hit.collider.gameObject.SendMessage("Hit", gun.Damage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}

public class Gun
{
    public string Name { get; }
    public int MaxAmmo { get; }
    public int LoadedAmmo { get; private set; }
    public float FireRate { get; }
    public float ShotWidth { get; }
    public int Damage { get; }
    public bool Auto { get; }
    private AudioClip FireSound { get; }
    private AudioClip EmptySound { get; }
    private AudioSource gunAudio;

    public Gun(string name, int maxAmmo, int loadedAmmo, float fireRate, float shotWidth, int damage, bool auto,
        [CanBeNull] AudioClip fireSound, [CanBeNull] AudioClip emptySound)
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

    public void SetGunAudio(AudioSource audio)
    {
        gunAudio = audio;
    }

    public void ChangeAmmo(int num)
    {
        LoadedAmmo += num;
        if (LoadedAmmo > MaxAmmo) LoadedAmmo = MaxAmmo;
    }

    public void playFireSound()
    {
        // Debug.Log(FireSound);
        if (FireSound != null)
        {
            gunAudio.clip = FireSound;
            gunAudio.Play();
        }
    }

    public void playEmptySound()
    {
        if (EmptySound != null)
        {
            gunAudio.clip = EmptySound;
            gunAudio.Play();
        }
    }
}