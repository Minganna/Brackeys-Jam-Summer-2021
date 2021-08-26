using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int maxHealth=1;
    int currentHealth;

    public int deathSound;

    public GameObject ItemToDrop;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(bool isJumpDeath)
    {
        currentHealth--;
        if(currentHealth<=0)
        {

            AudioManager.instance.Sfx(deathSound);
            if(isJumpDeath)
            {
                PlayerController.instance.Bounce();
            }
            Instantiate(ItemToDrop, transform.position+new Vector3(0.0f,1.0f,0.0f), transform.rotation);
            this.GetComponent<EnemyController>().PlayDeath();
        }
    }
}
