using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthPickUp : MonoBehaviour
{
    public int HealAmount;
    public bool isFullHeal;
    public bool isExtraLife;

    public int LifeSfx;
    public int HealthSfx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag==("Player"))
        {
            if(isFullHeal)
            {
                HealthManager.instance.ResetHealth();
                AudioManager.instance.Sfx(HealthSfx);
            }
            else if(isExtraLife)
            {
                HealthManager.instance.AddLife();
                AudioManager.instance.Sfx(LifeSfx);
            }
            else
            {
                HealthManager.instance.AddHealth(HealAmount);
                AudioManager.instance.Sfx(HealthSfx);
            }    
            Destroy(gameObject);
        }
    }
}
