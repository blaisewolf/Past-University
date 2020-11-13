using TournamentGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentGame.Model.Characters;

namespace TournamentGame.Model.Fields
{
    class HauntedHouse : MonsterField
    {
        public HauntedHouse(int id)
        {
            Id = id;
            ImageSource = "/Assets/FieldIcons/witchhouse.png";
            Monster = new Ghost(id);
        }
    }
}
