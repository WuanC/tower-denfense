using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingAStar : PathFinding
{
    private void Start()
    {
        EventManager.Instance.Register(EventID.OnPlaceTower, SetObstacle);
    }
    public override List<Vector3Int> FindPath(Vector3Int start)
    {
        List<Node> open = new List<Node>();
        List<Node> close = new List<Node>();
        Node startNode = new Node(start, null, 0, GetH(start, endNode));
        open.Add(startNode);
        int i = 0;
        while(open.Count > 0)
        {
            i++;
            open.Sort((a, b) => a.F.CompareTo(b.F));
            Node currentNode = open[0];
            if(currentNode.posittion == endNode)
            {
               return ReconstructPath(currentNode);
            }
            open.Remove(currentNode);
            close.Add(currentNode);
            foreach(Node neighbor in GetNeighbors(currentNode))
            {
                if(close.Contains(neighbor) || !IsWalking(neighbor.posittion))
                {
                    continue;
                }
                float g = currentNode.G + 1;
                Node neighborNode = open.Find(n => n.posittion == neighbor.posittion);
                if(neighborNode == null)
                {
                    neighborNode = new Node(neighbor.posittion, currentNode, g, GetH(start, endNode));
                    open.Add(neighborNode);
                }
                else if(g < neighborNode.G)
                {
                    neighborNode.G = g;
                    neighborNode.parentNode = currentNode;
                }
            }
            if (i == 1000)
            {
                Debug.Log("k tim ra");
                return null;
            }

        }

        return null;
    }

    private int GetH(Vector3Int start ,Vector3Int end)
    {
        return Mathf.Abs(end.x - start.x) + Mathf.Abs(end.y - start.y);
    }
    private List<Vector3Int> ReconstructPath(Node endNode)
    {
        List<Vector3Int > path = new List<Vector3Int>();
        Node currentNode = endNode;
        while(currentNode != null)
        {
            path.Add(currentNode.posittion);
         //   Debug.Log("c  " + currentNode.posittion);
            currentNode = currentNode.parentNode;
        }
        path.Reverse();
        return path;

    }
}
