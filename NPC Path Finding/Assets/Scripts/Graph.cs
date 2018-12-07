using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {

    public LayerMask untraverseablemask;
    // Declare a vector 2 which will represent the world size.
    public Vector2 graphWorldSize;
    // Define how much space a node covers.
    public float nodeRadius;
    // Create a 2-Dimension array of nodes representing the graph.
    Node[,] graph;

    float nodeDiameter;
    int graphSizeX, graphSizeY;

    private void Start()
    {
        // Calculate the diameter. This is of course radius * 2.
        nodeDiameter = nodeRadius * 2;
        // Use RoundToInt to convert the vector properties to readable graph size
        // variables.
        graphSizeX = Mathf.RoundToInt(graphWorldSize.x / nodeDiameter);
        graphSizeY = Mathf.RoundToInt(graphWorldSize.y / nodeDiameter);
        // Create the graph.
        CreateGraph();
    }

    void CreateGraph()
    {

        // Create a new array of nodes called graph.
        graph = new Node[graphSizeX, graphSizeY];
        // Get bottom left of world.
        Vector3 graphBottomLeft = transform.position - Vector3.right * graphWorldSize.x / 2 - Vector3.forward * graphWorldSize.y / 2;
        for (int ix = 0; ix < graphSizeX; ix++)
        {
            for (int iy = 0; iy < graphSizeY; iy++)
            {
                // These for loops cycle each point of x and y on the graph starting from the bottom left.
                Vector3 graphPoint = graphBottomLeft + Vector3.right * (ix * nodeDiameter + nodeRadius) + Vector3.forward * (iy * nodeDiameter + nodeRadius);
                // If there's a collision, check traverseable to false.
                bool traverseable = !(Physics.CheckSphere(graphPoint, nodeRadius, untraverseablemask));
                graph[ix, iy] = new Node(traverseable, graphPoint, ix, iy);
            }
        }
        Debug.Log(graph[graphSizeX, graphSizeY]);
        Debug.Log(graph[graphSizeX, graphSizeY]);
    }

    public Node NodeFromGraphPoint(Vector3 graphPosition)
    {
        float percentX = (graphPosition.x + graphWorldSize.x / 2) / graphWorldSize.x;
        float percentY = (graphPosition.z + graphWorldSize.y / 2) / graphWorldSize.y;
        // If outside of grid, don't return invalid index in array.
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((graphSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((graphSizeY - 1) * percentY);

        return graph[x, y];
    }

    public List<Node> getNeighbours(Node node)
    {
        List<Node> Neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    // Skip this iteration.
                    continue;
                }
                int checkX = node.graphX + x; 
                int checkY = node.graphY + y;
                
                if (checkX >= 0 && checkX < graphSizeX && checkY >= 0 && checkY < graphSizeY)
                {
                    Neighbours.Add(graph[checkX, checkY]);
                }
            }
        }
        return Neighbours;
    }

    public List<Node> path;

    private void OnDrawGizmos()
        {
        Gizmos.DrawWireCube(transform.position, new Vector3(graphWorldSize.x, 1, graphWorldSize.y));

        if (graph != null)
        {
            foreach (Node n in graph)
            {
                Gizmos.color = (n.traverseable) ? Color.white : Color.red;
                if(path != null)
                {
                    if(path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                    }
                }
                Gizmos.DrawCube(n.graphPosition, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
        }
}
