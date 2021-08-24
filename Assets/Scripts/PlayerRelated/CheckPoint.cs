using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
   
    public Texture2D CheckPointOff;
    public Texture2D CheckPointOn;
    bool IscheckPointOn;
    public GameObject CheckPointParticles;
    public GameObject ObjectRef;

    // Start is called before the first frame update
    void Awake()
    {
        CheckPointOff = Resources.Load("3DModels/Enviroment/PlayerHelpers/CheckPointOffColor", typeof(Texture2D)) as Texture2D;
        if(!CheckPointOff)
        {
            Debug.Log("Sorry not found texture");
        }
        CheckPointOn = Resources.Load("3DModels/Enviroment/PlayerHelpers/CheckPointColor", typeof(Texture2D)) as Texture2D;
        if (!CheckPointOn)
        {
            Debug.Log("Sorry not found texture");
        }
        CheckPointParticles = GetComponentInChildren<ParticleSystem>().gameObject;
        if(CheckPointParticles)
        {
            CheckPointParticles.SetActive(false);
        }
        ObjectRef = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            CheckPointActivateAndDeactivateLogic();
        }
    }

    private void CheckPointActivateAndDeactivateLogic()
    {
        GameManager.instance.SetSpawnPoint(this.transform.position);

        CheckPoint[] allCp = FindObjectsOfType<CheckPoint>();
        for (int i = 0; i < allCp.Length; i++)
        {
            if (allCp[i].ObjectRef != this)
            {
                allCp[i].ObjectRef.GetComponent<Renderer>().material.SetTexture("_MainTex", CheckPointOff);
                allCp[i].CheckPointParticles.SetActive(false);
                allCp[i].IscheckPointOn = false;
            }

        }
        if (!IscheckPointOn)
        {
            CheckPointParticles.SetActive(true);
        }
        this.GetComponent<Renderer>().material.SetTexture("_MainTex", CheckPointOn);
        IscheckPointOn = true;
    }
}
