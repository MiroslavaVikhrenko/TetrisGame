using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    //an abstract parent class => later we define sub-clases for each block
    public abstract class Block
    {
        //2-dimensional position array which contains the tile positions (1 block = 4 tiles) in the 4 rotation states
        protected abstract Position[][] Tiles { get; }
        //start offset which decides where the block spawns in the grid 
        protected abstract Position StartOffset { get; }
        //integer id which we need to distinguish the blocks
        public abstract int Id { get; }
        //store the current rotation state and the current offset
        private int rotationState;
        private Position offset;
        //in the constructor we set the offset = to the start offset 
        public Block()
        {
            offset = new Position(StartOffset.Row, StartOffset.Column);
        }
        //Method that returns the grid positions occupied by the block factoring in the current rotation and offset
        public IEnumerable<Position> TilePosition()
        {
            //loops over the tile positions in the current rotation state and adds the row offset and column offset
            foreach (Position p in Tiles[rotationState])
            {
                yield return new Position(p.Row + offset.Row, p.Column + offset.Column);
            }
        }
        //Method to rotate the block 90 degrees clockwise
        //we do that by incrementing the current rotation state wrapping around to 0 if it's in the final state
        public void RotateCW()
        {
            rotationState = (rotationState + 1) % Tiles.Length;
        }
        //Method to rotate the block counter-clockwise (similar to RotateCW())
        public void RotateCCW()
        {
            if (rotationState == 0)
            {
                rotationState = Tiles.Length - 1;
            }
            else
            {
                rotationState--;
            }
        }
        //Method to move the block by given number of rows and colums
        public void Move(int rows, int columns)
        {
            offset.Row += rows;
            offset.Column += columns;
        }
        //Method to reset the rotation and position
        public void Reset()
        {
            rotationState = 0;
            offset.Row = StartOffset.Row;
            offset.Column = StartOffset.Column;
        }
    }
}
