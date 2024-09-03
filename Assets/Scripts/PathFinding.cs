using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    
    private List<GenCube> openList;
    private List<GenCube> closedList;

    public List<GenCube> FindPath(GenCube startCube, GenCube targetCube)
    {
        openList = new List<GenCube> { startCube };
        closedList = new List<GenCube>();

        while (openList.Count > 0)
        {
            GenCube currentCube = GetLowestCostingCube(openList);
            if (currentCube == targetCube)
            {
                return RetracePath(startCube, targetCube);
            }

            openList.Remove(currentCube);
            closedList.Add(currentCube);

            foreach (GenCube neighbour in GetNeighbours(currentCube))
            {
                if (closedList.Contains(neighbour) || neighbour.BGetOccupiedStatus())
                {
                    continue;
                }

                int newCostToNeighbour = currentCube.gCost + GetDistance(currentCube, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openList.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetCube);
                    neighbour.parent = currentCube;

                    if (!openList.Contains(neighbour))
                    {
                        openList.Add(neighbour);
                    }
                }
            }
        }
        return null;
    }

    private List<GenCube> RetracePath(GenCube startCube, GenCube endCube)
    {
        List<GenCube> path = new List<GenCube>();
        GenCube currentCube = endCube;

        while (currentCube != startCube)
        {
            path.Add(currentCube);
            currentCube = currentCube.parent;
        }
        path.Reverse();
        return path;
    }

    private int GetDistance(GenCube cubeA, GenCube cubeB)
    {
        int distanceX = Mathf.Abs(cubeA.GetRow() - cubeB.GetRow());
        int distanceY = Mathf.Abs(cubeA.GetColumn() - cubeB.GetColumn());
        return distanceX + distanceY;
    }

    private GenCube GetLowestCostingCube(List<GenCube> cubeList)
    {
        GenCube lowestCostingNode = cubeList[0];
        foreach (GenCube genCube in cubeList)
        {
            if (genCube.FCost < lowestCostingNode.FCost)
            {
                lowestCostingNode = genCube;
            }
        }
        return lowestCostingNode;
    }

    public List<GenCube> GetNeighbours(GenCube genCube)
    {
        List<GenCube> neighbours = new List<GenCube>();

        int x = genCube.GetRow();
        int y = genCube.GetColumn();

        //checking only left, right, down.
        
        if (x - 1 >= 0 && !gridManager.GetGridArray()[x - 1, y].BGetOccupiedStatus())
        {
            neighbours.Add(gridManager.GetGridArray()[x - 1, y]);
        }

        if (x + 1 < gridManager.GetRows() && !gridManager.GetGridArray()[x + 1, y].BGetOccupiedStatus())
        {
            neighbours.Add(gridManager.GetGridArray()[x + 1, y]);
        }

        if (y - 1 >= 0 && !gridManager.GetGridArray()[x, y - 1].BGetOccupiedStatus())
        {
            neighbours.Add(gridManager.GetGridArray()[x, y - 1]);
        }

        if (y + 1 < gridManager.GetColumns() && !gridManager.GetGridArray()[x, y + 1].BGetOccupiedStatus())
        {
            neighbours.Add(gridManager.GetGridArray()[x, y + 1]);
        }

        return neighbours;
    }
}
