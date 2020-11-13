using TournamentGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentGame.Model.Characters;

namespace TournamentGame.Model.Fields
{
    class Mountain : MonsterField
    {
        public Mountain(int id)
        {
            Id = id;
            ImageSource = "/Assets/FieldIcons/mountain.png";
            Monster = new Yeti(id);
        }
    }
}
