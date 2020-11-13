using TournamentGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentGame.Model.Characters
{
    class Dummy : Monster
    {
        public Dummy()
        {
            Name = "No monster here";
            MaxHp = Hp = 0;
            Attack = 0;
        }
    }
}
