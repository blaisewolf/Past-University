using TournamentGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentGame.Model.Characters;

namespace TournamentGame.Model.Fields
{
    class FinishField : MonsterField
    {
        public FinishField(int id)
        {
            Id = id;
            ImageSource = "/Assets/FieldIcons/castle.png";
            Monster = new Dragon(id);
        }
    }
}
