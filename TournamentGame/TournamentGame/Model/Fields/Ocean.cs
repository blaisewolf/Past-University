using TournamentGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentGame.Model.Characters;

namespace TournamentGame.Model.Fields
{
    class Ocean : MonsterField
    {
        public Ocean(int id)
        {
            Id = id;
            ImageSource = "/Assets/FieldIcons/ocean.png";
            Monster = new Mermaid(id);
        }
    }
}
