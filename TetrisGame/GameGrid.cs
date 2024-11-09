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
    }
}
