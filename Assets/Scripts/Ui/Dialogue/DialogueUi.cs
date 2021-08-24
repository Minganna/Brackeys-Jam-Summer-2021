using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;
using TMPro;

namespace UI
{
    public class DialogueUi : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI DialogueText;
        [SerializeField]
        TextMeshProUGUI DialogueName;

        public static DialogueUi instance;

        private void Awake()
        {
            instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            this.gameObject.SetActive(false);
            PlayerConversant.instance.onConversationUpdated += UpdateUI;
            UpdateUI();
        }

        // Update is called once per frame
        public void UpdateUI()
        {
            if(PlayerConversant.instance)
            {
                PlayerConversant.instance.GetSpeaker();
                DialogueName.text = PlayerConversant.instance.GetCurrentConversantName();
                DialogueText.text = PlayerConversant.instance.GetText();
            }

        }
    }
}
