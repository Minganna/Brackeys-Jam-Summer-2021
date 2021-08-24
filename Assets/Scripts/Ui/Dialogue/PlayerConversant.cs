using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using System.Linq;
using System;

namespace Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        public static PlayerConversant instance;
        Dialogue currentDialogue;
        [SerializeField] string playerName;
        [SerializeField] Texture2D playerAvatar;
        DialogueNode currentNode = null;
        bool isDialogueOn = false;
        public bool canDialogueWithCharacter = false;
        NPCDialogues currentConverstant = null;
        public event Action onConversationUpdated;
        bool isPlayerSpeaking;


        private void Awake()
        {
            instance = this;

        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (isDialogueOn)
                   {
 
                    if(HasNext())
                    {
                        Next();
                        DialogueUi.instance.UpdateUI();
                    }
                    else
                    {
                        DialogueUi.instance.gameObject.SetActive(false);
                        PlayerController.instance.canMove = true;
                        isDialogueOn = false;
                    }
                    return;
                }
                if (canDialogueWithCharacter&&!isDialogueOn)
                {
                    StartDialogue();
                }
            }
 
        }

        public void getDialogueData(NPCDialogues newConversant, Dialogue newDialogue)
        {
            currentConverstant = newConversant;
            currentDialogue = newDialogue;
            newDialogue.OnValidate();
            canDialogueWithCharacter = true;
        }

        public void StartDialogue()
        {
            isDialogueOn = true;
            DialogueUi.instance.gameObject.SetActive(true);
            PlayerController.instance.canMove = false;
            currentNode = currentDialogue.GetRootNode();
            TriggerEnterAction();
            onConversationUpdated();
        }

        public string GetText()
        {
            if(currentNode==null)
            {
                return "";
            }
            return currentNode.GetText();
        }

        public void Quit()
        {
            TriggerExitAction();
            currentConverstant = null;
            currentDialogue = null;
            currentNode = null;
        }

        public void GetSpeaker()
        {
            if(currentNode)
            {
                isPlayerSpeaking = currentNode.isPlayerSpeaking();
            }
            
        }
        public void Next()
        {
           DialogueNode[] childnodes= currentDialogue.GetAllChildren(currentNode).ToArray();
            currentNode = childnodes[0];
        }

        public bool HasNext()
        {
            return currentDialogue.GetAllChildren(currentNode).ToArray().Count() >0;
        }

        public string GetCurrentConversantName()
        {
            if(currentNode)
            {
                if (isPlayerSpeaking)
                {
                    return playerName;
                }
                else
                {
                        return currentConverstant.GetName();

                    
                }
            }
            else
            {
                return "";
            }
        }

        public Texture2D GetCurrentAvatar()
        {
            if (isPlayerSpeaking)
            {
                return playerAvatar;
            }
            else
            {
                return currentConverstant.GetAvatar();
            }
        }


        private void TriggerEnterAction()
        {
            if (currentNode != null)
            {
                TriggerAction(currentNode.GetOnEnterAction());
            }
        }
        private void TriggerExitAction()
        {
            if (currentNode != null)
            {

                TriggerAction(currentNode.GetOnExitAction());


            }
        }

        private void TriggerAction(string action)
        {
            Debug.Log("Triggering " + action);
            if (action == "") return;

            foreach (DialogueTrigger trigger in currentConverstant.GetComponents<DialogueTrigger>())
            {
                
                trigger.Trigger(action);
            }
        }

    }
}
