namespace TournamentGame.Model
{
    using System;
    using TournamentGame.Model.Characters;

    class StartField : MonsterField
    {
        public StartField(int id)
        {
            Id = id;
            ImageSource = "/Assets/FieldIcons/arrow-dunk.png";
            Monster = new Dummy();  
        }
    }
}
