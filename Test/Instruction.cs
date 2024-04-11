using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Instruction
    {
        private int cratesNumber;
        private int sourceStack;
        private int targetStack;

        public Instruction(int cratesNumber, int sourceStack, int targetStack)
        {
            this.cratesNumber = cratesNumber;
            this.sourceStack = sourceStack - 1;
            this.targetStack = targetStack - 1;
        }

        public void Print()
        {
            Console.WriteLine($"\nMove {cratesNumber} crates from stack {sourceStack + 1} to stack {targetStack + 1}.");
        }

        public bool IsValid(List<List<char>> puzzle)
        {
            try
            {
                return sourceStack <= puzzle.Count - 1 && targetStack <= puzzle.Count - 1 && sourceStack != targetStack && cratesNumber <= puzzle[sourceStack].Count;
                //stiva origine si destinatie trebuie sa existe
                //stiva origine si destinatie trebuie sa fie diferite
                //limiti cutii din stiva origine
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Execute(ref List<List<char>> puzzle)
        {
            int cratesMoved = 0;
            while (cratesMoved < cratesNumber)
            {
                char crate = puzzle[sourceStack].Last();
                puzzle[targetStack].Add(crate);
                puzzle[sourceStack].Remove(crate);
                cratesMoved++;
            }
        }
    }
}
