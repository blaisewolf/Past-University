using TournamentGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentGame.Model.Characters;

namespace TournamentGame.Model.Fields
{
    class Waterfall : MonsterField
    {
        public Waterfall(int id)
        {
            Id = id;
            ImageSource = "/Assets/FieldIcons/waterfall.png";
            Monster = new FishMonster(id);
        }
    }
}
