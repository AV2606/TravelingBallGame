using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    /// <summary>
    /// The finish status of a round in a <see cref="Game"/>
    /// </summary>
    enum Finish
    {
        /// <summary>
        /// The round has cleard.
        /// </summary>
        Cleard=1,
        /// <summary>
        /// The player has tried to move out side of the map.
        /// </summary>
        LeftGameZone=0xe7707,
        /// <summary>
        /// The player has colided with a shape on the map
        /// </summary>
        SmashedWithShape=0x101,
        /// <summary>
        /// The round couldnt be started because the player has no more rounds left.
        /// </summary>
        OutOfRounds=0,
        /// <summary>
        /// The map for some reason could not be initialized.
        /// </summary>
        MapCantBeInitialized=0x00b5,
        /// <summary>
        /// A message that indicates basicly nothing.
        /// </summary>
        EmptyMessage=0xa11800d,
        /// <summary>
        /// The ball is tring to jump back to its currently cell.
        /// </summary>
        TriesToReJump=0xba11ba1,
        /// <summary>
        /// The ball tried to revisit a cell twice in the same round.
        /// </summary>
        RegularVisitor=0x80
    }
}
