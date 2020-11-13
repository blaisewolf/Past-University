namespace TournamentGame.Model
{
    using System;
     public class Player : Character
     {
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        private int _fieldNumber; 
        public int FieldNumber
        {
            get => _fieldNumber;
            set => SetProperty(ref _fieldNumber, value);
        }
        private int _turnPhase;
        public int TurnPhase
        {
            get => _turnPhase;
            set => SetProperty(ref _turnPhase, value);
        }
        private int _level;
        public int Level
        {
            get => _level;
            set => SetProperty(ref _level, value);
        }
        //private Items equipped;
        public Player(int id) //lvl1 player létrehozása
        {
            Id = id; // egyedi id a körök meghatározásához, stb
            TurnPhase = 0;
            MaxHp = Hp = 15;
            Attack = 5;
            Level = 1;
            FieldNumber = 0;
            Name = "Player " + (id + 1);
        }
        public void LevelUp()
        {
            Level++;
            MaxHp += 10;
            Hp = MaxHp;
            Attack += 5;
        }
    }
}
