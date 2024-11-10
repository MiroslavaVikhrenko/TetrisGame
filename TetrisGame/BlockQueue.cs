using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    //Block Queue class responsible for picking the next block in the game
    public class BlockQueue
    {
        //contains a block array with an instance of the 7 block classes which we will recycle
        private readonly Block[] blocks = new Block[]
        {
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new OBlock(),
            new SBlock(),
            new TBlock(),
            new ZBlock()
        };
        //we also need a random object 
        private readonly Random random = new Random();
        //a property for the next block in the queue
        public Block NextBlock {  get; private set; }
        //when we write the UI we will preview this block so the player knows what's coming
        //you could also store an array here containing the next few blocks and preview all of them

        //in the constructor we initialize the next block with a random block
        public BlockQueue()
        {
            NextBlock = RandomBlock();
        }

        //Method to return a random block
        private Block RandomBlock()
        {
            return blocks[random.Next(blocks.Length)];
        }
        
        //Method to return the next block and updates the property
        public Block GetAndUpdate()
        {
            //since we don't want to return the same block twice in a row => we keep picking until we get a new one 
            Block block = NextBlock;
            do
            {
                NextBlock = RandomBlock();
            }
            while(block.Id == NextBlock.Id);
            return block;
        }
    }
}
