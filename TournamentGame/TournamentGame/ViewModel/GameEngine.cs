namespace TournamentGame.ViewModel
{
    using TournamentGame.Model;
    using System;
    using System.Collections.Generic;
    using TournamentGame.Assist;
    using System.Windows;
    using System.Linq;
    using System.IO;

    public class GameEngine : PropertyAssistant
    {
        #region TurnInfo
        private int _playersCount;
        public int PlayersCount
        {
            get => _playersCount;
            set => SetProperty(ref _playersCount, value);
        }
        private int _currentPlayerId; // jelenlegi játékos
        public int CurrentPlayerId
        {
            get => _currentPlayerId;
            set => SetProperty(ref _currentPlayerId, value);
        }
        /*private int _turnPhase;
        public int TurnPhase
        {
            get => _turnPhase;
            set => SetProperty(ref _turnPhase, value);
        }*/
        #endregion
        #region ViewInfo
        private string _currentDiceViewPath; // kocka oldala
        public string CurrentDiceViewPath
        {
            get => _currentDiceViewPath;
            set => SetProperty(ref _currentDiceViewPath, value);
        }
        private string _currentMonsterName; // jelenlegi ellenfél neve
        public string CurrentMonsterName
        {
            get => _currentMonsterName;
            set => SetProperty(ref _currentMonsterName, value);
        }
        private string _currentMonsterAttack; // jelenlegi ellenfél ereje
        public string CurrentMonsterAttack
        {
            get => _currentMonsterAttack;
            set => SetProperty(ref _currentMonsterAttack, value);
        }
        private string _currentMonsterHp; // jelenlegi ellenfél hp-ja
        public string CurrentMonsterHp
        {
            get => _currentMonsterHp;
            set => SetProperty(ref _currentMonsterHp, value);
        }
        private string _currentMonsterMaxHp; // jelenlegi ellenfél hp-ja
        public string CurrentMonsterMaxHp
        {
            get => _currentMonsterMaxHp;
            set => SetProperty(ref _currentMonsterMaxHp, value);
        }
        private string _currentPlayerName; // jelenlegi player neve
        public string CurrentPlayerName
        {
            get => _currentPlayerName;
            set => SetProperty(ref _currentPlayerName, value);
        }
        private string _currentPlayerAttack; // jelenlegi player támadása
        public string CurrentPlayerAttack
        {
            get => _currentPlayerAttack;
            set => SetProperty(ref _currentPlayerAttack, value);
        }
        private string _currentPlayerHp; // jelenlegi player hp-ja
        public string CurrentPlayerHp
        {
            get => _currentPlayerHp;
            set => SetProperty(ref _currentPlayerHp, value);
        }
        private string _currentPlayerMaxHp; // jelenlegi player max hp-ja
        public string CurrentPlayerMaxHp
        {
            get => _currentPlayerMaxHp;
            set => SetProperty(ref _currentPlayerMaxHp, value);
        }
        private string _currentPlayerFieldNumber; // jelenlegi player max hp-ja
        public string CurrentPlayerFieldNumber
        {
            get => _currentPlayerFieldNumber;
            set => SetProperty(ref _currentPlayerFieldNumber, value);
        }
        private string _currentPhase; // jelenlegi player max hp-ja
        public string CurrentPhase
        {
            get => _currentPhase;
            set => SetProperty(ref _currentPhase, value);
        }
        #endregion
        private List<Score> _currentScores;
        public List<Score> CurrentScores { get => _currentScores; set => SetProperty(ref _currentScores, value); }
        private IDictionary<int, string> _fieldsSource;
        public IDictionary<int, string> FieldsSource
        {
            get => _fieldsSource;
            set => SetProperty(ref _fieldsSource, value);
        }
        private IDictionary<int, Player> players;
        private RollingDice dice;
        private IDictionary<int, MonsterField> fields;
        private FieldProcessor fieldProcessor;
        private BattleProcessor battleProcessor;
        public CommandBase RollDiceCommand { get; private set; }
        public CommandBase ExitCommand { get; private set; }
        public CommandBase ScoresCommand { get; private set; }
        #region Constructor
        public GameEngine(int numberOfPlayers)
        {
            PlayersCount = numberOfPlayers;
            //TurnPhase = 0;
            CurrentPlayerId = 0;
            dice = new RollingDice();
            CurrentDiceViewPath = dice.ViewPath;
            RollDiceCommand = new CommandBase(ExecuteTurnPahse);
            ExitCommand = new CommandBase(ExitGame);
            ScoresCommand = new CommandBase(ShowScores);
            players = new Dictionary<int, Player>();
            battleProcessor = new BattleProcessor();
            fieldProcessor = new FieldProcessor();
            for (int i = 0; i < PlayersCount; i++)
            {
                players.Add(i, new Player(i));
            }
            fields = fieldProcessor.SetupFields();
            FieldsSource = new Dictionary<int, string>();
            CurrentScores = new List<Score>();
            SetupFieldImages();
            UpdateInfo();
            CreateScores();
        }
        #endregion
        #region Events
        public void RollDice()
        {
            dice.Roll();
            CurrentDiceViewPath = dice.ViewPath;
        }
        public void ExecuteTurnPahse()
        {
            Monster monster = fields[players[CurrentPlayerId].FieldNumber].Monster;
            Player player = players[CurrentPlayerId];
            RollDice();
            if (players[CurrentPlayerId].TurnPhase == 0)
            {
                Step(player);
                players[CurrentPlayerId].TurnPhase ++;
            }
            else
            {
                if (battleProcessor.CheckPlayerAlive(player) && battleProcessor.CheckMonsterAlive(monster))
                {
                    battleProcessor.Fight(monster, player, dice);
                }
                if (!battleProcessor.CheckPlayerAlive(player) && battleProcessor.CheckMonsterAlive(monster))
                {
                    MessageBox.Show("Lose");
                    battleProcessor.PunishPlayer(player, dice);
                    players[CurrentPlayerId].TurnPhase = 0;
                    NextPlayer();
                }
                else if (battleProcessor.CheckPlayerAlive(player) && !battleProcessor.CheckMonsterAlive(monster))
                {
                    MessageBox.Show("Win");
                    player.LevelUp();
                    players[CurrentPlayerId].TurnPhase = 0;
                    NextPlayer();
                }
                else if (!battleProcessor.CheckPlayerAlive(player) && !battleProcessor.CheckMonsterAlive(monster))
                {
                    MessageBox.Show("Draw");
                    players[CurrentPlayerId].TurnPhase = 0;
                    NextPlayer();
                }
                if (battleProcessor.CheckFinalBoss(monster, player.FieldNumber))
                {
                    GameOverScenario(player);
                }
            }
            UpdateInfo();
            UpdateScores();
        }
        public void GameOverScenario(Player player)
        {
            string text = "Winner is :" + player.Name + "@New game?";
            SaveScores();
            text = text.Replace("@", System.Environment.NewLine);
            MessageBoxResult result = MessageBox.Show(text, "End", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Menu newGame = new Menu();
                newGame.Show();
                App.Current.Windows[0].Close();
            }
            else if (result == MessageBoxResult.No)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }
        public void PlayerFightLost(Player player)
        {
            dice.Roll();
            battleProcessor.PunishPlayer(player, dice);
        }
        public void NextPlayer()
        {
            if (PlayersCount == 1)
            {
                return;
            }
            else if (CurrentPlayerId < PlayersCount-1)
            {
                CurrentPlayerId += 1;
            }
            else if (CurrentPlayerId == PlayersCount-1)
            {
                CurrentPlayerId = 0;
            }    
        }
        public void Step(Player player)
        {
            
            if (player.FieldNumber + dice.Result > 99)
            {
                player.FieldNumber = 99;
            }
            else
            {
                player.FieldNumber += dice.Result;
            }
        }
        public void ExitGame()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Exit", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }
        public void ShowScores()
        {
            Scores newScores = new Scores(this);
            newScores.Show();
        }
        public void SaveScores()
        {
            IEnumerable<Score> killQ = from kill in CurrentScores orderby kill.Level descending select kill; //megölt monsterek alapján csökkenő
            IEnumerable<Score> levelQ = from hlvl in CurrentScores where hlvl.iLvl >= 10 orderby hlvl ascending select hlvl;//kik azok akik eljutottak lvl10ig
            IEnumerable<Score> fieldQ = from names in CurrentScores orderby names.Field descending select names; //jelenlegi field alapján csökkenő
            List<string> output = new List<string>();
            output.Add("Most kills");
            foreach (Score player in killQ) output.Add(player.Name + " killed " + player.Level + " monsters");
            output.Add("Above lvl 10");
            foreach (Score player in levelQ) output.Add(player.Name + " achieved lvl " + player.Level);
            output.Add("Best fields");
            foreach (Score player in fieldQ) output.Add(player.Name + " reached  the" + player.Level + ". field");
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Scores.txt"))) foreach (string entry in output) outputFile.WriteLine(entry);
        }
        #endregion
        #region Setup
        public void SetupFieldImages()
        {
            for (int i = 0; i < fields.Count; i++)
            {
                FieldsSource.Add(i, fields[i].ImageSource);
            }
        }
        public void CreateScores()
        {
            CurrentScores.Add(new Model.Score() { Name = "Name", Level = "Level", Field = "Current Field", iLvl = 0});
            CurrentScores.Add(new Model.Score() { Name = "-", Level = "-", Field = "-", iLvl = 0});
            for (int i = 0; i< players.Count; i++)
            {
                CurrentScores.Add(new Model.Score() { Name = players[i].Name, Level = players[i].Level.ToString(), Field = players[i].FieldNumber.ToString(), iLvl = players[i].Level });
            }
        }
        public void UpdateScores()
        {
            for (int i = 0; i < players.Count; i++)
            {
                CurrentScores[i+2].Name = players[i].Name;
                CurrentScores[i+2].Level = players[i].Level.ToString();
                CurrentScores[i+2].Field = players[i].FieldNumber.ToString();
                CurrentScores[i + 2].iLvl = players[i].Level;
            }
        }
        public void UpdateInfo()
        {
            CurrentPlayerName = players[CurrentPlayerId].Name;
            CurrentPlayerAttack = "Attack: " + players[CurrentPlayerId].Attack;
            CurrentPlayerHp = "" + players[CurrentPlayerId].Hp;
            CurrentPlayerMaxHp = "" + players[CurrentPlayerId].MaxHp;
            CurrentPlayerFieldNumber = "Field: " + (players[CurrentPlayerId].FieldNumber+1);
            CurrentMonsterName = fields[players[CurrentPlayerId].FieldNumber].GetMonsterName();
            CurrentMonsterAttack = "Attack: " + fields[players[CurrentPlayerId].FieldNumber].GetMonsterAttack();
            CurrentMonsterHp = "" + fields[players[CurrentPlayerId].FieldNumber].GetMonsterHp();
            CurrentMonsterMaxHp = "" + fields[players[CurrentPlayerId].FieldNumber].GetMonsterMaxHp();
            if (players[CurrentPlayerId].TurnPhase == 0) CurrentPhase = "Roll for Step";
            else CurrentPhase = "Roll for Attack";
        }
        #endregion
    }
}
