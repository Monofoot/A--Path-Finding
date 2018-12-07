using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

    // Two transforms control which are essentially the starting point and end points.
    public Transform seeker;
    public Transform target;

    Graph graph;
    private void Awake()
    {
        graph = GetComponent<Graph>();
          
    }

    private void Update()
    {
        FindPath(seeker.position, target.position);
    }

    void FindPath(Vector3 start, Vector3 end)
    {
        // Create two new node positions for the start and end nodes.
        Node startNode = graph.NodeFromGraphPoint(start);
        Node endNode = graph.NodeFromGraphPoint(end);

        // A* requires two lists: and open and closed list.
        // Create a list of nodes for the open set. Add the start node to this later.
        List<Node> openSet = new List<Node>();
        // Use a hash set for the closed set.
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);


        // While we have an open set:
        while(openSet.Count > 0)
        {
            // The current node is equal to the first element in the open set.
            Node currentNode = openSet[0];
            // Loop through each node in the open set.
            for (int i = 1; i < openSet.Count; i++)
            {
                // If the node has a lower fCost than the current node
                if(openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    // Set the current node to that code.
                    currentNode = openSet[i];
                }
            }
            // Remove the current node from the stack.
            openSet.Remove(currentNode);
            // Add it to the closed set.
            closedSet.Add(currentNode);

            if (currentNode == endNode)
            {
                // Leave the loop.
                RetracePath(startNode, endNode);
                return;
            }
            foreach (Node neighbour in graph.getNeighbours(currentNode))
            {
                if (!neighbour.traverseable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int movementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (movementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = movementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, endNode);
                    // Finally, set the neighbour's parent to the current node.
                    neighbour.parent = currentNode;

                    if(!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }

            }
        }
    }

    // Retracing the path allows us to trace a new patch to the destination.
    void RetracePath(Node start, Node end)
    {
        List<Node> path = new List<Node>();
        Node currentNode = end;

        while (currentNode != start)
        {
            // Add the current node when we're not at the start.
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        graph.path = path;
    }

    int GetDistance(Node A, Node B)
    {
        int distanceX = Mathf.Abs(A.graphX - B.graphX);
        int distanceY = Mathf.Abs(A.graphY - B.graphY);

        // If the X distance is greater than Y
        if (distanceX > distanceY)
        {
            // Return the travel sum for X.
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        // Return the travel sum for Y.
        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
}
