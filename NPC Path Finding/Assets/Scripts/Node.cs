using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{

    // Each node exists in a grid on the graph.
    public Node parent;
    // A node has two states:
    // It can be travrsed or it cannot.
    public bool traverseable;
    // graphPosition tracks the current position on the graph.
    public Vector3 graphPosition;

    public int gCost;
    public int hCost;

    // Keep track of coordinates.
    public int graphX;
    public int graphY;

    // Constructor takes an initial boolean and a starting graph position.
    public Node(bool _traverseable, Vector3 _graphPosition, int _x, int _y)
    {
        traverseable = _traverseable;
        graphPosition = _graphPosition;
        graphX = _x;
        graphY = _y;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}
