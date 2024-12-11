using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class GameEvent : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject _hiddenDoor;
    [SerializeField] private GameObject _hiddenRoom;
    [SerializeField] private GameObject _weaponPickUp;
    [SerializeField] private GameObject _weaponInHand;
    [SerializeField] private GameObject weaponParticles;
    [SerializeField] private GameObject randomRoof;
    [SerializeField] private GameObject chest1;
    [SerializeField] private GameObject chest2;
    private PlayerStats pStats;
    [SerializeField] private GameObject bug;
    [SerializeField] private GameObject ladder;
    [SerializeField] UIManager UIThingy;
    
    [Header("Sounds")]
    [SerializeField] private AudioClip chestSound;
    [SerializeField] private AudioClip enchantSound;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip doorSound;
    
    private void Start()
    {
        _hiddenRoom.SetActive(false);
        _weaponPickUp.SetActive(true);
        _weaponInHand.SetActive(false);
        randomRoof.SetActive(true);
        pStats = player.GetComponent<PlayerStats>();
        weaponParticles.SetActive(false);
    }

    public void HiddenDoor()
    {
        _hiddenDoor.transform.DOShakePosition(2f, 0.05f, 50, 90f, false, false);
        _hiddenDoor.transform.DOLocalMoveY( -5, 5).SetDelay(1).SetLoops(0);
        _hiddenRoom.SetActive(true);
        randomRoof.SetActive(false);
        SoundManager.instance.PlaySound(doorSound);
    }

    public void WeaponPickUp()
    {
        _weaponPickUp.SetActive(false);
        _weaponInHand.SetActive(true);
        pStats.AddWeapon();
    }

    public void Chest1()
    {
        DOTween.Play(chest1);
        pStats.AddCoins();
        SoundManager.instance.PlaySound(chestSound);
    }
    
    public void Chest2()
    {
        DOTween.Play(chest2);
        pStats.AddCoins();
        SoundManager.instance.PlaySound(chestSound);
    }

    public void Treasure()
    {
        pStats.AddCoins();
        SoundManager.instance.PlaySound(chestSound);
    }

    public void ForbiddenSoup()
    {
        if (!pStats.weapon)
        {
            pStats.Die();
        }
        else
        {
            pStats.EnchantWeapon();
            SoundManager.instance.PlaySound(enchantSound);
            weaponParticles.SetActive(true);
        }
    }

    public void Combat()
    {
        if (pStats.enchantedWeapon)
        {
            StartCoroutine(EnemyDeath());
        }
        else
        {
            bug.GetComponent<Animator>().SetTrigger("Smash Attack");
            pStats.Die();
        }
    }

    IEnumerator EnemyDeath()
    {
        SoundManager.instance.PlaySound(attackSound);
        bug.GetComponent<Animator>().SetTrigger("Die");
        player.GetComponent<Animator>().SetTrigger("Attack");
        pStats.AddManyCoins();
        yield return new WaitForSeconds(3f);
        bug.SetActive(false);
        ladder.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ladder.transform.DOLocalMoveY(0, 3);
        SoundManager.instance.PlaySound(doorSound);
    }

    public void Ladder()
    {
        StartCoroutine(UIThingy.VictorySequence());
    }
}
