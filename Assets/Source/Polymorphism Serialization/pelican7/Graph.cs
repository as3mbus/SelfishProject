using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace as3mbus.Selfish.Source.Polymorphism_Serialization.pelican7
{
    [CreateAssetMenu(fileName = "graph",menuName = "graph")]
    public class Graph : ScriptableObject
    {
        [SerializeField]
        private List<Node> nodes;
        private List<Node> Nodes => nodes ?? (nodes = new List<Node>());

        public static Graph Create(string name)
        {
            Graph graph = CreateInstance<Graph>();

            string path = string.Format("Assets/{0}.asset", name);
            AssetDatabase.CreateAsset(graph, path);

            return graph;
        }

        public void AddNode(Node node)
        {
            Nodes.Add(node);
            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
        }

    }
}