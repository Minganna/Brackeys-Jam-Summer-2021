using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dialogue
{
    public class NPCDialogues : MonoBehaviour
    {
        [SerializeField] Dialogue characterDialouges;
        [SerializeField] string converstantname;
        [SerializeField] Texture2D converstantAvatar;
        Animator anim;


        private void Awake()
        {
            anim = this.GetComponentInChildren<Animator>();
        }


        public Dialogue GetDialogues()
        {
            return characterDialouges;
        }

        public string GetName()
        {
            return converstantname;
        }

        public Texture2D GetAvatar()
        {
            return converstantAvatar;
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerConversant.instance.getDialogueData(this, characterDialouges);
            PlayerController.instance.canPunch = false;
            anim.SetBool("Talked", true);

        }
        private void OnTriggerExit(Collider other)
        {
            PlayerConversant.instance.canDialogueWithCharacter = false;
            PlayerController.instance.canPunch = true;
            anim.SetBool("Talked", false);
        }
    }
}