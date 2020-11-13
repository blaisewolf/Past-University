using System;
using System.Windows;
using TournamentGame.Assist;
using TournamentGame.Model;

namespace TournamentGame.ViewModel
{
    public class BattleProcessor : PropertyAssistant
    {
        public BattleProcessor() { }
        public void Fight(Monster monster, Player player, RollingDice dice)
        {
            if (CheckMonsterAlive(monster) && CheckPlayerAlive(player))
            {
                AttackMonster(player, monster ,dice.Result);
                MessageBox.Show("Damage to " + monster.Name + ": "+ ( player.Attack + dice.Result ) );
                if (CheckMonsterAlive(monster))
                {
                    MessageBox.Show(monster.Name + " attack: " + monster.Attack);
                    MonsterAttack(player, monster);
                }
            }
        }
        public void MonsterAttack(Player player, Monster monster)
        {
            player.Hp -= monster.Attack;
        }
        public void AttackMonster(Player player, Monster monster, int damageRoll)
        {
            monster.Hp -= player.Attack + damageRoll;
        }
        public void PunishPlayer(Player player, RollingDice dice)
        {
            dice.Roll();
            if (player.FieldNumber - dice.Result < 0)
            {
                player.FieldNumber = 0;
            }
            else
            {
                player.FieldNumber -= dice.Result;
            }
        }
        public bool CheckMonsterAlive(Monster monster)
        {
            return (monster.Hp > 0) ? true : false;
        }
        public bool CheckPlayerAlive(Player player)
        {
            return (player.Hp < 1) ? false : true;
        }
        public bool CheckFinalBoss(Monster monster, int fieldNumber)
        {
            return (fieldNumber == 99 && !CheckMonsterAlive(monster)) ? true : false;
        }
    }
}
