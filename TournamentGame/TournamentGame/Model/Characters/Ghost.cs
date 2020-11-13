using TournamentGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentGame.Model.Characters
{
    class Ghost : Monster
    {
        public Ghost(int id)
        {
            Name = "Spooker";
            MaxHp = Hp = 10 + rnd.Next(1, id);
            Attack = 5 + rnd.Next(1, id);
        }
    }
}
