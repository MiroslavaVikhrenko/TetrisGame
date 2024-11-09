using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    //a sub-class for O-Block => this block is unique because it occuppies the same positions in every rotation state
    public class OBlock : Block
    {
        //store the tile positions for the 4 rotation states => we could copy and paste the same positions 4 times but that is unnesessary
        private readonly Position[][] tiles = new Position[][]
        {
            new Position[]{new (0,0), new (0,1), new (1,0), new (1, 1) }
        };
        //fill out the required properties
        //the id should be 4
        public override int Id => 4;
        //the start offset should be 0 4
        protected override Position StartOffset => new Position(0,4);
        //for the tiles property we freturn the tiles array above
        protected override Position[][] Tiles => tiles;
        //the functionality is in the base class 
    }
}
