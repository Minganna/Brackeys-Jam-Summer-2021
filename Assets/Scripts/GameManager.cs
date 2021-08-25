using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    Vector3 respawnPosition;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if(PlayerController.instance)
        {
            respawnPosition = PlayerController.instance.transform.position;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        StartCoroutine(RespawnPlayer());
        HealthManager.instance.PlayerKilled();
    }

    public IEnumerator RespawnPlayer()
    {
        PlayerController.instance.gameObject.SetActive(false);
        if(CameraController.instance.cameraBrain)
        {
            CameraController.instance.cameraBrain.enabled = false;
        }
        if(UiManager.instance)
        {
            UiManager.instance.fadeToBlack = true;
        }
        GameObject DeathEffect = Resources.Load("Prefabs/Effects/Player Death Effect", typeof(GameObject)) as GameObject;
        GameObject DeathInScene=null;
        if (DeathEffect)
        {
             DeathInScene= Instantiate(DeathEffect, PlayerController.instance.transform.position+new Vector3(0.0f,1.0f,0.0f),PlayerController.instance.transform.rotation);
        }
        yield return new WaitForSeconds(2.0f);
        UiManager.instance.fadeFromBlack = true;
        PlayerController.instance.transform.position = respawnPosition;
        CameraController.instance.cameraBrain.enabled = true;
        PlayerController.instance.gameObject.SetActive(true);
        HealthManager.instance.ResetHealth();
    }

    public void SetSpawnPoint(Vector3 newSpawnPosition)
    {
        respawnPosition = newSpawnPosition;
    }
}
