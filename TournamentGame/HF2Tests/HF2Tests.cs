using TournamentGame.ViewModel;
using System;
using Xunit;
using TournamentGame.Model;
using TournamentGame.Model.Characters;

namespace HF2Tests
{
    public class HF2Tests
    {
        [Fact]
        public void Dice()
        {
            RollingDice rollingDice = new RollingDice();
            rollingDice.Roll();
            Assert.InRange(rollingDice.Result, 1, 6);
        }
        [Fact]
        public void OverStepProtectionTest()
        {
            GameEngine gameEngine = new GameEngine(1);
            Player player = new Player(2);
            player.FieldNumber = 98;
            gameEngine.RollDice();
            gameEngine.Step(player);
            Assert.Equal(99, player.FieldNumber);
        }
        [Fact]
        public void MonsterAttackTest()
        {
            RollingDice rollingDice = new RollingDice();
            Player player = new Player(2);
            BattleProcessor battleProcessor = new BattleProcessor();
            Monster monster = new Yeti(1);
            player.Attack = 100;
            battleProcessor.AttackMonster(player, monster, 5);
            Assert.False(battleProcessor.CheckMonsterAlive(monster));
        }
        [Fact]
        public void AttackMonsterTest()
        {
            RollingDice rollingDice = new RollingDice();
            Player player = new Player(2);
            BattleProcessor battleProcessor = new BattleProcessor();
            Monster monster = new Yeti(1);
            monster.Attack = 100;
            battleProcessor.MonsterAttack(player, monster);
            Assert.False(battleProcessor.CheckPlayerAlive(player));
        }
    }
}