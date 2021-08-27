using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    Animator anim;
    public bool endLevel = true;
    public int tadpoleindex;
    public int Level;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            anim.SetTrigger("Hit");
            PlayerController.instance.SetWinningAnim();
            StartCoroutine(GameManager.instance.LevelEndCo(endLevel,this.GetComponent<LevelExit>()));
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
