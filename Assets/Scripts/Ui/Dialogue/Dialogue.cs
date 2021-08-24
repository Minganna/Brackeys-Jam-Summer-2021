using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "EmptyDialogue", menuName = "Dialogue", order = 0)]
    public class Dialogue : ScriptableObject,ISerializationCallbackReceiver
    {
        [SerializeField]
        List<DialogueNode> nodes=new List<DialogueNode>();
        [SerializeField]
        Vector2 newNodeOffset = new Vector2(250, 0);

        Dictionary<string, DialogueNode> nodeloockup = new Dictionary<string, DialogueNode>();


        public void OnValidate()
        {
            nodeloockup.Clear();
            foreach(DialogueNode node in GetAllNodes())
            {
                nodeloockup[node.name] = node;
            }
        }


        public IEnumerable<DialogueNode> GetAllNodes()
        {
            return nodes;
        }

        public DialogueNode GetRootNode()
        {
            return nodes[0];
        }
        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode Parentnode)
        {
            foreach(string childId in Parentnode.GetChildren())
            {
                if(nodeloockup.ContainsKey(childId))
                {
                     yield return nodeloockup[childId];
                } 
            }

        }

        public IEnumerable<DialogueNode> GetPlayerChildren(DialogueNode currentNode)
        {
            foreach(DialogueNode node in GetAllChildren(currentNode))
            {
                if(node.isPlayerSpeaking())
                {
                    yield return node;
                }
            }
        }
        public IEnumerable<DialogueNode> GetAIChildren(DialogueNode currentNode)
        {
            foreach (DialogueNode node in GetAllChildren(currentNode))
            {
                if (!node.isPlayerSpeaking())
                {
                    yield return node;
                }
            }
        }

        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (nodes.Count == 0)
            {
                DialogueNode newNode= MakeNode(null);
                AddNode(newNode);
            }
            if (AssetDatabase.GetAssetPath(this) != "")
            {
                foreach (DialogueNode node in GetAllNodes())
                {
                    if (AssetDatabase.GetAssetPath(node) == ""&&node!=null)
                    {
                        AssetDatabase.AddObjectToAsset(node, this);
                    }
                }
            }

#endif
        }

        public void OnAfterDeserialize()
        {
        }


#if UNITY_EDITOR
       
        public void CreateNode(DialogueNode creatingNode)
        {
            DialogueNode newNode = MakeNode(creatingNode);
            Undo.RegisterCompleteObjectUndo(newNode, "Created Dialogue node");
            Undo.RecordObject(this, "Added Dialogue Node");
            AddNode(newNode);
        }

        private DialogueNode MakeNode(DialogueNode creatingNode)
        {
            DialogueNode newNode = CreateInstance<DialogueNode>();
            if (nodes.Count == 0)
            {
                newNode.name = "MainNode";
            }
            else
            {
                newNode.name = Guid.NewGuid().ToString();
            }
            if (creatingNode != null)
            {
                creatingNode.AddChild(newNode.name);
                newNode.SetSpeaker(!creatingNode.isPlayerSpeaking());
                newNode.SetPosition(creatingNode.GetRect().position + newNodeOffset);
            }
            

            return newNode;
        }

        private void AddNode(DialogueNode newNode)
        {
            nodes.Add(newNode);
            OnValidate();
        }

        public void DeleteNode(DialogueNode deletinggNode)
        {
            Undo.RecordObject(this, "Deleted Dialogue Node");
            nodes.Remove(deletinggNode);
            OnValidate();
            CleanDanglingChildren(deletinggNode);
            Undo.DestroyObjectImmediate(deletinggNode);

        }

        private void CleanDanglingChildren(DialogueNode NodeToDelete)
        {
            foreach (DialogueNode node in GetAllNodes())
            {
                node.RemoveChild(NodeToDelete.name);
            }
        }

#endif
    }
}


