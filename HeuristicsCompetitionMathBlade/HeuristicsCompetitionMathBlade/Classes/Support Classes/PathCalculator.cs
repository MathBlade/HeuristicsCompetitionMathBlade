using HeuristicsCompetitionMathBlade.Classes.Grid_Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HeuristicsCompetitionMathBlade.Classes.Support_Classes
{
    class PathCalculator
    {

       

        private static float getCostWithoutWeight(Node n1, Node n2, bool realDistance)
        {
            GridLocation difference = new GridLocation(Math.Abs(n2.GridLocation.X - n1.GridLocation.X), Math.Abs(n2.GridLocation.Y - n1.GridLocation.Y));
            if (realDistance)
            {
                
                if (difference.X > difference.Y)
                {
                    return (float)Math.Sqrt(2) * difference.Y + (difference.X - difference.Y);

                }
                else
                {
                    return (float)Math.Sqrt(2) * difference.X + (difference.Y - difference.X);
                }
            }
            else
            {
                int remainder = Math.Abs(difference.X - difference.Y);
                int diagonalCount = Math.Min(difference.X, difference.Y);

                return (float)(diagonalCount + remainder);

            }
            
        }

        


        
        public static float GetHCost(Grid grid, Node node)
        {
            return getCostWithoutWeight(node, grid.EndNode, grid.UseRealDistance);
        }

        private static List<Node> GetPathFromParents(List<Node> closedNodes)
        {
            List<Node> nodePath = new List<Node>();
            Node lastNode = closedNodes.Last();
            nodePath.Add(lastNode);
            while (lastNode.ParentNode != null)
            {
                nodePath.Add(lastNode.ParentNode);
                lastNode = lastNode.ParentNode;
            }

            return nodePath;
            
        }
        
        private static Node GetNodeToExamine(Grid grid, List<Node> openNodes)
        {
            Node nodeWithLowestFCost = null;
            foreach(Node node in openNodes)
            {
                if (nodeWithLowestFCost == null)
                {
                    nodeWithLowestFCost = node;
                }

                Node.CalculateHCostIfNeeded(grid, node);

                if ((node.FCost < nodeWithLowestFCost.FCost) || (node.FCost == nodeWithLowestFCost.FCost && node.HCost < nodeWithLowestFCost.HCost))
                {
                    nodeWithLowestFCost = node;
                }
            }

            return nodeWithLowestFCost;
        }
        public static List<Node> CalculateShortestPath(Grid grid)
        {
            List<Node> closedNodes = new List<Node>();
            List<Node> openNodes = new List<Node>();
            openNodes.Add(grid.StartNode);

            while (openNodes.Count > 0)
            {
                Node currentNode = GetNodeToExamine(grid,openNodes);

                openNodes.Remove(currentNode);
                closedNodes.Add(currentNode);

                

                if (currentNode.GridLocation.X == grid.EndNode.GridLocation.X && currentNode.GridLocation.Y == grid.EndNode.GridLocation.Y)
                {
                    return GetPathFromParents(closedNodes);
                }

                foreach (Node node in grid.GetNeighbors(currentNode))
                //foreach (Node node in grid.GetNeighbors(currentNode))
                {
                    if (closedNodes.Contains(node))
                    {
                        continue;
                    }
                    else if (!openNodes.Contains(node))
                    {
                        openNodes.Add(node);
                        
                    }

                    float gCost = currentNode.GCost + getCostWithoutWeight(currentNode, node, grid.UseRealDistance) + ((node.GridLocation.X != grid.EndNode.GridLocation.X || node.GridLocation.Y != grid.EndNode.GridLocation.Y) ? node.Weight : Node.END_WEIGHT);
                    if (gCost < node.GCost)
                    {
                        node.GCost = gCost;
                        node.ParentNode = currentNode;

                    }
                    
                }
            }

            return null; 
           /* List<Node> nodesAlreadyEvaluated = new List<Node>();
            List<Node> nodesUnderInvestigation = new List<Node>();
            nodesUnderInvestigation.Add(grid.StartNode);

            Node currentNode = nodesUnderInvestigation[0];
            
            while (nodesUnderInvestigation.Count > 0)
            {
                currentNode = null;
                
                //Get node with the lowest FCost.
                foreach (Node n in nodesUnderInvestigation)
                {
                    if (currentNode == null)
                    {
                        currentNode = n;
                    }
                    if (n.FCost < currentNode.FCost || (n.FCost == currentNode.FCost && n.HCost < currentNode.HCost))
                    {
                       currentNode = n;
                    }
                }
               
                 
                

                nodesUnderInvestigation.Remove(currentNode);
                nodesAlreadyEvaluated.Add(currentNode);



                if (currentNode.GridLocation.X == grid.EndNode.GridLocation.X && currentNode.GridLocation.X == grid.EndNode.GridLocation.Y)
                {
                    return GetPathFromParents(grid.StartNode,grid.EndNode);
                }

                List<Node> neighbors = grid.GetNeighbors(currentNode);
                foreach (Node neighbor in neighbors)
                {
                    if (nodesAlreadyEvaluated.Contains(neighbor))
                    {
                        continue;
                    }
                   

                    if (!nodesUnderInvestigation.Contains(neighbor) && !nodesAlreadyEvaluated.Contains(neighbor))
                    {
                        nodesUnderInvestigation.Add(neighbor);
                    }

                    float gScore = currentNode.GCost + getCostWithoutWeight(currentNode, neighbor);
                    if (gScore < neighbor.GCost)
                    {
                        neighbor.GCost = gScore;
                        neighbor.ParentNode = currentNode;
                    }
                }


            }

            return null;*/
           
        }
        
    }
}
