using HeuristicsCompetitionMathBlade.Classes.Grid_Classes;
using HeuristicsCompetitionMathBlade.Classes.Support_Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace HeuristicsCompetitionMathBlade.Classes
{
    class GridManager
    {

        private const string GRID_PARAMETER = "-g";
        private const string FILE_PARAMETER_NOT_FOUND = "FILE PARAMETER NOT FOUND";

        Dictionary<string, string> _commandLineParameters;
        Grid _givenGrid;
        List<Node> _path;
        String _helpString;

        public Grid GivenGrid { get => _givenGrid; set => _givenGrid = value; }

        public GridManager(string[] args)
        {
            _helpString = "One parameter must be -g with the .txt grid file to be read in. \n " +
                "The remaining parameters must be -x and a number, where the x is the letter in the grid, and the number is the weight of that space. \n " +
                "Anything omitted is impassable. \n " +
                "Should the Start be something other than S use -Y Start where Y is the letter that represents the start " + "\n " +
                "Should the End be something other than -f use -Q End where Q is the letter of the end in the graph. \n" + 
                "Example1: dotnet HeuristicsCompetitionMathBlade.dll -g \"Grid 1.txt\" -z 3 -h 5 -o 1\n" +
                "Example2: dotnet HeuristicsCompetitionMathBlade.dll -g \"{EntirePath}\\Grid 1.txt\" -z 3 -h 5 -o 1\n" +
                "You can also have the program not use \"real distance\" and instead assume the diagonals are a distance of 1 based on request. " +
                "Example3: dotnet HeuristicsCompetitionMathBlade.dll -g \"C: \\Users\\MathBlade\\source\\repos\\HeuristicsCompetitionMathBlade\\HeuristicsCompetitionMathBlade\\Data Files\\Grid 5.txt\" -o 1 -z 3 -h 5 -RealDistance false";


            _commandLineParameters = ReadParameters(args);
            try
            {
                _givenGrid = BuildGivenGridFromFile();
                Stopwatch stopwatchPath = new Stopwatch();
                stopwatchPath.Start();
                _path = PathCalculator.CalculateShortestPath(_givenGrid);
                stopwatchPath.Stop();
                _givenGrid.TimeToCalculatePath = stopwatchPath.ElapsedMilliseconds;

            }
            catch (ArgumentNullException e)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(e.Message);
            }

            
            
        }



        private Grid BuildGivenGridFromFile()
        {
            string fileName = _commandLineParameters.GetValueOrDefault(GRID_PARAMETER, FILE_PARAMETER_NOT_FOUND);
            if (fileName.Equals(FILE_PARAMETER_NOT_FOUND))
            {
                throw new ArgumentNullException("The grid parameter was not found. " + System.Environment.NewLine + _helpString);
            }

            

            return GridBuilder.BuildGridFromFileName(fileName, _commandLineParameters);
        }


        public void DisplayGivenGrid(List<Node> path)
        {
           
            bool pathDisplayOn = (path != null && path.Count > 0);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine((!pathDisplayOn ? "Given Grid Without Path" : "Given Grid With Path Highlighted Red")); 
            for(int y=0; y <= _givenGrid.YMax; y++)
            {
                for (int x=0; x <= _givenGrid.XMax; x++)
                {
                    Node aNode = _givenGrid.NodeAtLocation(x, y);
                    string drawString = null;
                    Console.ForegroundColor = ConsoleColor.Black;
                    switch(aNode.Weight)
                    {
                        case Node.NODE_END:
                            drawString = Node.END_CHAR.ToString();
                            break;
                        case Node.NODE_START:
                            drawString = Node.START_CHAR.ToString();
                            break;
                        default:
                            drawString = aNode.Weight + "";
                            break;
                    }
                    if (aNode != null && aNode.Weight != Node.NODE_IMPASSABLE)
                    {
                        if (pathDisplayOn && path.Contains(aNode))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        if (x == _givenGrid.XMax) {
                            Console.WriteLine(drawString);
                        }
                        else
                        {
                            Console.Write(drawString);
                        }
                        
                        
                    }
                    else if (aNode != null && aNode.Weight == Node.NODE_IMPASSABLE)
                    {
                        if (x == _givenGrid.XMax)
                        {
                            Console.WriteLine(DrawingHelper.BLOCK);
                        }
                        else
                        {
                            Console.Write(DrawingHelper.BLOCK);
                        }
                    }
                   

                }



            }


            if (pathDisplayOn)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                
              
                /*foreach(Node node in path)
                {
                    Console.WriteLine("Node X, Y - MovementCost " + node.GridLocation.X + "," + node.GridLocation.Y + " - " + node.GCost);
                   
                    
                }*/
                Console.WriteLine("Time to read data from file in milliseconds: " + _givenGrid.TimeToReadFromFile);
                Console.WriteLine("Time to calculate path in milliseconds: " + _givenGrid.TimeToCalculatePath);
                Console.WriteLine("Movement Cost: " + _givenGrid.EndNode.GCost);
              
            }


        }

        public void DisplayGivenGridWithPath()
        {
            DisplayGivenGrid(_path);
        }


 


        private Dictionary<string,string> ReadParameters(string[] args)
        {
            _commandLineParameters = DictionaryBuilder.BuildParameterDictionary(args);
            if (_commandLineParameters == null)
            {
                throw new ArgumentException("Your parameters for the grid program are invalid. " + System.Environment.NewLine + _helpString);
            }
            else
            {
                return _commandLineParameters; 
            }
        }
    }
}
