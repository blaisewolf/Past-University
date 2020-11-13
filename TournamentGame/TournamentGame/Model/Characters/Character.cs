using System;
using TournamentGame.Assist;

namespace TournamentGame.Model
{
    public abstract class Character : PropertyAssistant
    {
        protected string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        private int _maxHp;
        public int MaxHp
        {
            get => _maxHp;
            set => SetProperty(ref _maxHp, value);
        }
        protected int _hp;
        public int Hp
        {
            get => _hp;
            set => SetProperty(ref _hp, value);
        }
        protected int _attack;
        public int Attack
        {
            get => _attack;
            set => SetProperty(ref _attack, value);
        }
    }
}
