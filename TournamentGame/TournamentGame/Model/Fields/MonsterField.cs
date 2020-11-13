namespace TournamentGame.Model
{
    using System;
    using TournamentGame.Assist;

    public abstract class MonsterField : PropertyAssistant
    {
        private Monster _monster;
        public Monster Monster
        {
            get => _monster;
            set => SetProperty(ref _monster, value);
        }
        protected int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        protected string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }
        public int GetMonsterHp()
        {
            return Monster.Hp;
        }
        public int GetMonsterAttack()
        {
            return Monster.Attack;
        }
        public int GetMonsterMaxHp()
        {
            return Monster.MaxHp;
        }
        public string GetMonsterName()
        {
            return Monster.Name;
        }
        public void DamageMonster(int damage)
        {
            Monster.Hp -= damage;
        } 
    }
}
