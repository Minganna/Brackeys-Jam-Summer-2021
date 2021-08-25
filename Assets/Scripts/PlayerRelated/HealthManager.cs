using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public static HealthManager instance;

    public int currentHealth, maxHealth,Lives;
    public float invicibleHealth=2.0f;
    private float invincibleCounter;
    public int DeathSFX;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        FlashingPlayerLogic();
    }

    private void FlashingPlayerLogic()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;

            for (int i = 0; i < PlayerController.instance.playerPieces.Length; i++)
            {
                if (Mathf.Floor(invincibleCounter * 5.0f) % 2 == 0)
                {
                    PlayerController.instance.playerPieces[i].SetActive(true);
                }
                else
                {
                    PlayerController.instance.playerPieces[i].SetActive(false);
                }

                if (invincibleCounter <= 0)
                {
                    PlayerController.instance.playerPieces[i].SetActive(true);
                }
            }
        }
    }

    public void Hurt(int damage)
    {
        if(invincibleCounter<=0)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                PlayerKilled();
                GameManager.instance.Respawn();
            }
            else
            {
                PlayerController.instance.KnockBack();
                invincibleCounter = invicibleHealth;
            }
            UpdateUI();
        }
 
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void AddHealth(int amountHeal)
    {
        if(currentHealth<maxHealth)
        {
            currentHealth += amountHeal;
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        UiManager.instance.LifeText.text = Lives.ToString();
        for (int i = 0; i <= UiManager.instance.HealthImages.Length; i++)
        {
            if(i<currentHealth)
            {
                UiManager.instance.HealthImages[i].gameObject.SetActive(true);
            }
            else
            {
                if(i< UiManager.instance.HealthImages.Length)
                {
                    UiManager.instance.HealthImages[i].gameObject.SetActive(false);
                }
                
            }    
        }
    }

    public void AddLife()
    {
        Lives++;
        UpdateUI();
    }

    public void PlayerKilled()
    {
        AudioManager.instance.Sfx(DeathSFX);
        currentHealth = 0;
        if(Lives>0)
        {
            Lives--;
        }
        else
        {
            Debug.Log("BackToMainScreen");
        }
        UpdateUI();
        
    }
}
