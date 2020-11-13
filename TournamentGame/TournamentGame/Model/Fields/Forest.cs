using TournamentGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentGame.Model.Characters;

namespace TournamentGame.Model.Fields
{
    class Forest : MonsterField
    {
        public Forest(int id)
        {
            Id = id;
            ImageSource = "/Assets/FieldIcons/forest.png";
            Monster = new Spider(id);
        }
    }
}
