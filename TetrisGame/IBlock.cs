using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    //a sub-class for I-Block
    public class IBlock : Block
    {
        //store the tile positions for the 4 rotation states
        private readonly Position[][] tiles = new Position[][]
        {
            new Position[]{new (1,0), new (1,1), new (1,2), new (1, 3) },
            new Position[]{new (0,2), new (1,2), new (2,2), new (3, 2) },
            new Position[]{new (2,0), new (2,1), new (2,2), new (2, 3) },
            new Position[]{new (0,1), new (1,1), new (2,1), new (3,1) }
        };
        //fill out the required properties
        //the id should be 1
        public override int Id => 1;
        //the start offset should be -1 3 => this will make the block spawn in the middle of the top row
        protected override Position StartOffset => new Position(-1, 3);
        //for the tiles property we freturn the tiles array above
        protected override Position[][] Tiles => tiles;
        //the functionality is in the base class 
    }
}
