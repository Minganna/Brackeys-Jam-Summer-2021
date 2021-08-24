using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] string action;
        [SerializeField] UnityEvent onTrigger;
        // Start is called before the first frame update
        public void Trigger(string actionToTrigger)
        {
            if(actionToTrigger==action)
            {
                onTrigger.Invoke();
            }
        }
    }
}
