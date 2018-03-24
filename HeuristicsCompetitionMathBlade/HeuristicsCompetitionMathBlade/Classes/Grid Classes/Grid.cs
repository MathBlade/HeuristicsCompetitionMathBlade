using HeuristicsCompetitionMathBlade.Classes.Grid_Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HeuristicsCompetitionMathBlade.Classes.Support_Classes;

namespace HeuristicsCompetitionMathBlade.Classes
{
    public class Grid
    {        
        Node _startNode;
        Node _endNode;
        int _xMax;
        int _yMax;
        long _timeToReadFromFile;
        Dictionary<GridLocation, Node> _nodeDictionaryData;
        long _timeToCalculatePath;
        bool _useRealDistance;

        public static string DISTANCE_WORD = "RealDistance";


        public Grid(Dictionary<GridLocation,Node> nodeDictionaryData, int xMax, int yMax, Node startNode, Node endNode, long timeToReadFromFile, bool useRealDistance)
        {
            _nodeDictionaryData = nodeDictionaryData;
            _xMax = xMax;
            _yMax = yMax;
            _startNode = startNode;
            _endNode = endNode;
            _timeToReadFromFile = timeToReadFromFile;
            _useRealDistance = useRealDistance;
        }

        public List<Node> GetNeighbors(Node node)
        {
            if (node.Neighbors != null && node.Neighbors.Count != 0)
            {
                return node.Neighbors;
            }
            int startX = node.GridLocation.X;
            int startY = node.GridLocation.Y;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i == j && i == 0)
                    {
                        //This is the node itself;
                        continue;
                    }
                    Node nodeAtLocation = NodeAtLocation(startX + i, startY + j);
                    if (nodeAtLocation != null && nodeAtLocation.Weight != Node.NODE_IMPASSABLE)
                    {

                        node.AddNeighbor(nodeAtLocation);
                    }

                }
            }
            return node.Neighbors;
        }
        

        
        public Node NodeAtLocation (int x, int y)
        {
            
            GridLocation gridLocation = new GridLocation(x, y);
            if (_nodeDictionaryData.ContainsKey(gridLocation))
            {
                return _nodeDictionaryData[gridLocation];
            }

            return null;
        
        }


        public int XMax { get { return _xMax; } }
        public int YMax { get { return _yMax; } }        
        public Node StartNode {  get { return _startNode; } }
        public Node EndNode {  get { return _endNode; } }
        public long TimeToReadFromFile { get { return _timeToReadFromFile;  } }

        public long TimeToCalculatePath { get { return _timeToCalculatePath; } set { _timeToCalculatePath = value; }  }
        public bool UseRealDistance { get { return _useRealDistance; }  }
    }

    
}
