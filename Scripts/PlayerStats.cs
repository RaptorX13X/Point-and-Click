using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerStats : MonoBehaviour
{
    public int coins;
    public bool weapon;
    public bool enchantedWeapon;
    public bool alive;
    [SerializeField] private Animator anim;
    private PlayerController controller;
    [SerializeField] UIManager UIThingy;
    public bool victory;
    private void Start()
    {
        coins = 0;
        weapon = false;
        enchantedWeapon = false;
        alive = true;
        controller = GetComponent<PlayerController>();
    }

    public void AddCoins()
    {
        coins += Random.Range(20, 50);
    }
    public void AddManyCoins()
    {
        coins += Random.Range(500, 700);
    }

    public void AddWeapon()
    {
        weapon = true;
        StartCoroutine(UIThingy.WeaponAcquired());
    }

    public void EnchantWeapon()
    {
        enchantedWeapon = true;
        StartCoroutine(UIThingy.WeaponEnchanted());
    }

    public void Die()
    {
        alive = false;
        anim.SetTrigger("Death");
        controller.enabled = !controller.enabled;
    }
    
}
