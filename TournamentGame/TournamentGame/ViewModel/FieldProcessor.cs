using System;
using System.Collections.Generic;
using TournamentGame.Assist;
using TournamentGame.Model;
using TournamentGame.Model.Fields;

namespace TournamentGame.ViewModel
{
    class FieldProcessor : PropertyAssistant
    {
        public IDictionary<int, MonsterField> SetupFields()
        {
            IDictionary<int, MonsterField> fields = new Dictionary<int, MonsterField>();
            fields.Add(0, new StartField(0)); //first field = 0
            Random random = new Random();
            for (int i = 1; i < 99; i++)
            {
                fields.Add(i, RandomField(random.Next(1, 6), i + 1));
            }
            fields.Add(99, new FinishField(99)); //last field = 99
            return fields;
        }
        private MonsterField RandomField(int random, int id)
        {
            switch (random)
            {
                case 1:
                    return new Forest(id);
                case 2:
                    return new Fortress(id);
                case 3:
                    return new HauntedHouse(id);
                case 4:
                    return new Mountain(id);
                case 5:
                    return new Ocean(id);
                case 6:
                    return new Waterfall(id);
                default:
                    return new Forest(id);
            }
        }
    }
}
