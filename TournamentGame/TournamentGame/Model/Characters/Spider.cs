using TournamentGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentGame.Model.Characters
{
    class Spider : Monster
    {
        public Spider(int id)
        {
            Name = "Pok";
            MaxHp = Hp = 10 + rnd.Next(1, id);
            Attack = 5 + rnd.Next(1, id);
        }
    }
}
