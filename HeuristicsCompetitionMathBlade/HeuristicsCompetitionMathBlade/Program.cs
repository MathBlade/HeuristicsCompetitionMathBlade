using HeuristicsCompetitionMathBlade.Classes;
using System;
using System.Collections.Generic;

namespace HeuristicsCompetitionMathBlade
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            
            Console.Clear();
            
            try
            {
                GridManager gridManager = new GridManager(args);
                if (gridManager.GivenGrid != null)
                { 
                    //gridManager.DisplayGivenGrid(null);
                    gridManager.DisplayGivenGridWithPath();
                }
                else
                {
                    
                    Console.WriteLine("One or more errors prevented processing. Please fix command line parameters before rerunning.");
                    
                    
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("An error has occurred: " + e.Message);
            }

            
           

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey(true);
        }

       
    }
}
