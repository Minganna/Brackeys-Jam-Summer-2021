﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    Vector3 respawnPosition;

    public static int CollectibleCount;

    public static bool isSketchCameraOn;

    private void Awake()
    {
        instance = this;
        Camera.main.GetComponent<CameraSketch>().enabled = isSketchCameraOn;
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

        AddCollectable(CollectibleCount);
        
    }

    private void Update()
    {
        if(Input.GetButtonDown("PauseMenu"))
        {
            PauseUnPause();
        }
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

    public void AddCollectable(int valueToAdd)
    {
        CollectibleCount += valueToAdd;
        UiManager.instance.CoinText.text = "" + CollectibleCount;
    }


    public void PauseUnPause()
    {
        if(UiManager.instance.pauseScreen.activeInHierarchy)
        {
            UiManager.instance.pauseScreen.SetActive(false);
            UiManager.instance.CloseOptions();
            Time.timeScale = 1.0f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UiManager.instance.pauseScreen.SetActive(true);
            Time.timeScale = 0.0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void SetCameraSketch()
    {
        isSketchCameraOn = true;
        Camera.main.GetComponent<CameraSketch>().enabled = isSketchCameraOn;
       
    }
}
