using TournamentGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentGame.Model.Characters;

namespace TournamentGame.Model.Fields
{
    class Fortress : MonsterField
    {
        public Fortress(int id)
        {
            Id = id;
            ImageSource = "/Assets/FieldIcons/fort.png";
            Monster = new Raider(id);
        }
    }
}
