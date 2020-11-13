using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentGame.Model
{
    public class Score : IComparable<Score>
    {
        public string Name { get; set; }
        public string Level { get; set; }
        public string Field { get; set; }
        public int iLvl { get; set; }

        public int CompareTo(Score that)
        {
            return this.iLvl.CompareTo(that.iLvl);
        }
    }
}
