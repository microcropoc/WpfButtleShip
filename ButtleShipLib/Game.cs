using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ButtleShipLib
{
    /// <summary>
    /// Главный класс игры(Singletone)
    /// </summary>
    public partial class Game
    {

        #region InitSingletone

        public static Game Instanse
        {
            get
            {
                if (_instanse == null)
                {
                    _instanse = new Game();
                }
                return _instanse;
            }
        }

        static Game _instanse;

        private Game()
        {
            PlayerOne = new Player("OnePlayer");
            PlayerTwo = new Player("TwoPlayer");

            //check dublicate field
            #region Check

            if (PlayerOne.ships.SequenceEqual(PlayerTwo.ships))
            {
                do
                {
                    PlayerTwo.AutoRankingShips();
                } while (PlayerOne.ships.SequenceEqual(PlayerTwo.ships));
            }

            #endregion

            #region Subscription on event Players

            PlayerOne.PlayerGameOverEvent += Player_GameOver;
            PlayerTwo.PlayerGameOverEvent += Player_GameOver;
            PlayerOne.DieShipAtPlayerEvent += Player_DieShip;
            PlayerTwo.DieShipAtPlayerEvent += Player_DieShip;
            PlayerOne.HitDeckAtPlayerEvent += Player_HitDeck;
            PlayerTwo.HitDeckAtPlayerEvent += Player_HitDeck;
            PlayerOne.NotHitAtPlayerEvent += Player_NotHit;
            PlayerTwo.NotHitAtPlayerEvent += Player_NotHit;

            #endregion

            OnChangedOrderOfPriority();
            _orderOfPriority = true;
        }

        #endregion

        #region PropertyAndVAr

        public bool IsOnePlayerAsAI
        {
            get
            {
                if(PlayerTwo.AIAttack!=null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsTwoPlayerAsAI
        {
            get
            {
                if (PlayerOne.AIAttack != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Player PlayerOne { get; }
        public Player PlayerTwo { get; }

        /// <summary>
        /// Очерёдность хода. Возвращает игрока чей ход.
        /// </summary>
        public Player OrderOfPriority
        {
            get
            {
                return _orderOfPriority ? PlayerOne : PlayerTwo;
            }
        }

        bool _orderOfPriority;

        #endregion

        #region Methods
        /// <summary>
        /// Ход.
        /// </summary>
        /// <param name="cell">ячейка на поле от 0 до 9 по строке и столбцу</param>
        /// <returns>если true, то ход был удачен</returns>
        public bool Course(CellField cell)
        {
            // если true, то попали по игроку
            bool result;

            if(_orderOfPriority)
            {
                result = PlayerTwo.AttackField(cell);
            }
            else
            {
                result = PlayerOne.AttackField(cell);
            }

            //если подбил, то не мением игрока
            if (!result)
            {
                _orderOfPriority = !_orderOfPriority;
            }
            OnChangedOrderOfPriority();
            return result;
        }


        /// <summary>
        /// Новая игра
        /// </summary>
        public void NewGame()
        {
            PlayerOne.AutoRankingShips();
            PlayerTwo.AutoRankingShips();
            _orderOfPriority = true;

            //check dublicate field
            #region Check

            if (PlayerOne.ships.SequenceEqual(PlayerTwo.ships))
            {
                do
                {
                    PlayerTwo.AutoRankingShips();
                } while (PlayerOne.ships.SequenceEqual(PlayerTwo.ships));
            }

            #endregion


            OnChangedOrderOfPriority();
            _orderOfPriority = true;
        }

        #endregion

    }
}
