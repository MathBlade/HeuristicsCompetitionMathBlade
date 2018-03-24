using System;
using System.Collections.Generic;
using System.Text;

namespace HeuristicsCompetitionMathBlade.Classes.Support_Classes
{
    public class DrawingHelper
    {
        public const string BLOCK = "█";
        public const string UPPER_LEFT_CORNER = "╔";
        public const string LOWER_LEFT_CORNER = "╚";
        public const string UPPER_RIGHT_CORNER = "╗";
        public const string LOWER_RIGHT_CORNER = "╝";
        public const string HORIZONTAL = "═";
        public const string VERTICAL = "║";

        public static void RectangleWithCharacter(Char character)
        {
            //return null;
            Console.Write(UPPER_LEFT_CORNER);
            Console.Write(HORIZONTAL);
            Console.WriteLine(UPPER_RIGHT_CORNER);
            Console.Write(VERTICAL);
            Console.Write(character);
            Console.WriteLine(VERTICAL);
            Console.Write(LOWER_LEFT_CORNER);
            Console.Write(HORIZONTAL);
            Console.Write(LOWER_RIGHT_CORNER);
            
        }
    }
}
