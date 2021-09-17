using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    /// <summary>
    /// Holds the result of a <see cref="Game"/>.
    /// </summary>
    class Results
    {
        private static string[] titles = new string[] { "RegularVisitor", "S.W.A.T", "Explorer", "Clumsy" };
        private static string getTitle(Results r)
        {
            if (r.Clearnce > r.ShapesColisions && r.Clearnce > r.ReVisits && r.Clearnce > r.TimesPlayerGotOutOfTheMap)
                return titles[1];
            if (r.ShapesColisions > r.Clearnce && r.ShapesColisions > r.ReVisits && r.ShapesColisions > r.TimesPlayerGotOutOfTheMap)
                return titles[3];
            if (r.ReVisits > r.Clearnce && r.ReVisits > r.ShapesColisions && r.ReVisits > r.TimesPlayerGotOutOfTheMap)
                return titles[0];
            return titles[2];
        }

        /// <summary>
        /// The point the player has earned.
        /// </summary>
        public int GoodPoints { get; set; }
        /// <summary>
        /// The name of the player.
        /// </summary>
        public string PlayerName { get; set; }
        /// <summary>
        /// The round the results are calculated from.
        /// </summary>
        public int Round { get; set; }
        /// <summary>
        /// The number of times in the game the player has tried to leave the play area.
        /// </summary>
        public int TimesPlayerGotOutOfTheMap { get; set; }
        /// <summary>
        /// The number of times the ball has been thrown into a shape.
        /// </summary>
        public int ShapesColisions { get; set; }
        /// <summary>
        /// How many times the player has cleared the map.
        /// </summary>
        public int Clearnce { get; set; }
        /// <summary>
        /// How many times the player has tried to revisit a cell.
        /// </summary>
        public int ReVisits { get; set; }
        /// <summary>
        /// A title of the player based on his gameplay.
        /// </summary>
        public string PlayerTitle => getTitle(this);

        public void AddToResult(Finish finish)
        {
            switch (finish)
            {
                case Finish.Cleard:
                    Clearnce++;
                    break;
                case Finish.LeftGameZone:
                    this.TimesPlayerGotOutOfTheMap++;
                    break;
                case Finish.RegularVisitor:
                    this.ReVisits++;
                    break;
                case Finish.SmashedWithShape:
                    ShapesColisions++;
                    break;
            }
        }
    }
}
