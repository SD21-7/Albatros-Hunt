using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using UnityEngine.UI;


public class PlayerGun : MonoBehaviour
{
    private SpriteRenderer sr;
    private Gun gun;

    public bool canFire = true;
    private float fireDown;
    private float reloadDown;
    [SerializeField] private float x2Timer;
    [SerializeField] private AudioSource cameraAudioObject;

    [Header("Power Ups")] private X2Points x2Points;
    public bool x2 = false;
    public float x2timer = 5f;
    public GameObject x2Image;

    public bool UnAmmo = false;
    public float UnAmmoTimer = 5f;
    public GameObject UnAmmoImage;
    
    public TextMeshProUGUI ammoText;
    private Camera _camera;

    public Gun GetGun()
    {
        return gun;
    }

    //BroadcastMessage("UpdateGun", SendMessageOptions.DontRequireReceiver);
    public void UpdateGun()
    {
        Gun newGun = GunDict.Guns[PlayerPrefs.GetString("Gun")];
        if (newGun == null) 
        {
            Debug.LogError("Gun is null");
            return;
        }
        gun = newGun;
        gun.SetGunAudio(cameraAudioObject);
        Debug.Log("new gun is" + gun.Name);
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Score", 99999);
        _camera = Camera.main;
        UpdateGun();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerPrefs.GetInt("Score"));
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

        if (UnAmmo)
        {
            if (gun.LoadedAmmo < gun.MaxAmmo)
            {
                gun.ChangeAmmo(gun.MaxAmmo);
            }

            sr = UnAmmoImage.GetComponent<SpriteRenderer>();
            UnAmmoTimer -= Time.deltaTime;
            sr.enabled = true;
            if (UnAmmoTimer <= 0)
            {
                UnAmmoTimer = 5f;
                UnAmmo = false;
                sr.enabled = false;
            }
        }
        ammoText.text = "Bullets:" + gun.LoadedAmmo + "/" + gun.MaxAmmo;
    }

    private void Fire()
    {
        //if (canFire)
        if(true)
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

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit)
                hit.collider.gameObject.SendMessage("Hit", gun.Damage, SendMessageOptions.DontRequireReceiver);
        }
    }
}

public class Gun
{
    public string Name { get; }
    public int MaxAmmo { get; }
    public int LoadedAmmo { get; set; }
    public float FireRate { get; }
    public float ShotWidth { get; }
    public int Damage { get; }
    public bool Auto { get; }
    private AudioClip FireSound { get; }
    private AudioClip EmptySound { get; }
    private AudioSource gunAudio;

    public Gun(string name, int maxAmmo, int loadedAmmo, float fireRate, float shotWidth, int damage, bool auto,
        [CanBeNull] string fireSound, [CanBeNull] string emptySound)
    {
        Name = name;
        MaxAmmo = maxAmmo;
        LoadedAmmo = loadedAmmo;
        FireRate = fireRate;
        ShotWidth = shotWidth;
        Damage = damage;
        Auto = auto;
        if(fireSound != null)
            FireSound = Resources.Load<AudioClip>("Audio/" + fireSound);
        if(emptySound != null)
            EmptySound = Resources.Load<AudioClip>("Audio/" + emptySound);
    }

    public void SetGunAudio(AudioSource audio)
    {
        gunAudio = audio;
    }

    public void ChangeAmmo(int num)
    {
        Debug.Log("Changing ammo by " + num);
        LoadedAmmo += num;
        if (LoadedAmmo > MaxAmmo) LoadedAmmo = MaxAmmo;
    }

    public void SetAmmo(int num)
    {
        LoadedAmmo = num;
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