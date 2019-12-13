using UnityEditor;

namespace as3mbus.Selfish.Source.Polymorphism_Serialization.pelican7
{
    public static class CreateNodesExample
    {
        [MenuItem("Window/Graph Serialization Example/Create Nodes")]
        public static void CreateNodes()
        {
            Node nodeA = Node.Create("NodeA");
            Node nodeB = Node.Create<SpriteNode>("NodeB");

            nodeA.Neighbors.Add(nodeB);
        }

        [MenuItem("Window/Graph Serialization Example/Create Graph")]
        public static void CreateGraph()
        {
            // Create graph.
            Graph graph = Graph.Create("NewGraph");

            // Create nodes.
            Node nodeA = Node.Create<Node>("NodeA");
            SpriteNode nodeB = Node.Create<SpriteNode>("NodeB");
            nodeA.Neighbors.Add(nodeB);

            // Add nodes to graph.
            graph.AddNode(nodeA);
            graph.AddNode(nodeB);
        }
    }
}