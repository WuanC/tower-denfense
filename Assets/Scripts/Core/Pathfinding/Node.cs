using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public Vector3Int posittion;
    public Node parentNode;
    public float G;
    public float H;
    public float F => G + H;
    public Node(Vector3Int posittion) {
        this.posittion = posittion;
    }
    public Node(Vector3Int posittion, Node parentNode, float g, float h) 
    {
        this.posittion = posittion;
        this.parentNode = parentNode;
        G = g;
        H = h;
    }

    public bool CompareTo(Node other)
    {
        return posittion.x == other.posittion.x && posittion.y == other.posittion.y && posittion.z == other.posittion.z;
    }
    
}
