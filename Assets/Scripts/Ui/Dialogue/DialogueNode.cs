using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Dialogue
{
    public class DialogueNode:ScriptableObject
    {
        [SerializeField]
        bool PlayerSpeaking=false;
        [SerializeField]
        bool Question = false;
        [SerializeField]
        string Text;
        [SerializeField]
        List<string> children=new List<string>();
        [SerializeField]
        Rect position=new Rect(0,0,250,100);
        [SerializeField]
        string OnEnterAction;
        [SerializeField]
        string OnExitAction;

        public Rect GetRect()
        {
            return position;
        }

        public string GetText()
        {
            return Text;
        }

        public List<string> GetChildren()
        {
            return children;
        }

        public bool isPlayerSpeaking()
        {
            return PlayerSpeaking;
        }

        public bool IsQuestion()
        {
            return Question;
        }

        public string GetOnEnterAction()
        {
            return OnEnterAction;
        }
        public string GetOnExitAction()
        {
            return OnExitAction;
        }


#if UNITY_EDITOR
        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Move dialogue objects");
            position.position = newPosition;
            EditorUtility.SetDirty(this);
        }

        public void SetText(string newText)
        {
            if(newText!=Text)
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                Text = newText;
                EditorUtility.SetDirty(this);
            }
        }

        public void AddChild(string childId)
        {
            Undo.RecordObject(this, "Add Dialogue link");
            children.Add(childId);
            EditorUtility.SetDirty(this);
        }

        public void RemoveChild(string childId)
        {
            Undo.RecordObject(this, "Remove Dialogue link");
            children.Remove(childId);
            EditorUtility.SetDirty(this);
        }

        public void SetSpeaker(bool newIsPlayerSpeaking)
        {
            Undo.RecordObject(this, "Change Dialogue Speaker");
            PlayerSpeaking = newIsPlayerSpeaking;
            EditorUtility.SetDirty(this);
        }

        public void SetIsQuestion(bool status)
        {
            Question = status;
        }
#endif
    }
}
