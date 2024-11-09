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

        //Method to clear a row
        private void ClearRow(int r)
        {
            for (int c = 0; r < Columns; c++)
            {
                grid[r, c] = 0;
            }
        }

        //Method to move a row down by a certain number of rows
        private void MoveRowDown(int r, int numRows)
        {
            for (int c=0; c< Columns; c++)
            {
                grid[r + numRows, c] = grid[r, c];
                grid[r, c] = 0;
            }
            //now we can implement a ClearFullRows method
        }

        //Method to clear full rows
        public int ClearFullRows()
        {
            //the cleared variable starts at 0 and we move from the bottom row towards the top
            int cleared = 0;
            for (int r = Rows - 1; r >= 0; r--)
            {
                //we check if the current row is full and if it is => we clear it and increment 'cleared'
                if (IsRowFull(r))
                {
                    ClearRow(r);
                    cleared++;
                }
                else if (cleared > 0)
                {
                    //otherwise if 'cleared' is greater than 0 then we move the current row down by the number of cleared rows 
                    MoveRowDown(r, cleared);
                }
            }
            //in the end we return the number of cleared rows
            return cleared;
        }

    }
}
