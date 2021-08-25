using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthPickUp : MonoBehaviour
{
    public int HealAmount;
    public bool isFullHeal;
    public bool isExtraLife;
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
            }
            else if(isExtraLife)
            {
                HealthManager.instance.AddLife();
            }
            else
            {
                HealthManager.instance.AddHealth(HealAmount);
            }    
            Destroy(gameObject);
        }
    }
}
