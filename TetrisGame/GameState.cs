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
                
                //fixing the spawn position - so far the blocks spawn in the two hidden rows but if they space in row 2 and 3
                //the top visible rows it would look better if they spawnned there
                //below logic will move the block down by 2 rows if nothing is in the way
                for (int i = 0; i <2; i++)
                {
                    currentBlock.Move(1, 0);
                    if (!BlockFits())
                    {
                        currentBlock.Move(-1, 0);
                    }
                }
            }
        }

        //add properties for the game grid, the block queue and a game over boolean
        public GameGrid GameGrid { get; }
        public BlockQueue BlockQueue { get; }
        public bool GameOver { get; private set; }

        //add Score property - here the score will be the total number of rows cleared => PlaceBlock() method
        public int Score { get; private set; }
        //add properties for held block and CanHold boolean => in the constructor we set CanHold to true
        public Block HeldBlock { get; private set; }
        public bool CanHold { get; private set; }


        //in the constructor we initialize the game grid with 22 rows and 10 colums
        //we also initialize the block queue and use it to get a random block for the current block property
        public GameState()
        {
            GameGrid = new GameGrid(22, 10);
            BlockQueue = new BlockQueue();
            CurrentBlock = BlockQueue.GetAndUpdate();
            CanHold = true;
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

        //Method to hold a block
        public void HoldBlock()
        {
            //if we cannot hold then we just return 
            if (!CanHold)
            {
                return;
            }
            //if there is no block on hold => we set the held block to the current block and the current block to the next block

            if (HeldBlock == null)
            {
                HeldBlock = CurrentBlock;
                CurrentBlock = BlockQueue.GetAndUpdate();               
            }
            else
            {
                //if there is a block on hold we have to swap the current block and the held block
                Block tmp = CurrentBlock;
                CurrentBlock = HeldBlock;
                HeldBlock = tmp;
            }
            //in the end we set CanHold to false so we cannot just spam hold
            CanHold = false;
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
        //Method to check if the game was lost
        private bool IsGameOver()
        {
            return !(GameGrid.IsRowEmpty(0) && GameGrid.IsRowEmpty(1));
            //if either of the hidden rows at the top are not empty => then the game is lost
        }
        //Method to be called when the current block cannot be moved down
        private void PlaceBlock()
        {
            //first it loops over the tile positions of the current block and sets those positions in the game grid equal to the block's id
            foreach (Position p in CurrentBlock.TilePosition())
            {
                GameGrid[p.Row, p.Column] = CurrentBlock.Id;
            }
            //then we clear any potentially full rows - recall that ClearFullRows() returns the number of cleared rows =>
            //we can increment the Score by that amount
            Score += GameGrid.ClearFullRows();
            //and check if the game is over => if it is we set gameover property to true
            if (IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                //if not => we update the current block
                CurrentBlock = BlockQueue.GetAndUpdate();
                CanHold = true;
            }
            //now we can write a move down method           
        }

        //Method to move the current block down
        public void MoveBlockDown()
        {
            CurrentBlock.Move(1, 0);
            if (!BlockFits())
            {
                //same strategy as above for other move methods
                CurrentBlock.Move(-1, 0);
                //call PlaceBlock() method in case the block cannot be moved down
                PlaceBlock();
            }
        }
    }
}
