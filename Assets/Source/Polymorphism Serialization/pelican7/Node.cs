using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace as3mbus.Selfish.Source.Polymorphism_Serialization.pelican7
{
    [System.Serializable]
    public class Node : ScriptableObject
    {
        [SerializeField]
        private List<Node> neighbors;

        public List<Node> Neighbors
        {
            get
            {
                if (neighbors == null)
                {
                    neighbors = new List<Node>();
                }

                return neighbors;
            }
        }

        public static Node Create(string name)
        {
            Node node = CreateInstance<Node>();

            string path = string.Format("Assets/{0}.asset", name);
            AssetDatabase.CreateAsset(node, path);

            return node;
        }
        public static T Create<T>(string name)
            where T : Node
        {
            T node = CreateInstance<T>();
            node.name = name;
            return node;
        }
        public void Awake()
        {
            Debug.Log("Awake");
        }

        public void OnEnable()
        {
            Debug.Log("OnEnable");
        }

        public void OnDisable()
        {
            Debug.Log("OnDisable");
        }

        public void OnDestroy()
        {
            Debug.Log("OnDestroy");
        }
    }
}