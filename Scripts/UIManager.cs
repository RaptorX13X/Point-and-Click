using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField] private TextMeshProUGUI itsTheFinalCounter;
    [SerializeField] private GameObject weaponPopup;
    private TextMeshProUGUI popupText;
    
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip victorySound;
    private void Start()
    {
        gameScreen.SetActive(true);
        pauseScreen.SetActive(false);
        deathScreen.SetActive(false);
        victoryScreen.SetActive(false);
        weaponPopup.SetActive(false);
        popupText = weaponPopup.GetComponent<TextMeshProUGUI>();
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (!playerStats.alive)
        {
            StartCoroutine(DeathSequence());
        }
        counter.text = playerStats.coins + "g";
        itsTheFinalCounter.text = "final score: " + playerStats.coins + "g";
    }

    IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(3f);
        gameScreen.SetActive(false);
        deathScreen.SetActive(true);
        SoundManager.instance.PlaySound(deathSound);
        Time.timeScale = 0f;
    }

    public IEnumerator VictorySequence()
    {
        yield return new WaitForSeconds(1f);
        gameScreen.SetActive(false);
        victoryScreen.SetActive(true);
        SoundManager.instance.PlaySound(victorySound);
        Time.timeScale = 0f;

    }
    public IEnumerator WeaponAcquired()
    {
        weaponPopup.SetActive(true);
        popupText.text = "Weapon acquired";
        yield return new WaitForSeconds(3f);
        weaponPopup.SetActive(false);
    }
    public IEnumerator WeaponEnchanted()
    {
        weaponPopup.SetActive(true);
        popupText.text = "Weapon enchanted";
        yield return new WaitForSeconds(3f);
        weaponPopup.SetActive(false);
    }
    

    public void PauseGame()
    {
        gameScreen.SetActive(false);
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        pauseScreen.SetActive(false);
        gameScreen.SetActive(true);
        Time.timeScale = 1f;
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
