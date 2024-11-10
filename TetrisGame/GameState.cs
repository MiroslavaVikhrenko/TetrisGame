using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    //class to handle the interactions between the parts we have written so far (GameGrid, blocks, BlockQueue, Position)
    public class GameState
    {
        //property with a backing field for the current block
        private Block currentBlock;
        public Block CurrentBlock
        {
            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.Reset();
                //when we update the current block the Reset() method is called to set the correct start position and rotation 
            }
        }

        //add properties for the game grid, the block queue and a game over boolean
        public GameGrid GameGrid { get; }
        public BlockQueue BlockQueue { get; }
        public bool GameOver { get; private set; }

        //in the constructor we initialize the game grid with 22 rows and 10 colums
        //we also initialize the block queue and use it to get a random block for the current block property
        public GameState()
        {
            GameGrid = new GameGrid(22, 10);
            BlockQueue = new BlockQueue();
            CurrentBlock = BlockQueue.GetAndUpdate();
        }

        //!!Method to check if the current block is in a legal position or not
        private bool BlockFits()
        {
            //loops over the tile positions of the current block and if any of them are outside the grid OR overlapping another tile
            //then we return false otherwise if we get through the entire loop we return true
            foreach (Position p in CurrentBlock.TilePosition())
            {
                if (!GameGrid.IsEmpty(p.Row, p.Column))
                {
                    return false;
                }
            }
            return true;
        }

        //Method to rotate the current block clockwise BUT ONLY IF it's possible to do so from where it is
        public void RotateBlockCW()
        {
            CurrentBlock.RotateCW();
            if (!BlockFits())
            {
                CurrentBlock.RotateCCW();
            }
            //the strategy we use is simply rotate the block and if it ends up in an illegal position then we rotate it back
        }
        //Method to rotate the current block counter-clockwise (same strategy as in RotateBlockCW() method)
        public void RotateBlockCCW()
        {
            CurrentBlock.RotateCCW();
            if (!BlockFits())
            {
                CurrentBlock.RotateCW();
            }
        }

        //Methods to move the current block left and right => the strategy will be the same as above
        //we try to move it and if it moves to an illegal position then we move it back
        public void MoveBlockLeft()
        {
            CurrentBlock.Move(0, -1);
            if (!BlockFits())
            {
                CurrentBlock.Move(0, 1);
            }
        }
        public void MoveBlockRight()
        {
            CurrentBlock.Move(0, 1);
            if (!BlockFits())
            {
                CurrentBlock.Move(0, -1);
            }
        }
    }
}
