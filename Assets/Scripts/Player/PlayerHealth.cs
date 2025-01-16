using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;
    public bool isDead;
    [Header("Healthbar")]
    public float maxHealth = 100;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    public TextMeshProUGUI healthPoints;

    [Header("Damage")]
    public Image overlay;
    public float duration;
    public float fadespeed;

    [Header("Death")]
    public GameObject gameOverUI;

    private float durationTimer;
    
    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        if (overlay.color.a > 0)
        {
            if (health < 30)
                return;
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadespeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }

    public void UpdateHealthUI()
    {
        
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB,hFraction, percentComplete);
        }

        if (fillF < hFraction)
        {
            backHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete);
        }
        healthPoints.text = health.ToString() + "/" + maxHealth.ToString(); 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isDead == false)
        {
            if (other.gameObject.CompareTag("EnemyLeftHand"))
            {
                TakeDamage(15f);
            }
            else if (other.gameObject.CompareTag("EnemyRightHand"))
            {
                TakeDamage(10f);
            }
        }
       
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        durationTimer = 0f;
        if (health <= 0)
        {
            PlayerDead();
            isDead = true;
        }
        else
        {
            int randomValue = UnityEngine.Random.Range(0, 2);
            if (randomValue == 0)
            {
                SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerHurt1);
            }
            else
            {
                SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerHurt2);
            }
           
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
        }
       
    }

    private void PlayerDead()
    {

        SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerDeath);
        GetComponent<InputManager>().enabled = false;
        GetComponentInChildren<Animator>().enabled = true;

        GetComponent<ScreenFader>().StartFade();
        StartCoroutine(ShowGameOver());
    }

    private IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(1f);
        
        gameOverUI.gameObject.SetActive(true);

        StartCoroutine(ReturtnToMainMenu());
    }

    private IEnumerator ReturtnToMainMenu()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void RestorteHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }
       
}
