using System;
using TournamentGame.Assist;

namespace TournamentGame.ViewModel
{
    public class RollingDice : PropertyAssistant
    {
        private int _sides;
        public int Sides
        {
            get => _sides;
            set => SetProperty(ref _sides, value);
        }
        private int _result;
        public int Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }
        private string _viewPath;
        public string ViewPath
        {
            get => _viewPath;
            set => SetProperty(ref _viewPath, value);
        }
        public RollingDice()
        {
            Sides = 6;
            Result = 0;
            SetViewPath();
        }
        public void Roll()
        {
            Random number = new Random();
            Result = number.Next(1, Sides + 1); // véletlen szerű szám 1 és oldalak száma között
            SetViewPath();
        }
        public void Default()
        {
            Result = 0;
            SetViewPath();
        }
        private void SetViewPath()
        {
            switch (Result)
            {
                case 0:
                    ViewPath = "/Assets/DiceIcon/rolling-dices.png";
                    break;
                case 1:
                    ViewPath = "/Assets/DiceIcon/dice-six-faces-one.png";
                    break;
                case 2:
                    ViewPath = "/Assets/DiceIcon/dice-six-faces-two.png";
                    break;
                case 3:
                    ViewPath = "/Assets/DiceIcon/dice-six-faces-three.png";
                    break;
                case 4:
                    ViewPath = "/Assets/DiceIcon/dice-six-faces-four.png";
                    break;
                case 5:
                    ViewPath = "/Assets/DiceIcon/dice-six-faces-five.png";
                    break;
                case 6:
                    ViewPath = "/Assets/DiceIcon/dice-six-faces-six.png";
                    break;
            }
        }
    }
}
