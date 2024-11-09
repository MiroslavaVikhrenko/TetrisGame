using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    //a sub-class for L-Block
    public class LBlock : Block
    {
        //store the tile positions for the 4 rotation states
        private readonly Position[][] tiles = new Position[][]
        {
            new Position[]{new (0,2), new (1,0), new (1,1), new (1,2) },
            new Position[]{new (0,1), new (1,1), new (2,1), new (2,2) },
            new Position[]{new (1,0), new (1,1), new (1,2), new (2,0) },
            new Position[]{new (0,0), new (0,1), new (1,1), new (2,1) }
        };
        //fill out the required properties
        //the id should be 3
        public override int Id => 3;
        //the start offset should be 0 3 
        protected override Position StartOffset => new Position(0, 3);
        //for the tiles property we freturn the tiles array above
        protected override Position[][] Tiles => tiles;
        //the functionality is in the base class 
    }
}
