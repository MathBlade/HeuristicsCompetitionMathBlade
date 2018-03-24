using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HeuristicsCompetitionMathBlade.Classes.Grid_Classes;
using System.Timers;
using System.Diagnostics;

namespace HeuristicsCompetitionMathBlade.Classes.Support_Classes
{
    public static class GridBuilder
    {
        public static Grid BuildGridFromFileName(string fileName, Dictionary<string,string> weights)
        {
            Stopwatch stopwatchForGridBuilding = new Stopwatch();
            stopwatchForGridBuilding.Start();

            bool useRealDistance = true;//Default to true if there's some issue with the parameters.

            //Verify the file's existence. 
            if (!File.Exists(fileName))
            {
                string path = Environment.CurrentDirectory + "\\Data Files\\";
              
                fileName = path + fileName;
                if (!File.Exists(fileName))
                {
                    throw new ArgumentNullException("Error: The file provided could not be found. Please verify the file " + fileName + " exists.");

                }
            }

            //Read the file into a string array.
            string[] fileData = null;
            try
            {
               
                fileData = File.ReadAllLines(fileName);
               

            }
            catch
            {
                throw new ArgumentException("An error occurred reading the file. Please verify permissions.");
            }

            string useRealDistanceString = null;
            bool ableToExtractRealDistanceSetting = weights.TryGetValue("-" + Grid.DISTANCE_WORD, out useRealDistanceString);
            
            if (ableToExtractRealDistanceSetting)
            {
                bool.TryParse(useRealDistanceString, out useRealDistance);
            }

            Dictionary<GridLocation, Node> nodeList = new Dictionary<GridLocation, Node>();
            //List<Node> nodes = new List<Node>();
            int y = 0;
            int xMax = 0;
            Node startNode = null;
            Node endNode = null;
            try
            {
                
                for (y=0; y < fileData.Length; y++)
                {
                    string[] nodesOnLine = fileData[y].Trim().Split(Node.NODE_DIVIDER);
                    int x = 0;
                    for (x=0; x < nodesOnLine.Length; x++)
                    {
                        string nodeGiven = nodesOnLine[x].ToLower();
                        string weightFound = null;
                        
                        

                        if (!weights.TryGetValue("-" + nodeGiven, out weightFound) && !weights.TryGetValue(nodeGiven, out weightFound))
                        {

                            


                            weights.Add("-" + nodeGiven, Node.NODE_IMPASSABLE + "");
                            if (!weights.TryGetValue("-" + nodeGiven, out weightFound))
                            {
                                throw new Exception("Could not add impassible weight for given value.");
                            }
                        }                       
                            
                        int weightAmount = Node.NODE_IMPASSABLE;
                        if ((weightFound == null) || (int.TryParse(weightFound, out weightAmount) == false))
                        {
                            if (weightFound != null)
                            {
                                
                                if (weightFound.ToLower().Equals(Node.START_WORD.ToLower()))
                                {
                                    GridLocation newGridLocation = new GridLocation(x, y);
                                    startNode = new Node(newGridLocation, Node.NODE_START);
                                    startNode.GCost = 0;
                                    nodeList.Add(newGridLocation, startNode);
                                  
                                    continue;
                                }
                                if (weightFound.ToLower().Equals(Node.END_WORD.ToLower()))
                                {
                                    GridLocation newGridLocation = new GridLocation(x, y);
                                    endNode = new Node(newGridLocation, Node.NODE_END);
                                    endNode.HCost = 0;
                                    nodeList.Add(newGridLocation, endNode);
                                  
                                    continue;
                                }
                            }

                            throw new Exception("Could not build node data for the given file. Please try again.");
                        }
                        else
                        {
                            GridLocation newGridLocation = new GridLocation(x, y);
                            nodeList.Add(newGridLocation, new Node(newGridLocation, weightAmount));
                            
                        }



                    }

                    if (xMax < x -1)
                    {
                        xMax = x -1;
                    }
                }
            }
            catch
            {
                
                throw new ArgumentException("An error occurred while parsing the grid into a collection of nodes.");
            }

            

            stopwatchForGridBuilding.Stop();

            return new Grid(nodeList,xMax,y-1, startNode, endNode, stopwatchForGridBuilding.ElapsedMilliseconds, useRealDistance);
        }
    }
}
