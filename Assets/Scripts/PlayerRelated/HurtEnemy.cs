using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public bool JumpDeath;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag== "Enemy")
        {
            other.GetComponent<EnemyHealthManager>().TakeDamage(JumpDeath);
        }
        if(other.tag=="ColorS")
        {
            if(!JumpDeath)
            {
                if(FindObjectOfType<ColorSphereLogic>())
                {
                    FindObjectOfType<ColorSphereLogic>().DestroySphereandchangeColor();
                }
            }
        }
    }
}
