using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    [SerializeField]
    int Damage = 1;
    public int DamageSfx;
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
        if(other.tag=="Player")
        {
            HealthManager.instance.Hurt(Damage);
            Debug.Log("Hurting the Player");
            AudioManager.instance.Sfx(DamageSfx);
        }
    }
}
