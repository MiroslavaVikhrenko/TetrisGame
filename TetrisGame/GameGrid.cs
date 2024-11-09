using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    
    public class GameGrid
    {
        //2-dimensional rectangular array - the 1st dim is  row and the 2nd is column
        private readonly int[,] grid;
        //properties for the number of rows and columns
        public int Rows { get; }
        public int Columns { get; }
        //define an indexer to provide an easy access to the array = we can use indexing directly on a gamegrid object
        public int this[int r, int c] 
        {
            get => grid[r, c];
            set => grid[r, c] = value;
        }
        //constructor takes number of rows and columns as parameters => this way the class can also be used in micro and macro version of Tetris with untraditional grid size
        public GameGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            grid = new int[Rows, Columns];
        }

        //convenience methods 

        //Method to check if the given row and column is inside the grid or not
        public bool IsInside(int r, int c)
        { 
            return r >= 0 && r < Rows && c >= 0 && c < Columns;
        }

        //Method to check if a given cell is empty or not
        public bool IsEmpty(int r, int c)
        {
            //it must be inside the grid and the value at that entry in the array must be 0
            return IsInside(r, c) && grid[r, c] == 0;
        }

        //Method to check if an entire row is full
        public bool IsRowFull(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        //Method to check if an entire row is empty
        public bool IsRowEmpty(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] != 0)
                {
                    return false;
                }
            }
            return true;

        }
    }
}
