using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class CratesPuzzle
    {
        private List<List<char>> puzzle;
        private List<char> cratesStack;
        private int instructionsNumber = 0;

        public CratesPuzzle()
        {
            puzzle = new List<List<char>>();
            cratesStack = new List<char>();
            puzzle.Add(cratesStack);
        }

        private char GetCrateInput()
        {
            char crate = ' ';
            Console.Write("\nEnter a char (a-z) for mark a crate: ");
            string input = Console.ReadLine()!;
            while (!char.TryParse(input, out crate) || !char.IsLetter(crate))
            {
                Console.Write("Enter a valid char (a-z) for mark a crate: ");
                input = Console.ReadLine()!;
            }
            return crate;
        }

        private bool ContainCrate(char crate)
        {
            foreach (List<char> cratesStack in puzzle)
            {
                foreach (char c in cratesStack)
                {
                    if (c == crate)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Draw()
        {
            while (true)
            {
                Console.WriteLine($"\nCurrent stack: Stack {puzzle.Count}.");
                PrintMenu();
                string option = Console.ReadLine()!;
                switch (option)
                {
                    case "1":
                        char crate = GetCrateInput();
                        while (ContainCrate(crate))
                        {
                            Console.WriteLine($"\nWarning: This crate [{char.ToUpper(crate)}] already exist!");
                            crate = GetCrateInput();
                        }
                        puzzle[puzzle.Count - 1].Add(crate);
                        break;
                    case "2":
                        if (cratesStack.Count > 0)
                        {
                            cratesStack = new List<char>();
                            puzzle.Add(cratesStack);
                        }
                        else
                        {
                            Console.WriteLine("\nWarning: You need at least one crate into the stack!");
                        }
                        break;
                    case "3":
                        if (puzzle.Count > 1)
                        {
                            return;
                        }
                        else
                        {
                            Console.WriteLine("\nWarning: You need at least two stacks!");
                        }
                        break;
                    default:
                        Console.WriteLine("\nWarning: Please choose a correct option!");
                        break;
                }
            }
        }

        public void CreateInstructions()
        {
            while (true)
            {
                Print2ndMenu();
                string option = Console.ReadLine()!;
                switch (option)
                {
                    case "1":
                        Instruction instruction = CreateInstruction();
                        while (!instruction.IsValid(puzzle))
                        {
                            Console.WriteLine("\nWarning: Recreate the instruction!");
                            instruction = CreateInstruction();
                        }
                        instruction.Execute(ref puzzle);
                        instructionsNumber++;
                        break;
                    case "2":
                        if (instructionsNumber > 0)
                        {
                            return;
                        }
                        else
                        {
                            Console.WriteLine("\nWarning: You need to create at least 1 instruction!\n");
                        }
                        break;
                    default:
                        Console.WriteLine("\nWarning: Select a valid option from the menu!\n");
                        break;
                }
            }
        }

        private Instruction CreateInstruction()
        {
            Console.Write("\nHow many crates do you want to move: ");
            int cratesMoved = GetInstructionInput();

            Console.Write("\nWhich stack do you want to move from: ");
            int sourceStack = GetInstructionInput();

            Console.Write("\nInto which stack do you want to move: ");
            int targetStack = GetInstructionInput();

            return new Instruction(cratesMoved, sourceStack, targetStack);
        }

        private int GetInstructionInput()
        {
            string input = Console.ReadLine()!;
            int output;
            while (!int.TryParse(input, out output))
            {
                Console.Write("Enter a valid integer number: ");
                input = Console.ReadLine()!;
            }
            return output;
        }

        public void Print()
        {
            Console.WriteLine();
            int stackNumber = 1;
            foreach (List<char> cratesStack in puzzle)
            {
                Console.Write($"Stack {stackNumber}: ");
                foreach (char crate in cratesStack)
                {
                    Console.Write(crate + " ");
                }

                Console.WriteLine();
                stackNumber++;
            }
            Console.WriteLine();
        }

        private void PrintMenu()
        {
            Console.WriteLine("1. Mark a crate.");
            Console.WriteLine("2. Go to next stack.");
            Console.WriteLine("3. Finish the puzzle.");
            Console.Write("Choose an option: ");
        }

        private void Print2ndMenu()
        {
            Console.WriteLine("\n--Instructions creator menu--");
            Console.WriteLine("1. Create an instruction.");
            Console.WriteLine("2. Exit instruction creator.");
            Console.Write("Choose an option: ");
        }

        public void Solution()
        {
            Console.Write("Solution: ");
            foreach(List<char> cratesStack in puzzle)
            {
                if(cratesStack.Count > 0)
                {
                    Console.Write(cratesStack.Last() + " ");
                }
            }
        }
    }
}

