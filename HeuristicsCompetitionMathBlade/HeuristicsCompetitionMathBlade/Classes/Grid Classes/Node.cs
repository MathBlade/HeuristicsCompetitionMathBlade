using HeuristicsCompetitionMathBlade.Classes.Support_Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeuristicsCompetitionMathBlade.Classes.Grid_Classes
{
    public class Node : IComparable<Node>
    {
        public const char NODE_DIVIDER = '\t';
        public const int NODE_IMPASSABLE = -1;
        public const int NODE_START = 0;
        public const int NODE_END = -3;
        public const string START_WORD = "Start";
        public const string END_WORD = "Stop";
        public const char START_CHAR = 'S';
        public const char END_CHAR = 'F';
        public const int END_WEIGHT = 1;


        GridLocation _gridLocation;
        int _weight;
        List<Node> _neighbors;
        float _gCost = int.MaxValue;
        float _hCost = int.MaxValue;
        Node _parentNode;
        public Node(GridLocation gridLocation, int weight)
        {
            _gridLocation = gridLocation;
            _weight = weight;
            _neighbors = new List<Node>();

        }

        public int CompareTo(Node other)
        {
            if (_gridLocation.Y != other.GridLocation.Y)
            {
                return _gridLocation.Y.CompareTo(other.GridLocation.Y);

            }
            else if (_gridLocation.X != other.GridLocation.X)
            {
                return _gridLocation.X.CompareTo(other.GridLocation.X);
            }
            else
            {
                throw new Exception("Grid Locations cannot be shared between nodes. ");
            }
        }
        
        public void AddNeighbor(Node node)
        {
            if (node == null)
            {
                return;
            }
            if (_neighbors == null)
            {
                _neighbors = new List<Node>();
            }
            
            _neighbors.Add(node);
        }

        
        
        public float FCost
        {
            get { 

                    return _gCost + _hCost;
                }
           
            
        }

        public static void CalculateHCostIfNeeded(Grid grid, Node node)
        {
            if (node.HCost != int.MaxValue)
            {
                return;
            }
            else
            {
                node.HCost = PathCalculator.GetHCost(grid, node);
                return;
            }
        }


        public GridLocation GridLocation { get { return _gridLocation; } }
        public int Weight { get { return _weight; } }
       
        public float GCost { get { return _gCost; } set { _gCost = value; } }
        public float HCost { get { return _hCost; } set { _hCost = value; } }
        public Node ParentNode { get { return _parentNode; } set { _parentNode = value; } }
        public List<Node> Neighbors { get { return _neighbors;  } set { _neighbors = value;  } }
        
    }


}
