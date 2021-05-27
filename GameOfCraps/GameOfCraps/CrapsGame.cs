using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace GameOfCraps
{
    public class CrapsGame
    {
        private enum DiceSum
        {
            SnakeEyes = 2,
            Trey = 3,
            Seven = 7,
            YoLeven = 11,
            BoxCars = 12
        };

        public enum GameStatus
        {
            Win,
            Lose,
            Continue
        };

        private RollDice _roll;
        private GameStatus _gameStatus;
        private DiceSum _diceSum;

        private int _numRolls;
        private int _sum;
        private int _point;
        public const int numGames = 50;
        private Statistics _statistics;

       public CrapsGame()
        {
            _roll = new RollDice();
            _statistics = new Statistics();

         }

        private void EvaluateRoll()
        {
            switch (_sum)
            {
                case 7:
                    _diceSum = DiceSum.Seven;
                    _gameStatus = GameStatus.Win;
                    _point = 0;
                    _statistics.SetStats(_numRolls, _gameStatus);
                 break;
                 
                 case 11:
                    _diceSum = DiceSum.YoLeven;
                    _gameStatus = GameStatus.Win;
                    _point = 0;
                    _statistics.SetStats(_numRolls, _gameStatus);
                 break;
                 
                 case 2:
                    _diceSum = DiceSum.SnakeEyes;
                    _gameStatus = GameStatus.Lose;
                    _point = 0;
                    _statistics.SetStats(_numRolls, _gameStatus);
                 break;
                 
                 case 3:
                    _diceSum = DiceSum.Trey;
                    _gameStatus = GameStatus.Lose;
                    _point = 0;
                    _statistics.SetStats(_numRolls, _gameStatus);
                 break;
                 
                 case 12:
                    _diceSum = DiceSum.BoxCars;
                    _gameStatus = GameStatus.Lose;
                    _point = 0;
                    _statistics.SetStats(_numRolls, _gameStatus);
                 break;
                 
                 default:
	 	            _gameStatus = GameStatus.Continue;
                    _point = _sum;
                    break; 
            }
        }
        private void DisplayMessage()
        {
            switch (_gameStatus)
            {
                case GameStatus.Win: 
                    Console.WriteLine(_numRolls == 1 ? 
                        $"Congratulations, you rolled {_diceSum}. YOU WIN!"
                        :$"Congratulations, you rolled {_sum}. YOU WIN!" );
                    break;
                case GameStatus.Lose: 
                    Console.WriteLine(_numRolls == 1 ? 
                        $"Sorry, you rolled {_diceSum}. YOU LOSE"
                        : $"Sorry, you rolled {_sum}. YOU LOSE");
                    break;
                default: 
                    Console.WriteLine($"You rolled {_sum}. Your point is {_point}. Keep rolling!");
                    break;
             }
        }
        public void Play()
        {
            for (int i = 0; i < numGames; i++)
            {
                //Console.WriteLine($"********************************* game # {i + 1}");
                Console.WriteLine("");

                _gameStatus = GameStatus.Continue;
                _numRolls = 0;
                
                _sum = _roll.DiceRoll();
                _numRolls++;
                
                EvaluateRoll();
                //DisplayMessage();

                while (_gameStatus == GameStatus.Continue)
                {
                    KeepPlaying();
                    //DisplayMessage();
                }
            }
            _statistics.DisplayStatistics();
        }
        private void KeepPlaying()
        {
            _sum = _roll.DiceRoll();
            _numRolls++;

            if (_sum == _point)
            {
                _gameStatus = GameStatus.Win;
                _statistics.SetStats(_numRolls, _gameStatus);
            }
            else if (_sum == 7)
            {
                _gameStatus = GameStatus.Lose;
                _statistics.SetStats(_numRolls, _gameStatus);
            }
            else
                _gameStatus = GameStatus.Continue;
        }
    }
}