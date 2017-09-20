using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ButtleShipLib
{
    public class Game
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
           

            #region Subscription on event Players

            PlayerOne.PlayerGameOverEvent += Player_GameOver;
            PlayerTwo.PlayerGameOverEvent += Player_GameOver;
            PlayerOne.DieShipAtPlayerEvent += Player_DieShip;
            PlayerTwo.DieShipAtPlayerEvent += Player_DieShip;
            PlayerOne.HitDeckAtPlayerEvent += Player_HitDeck;
            PlayerTwo.HitDeckAtPlayerEvent += Player_HitDeck;

            #endregion

            _orderOfPriority = true;
        }

        #endregion

        #region PropertyAndVAr

        public Player PlayerOne { get; }
        public Player PlayerTwo { get; }

        //очерёдность. true ходит первый игрок.
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

        public bool Course(int row, int col)
        {
            // если true, то попали по игроку
            bool result;

            if(_orderOfPriority)
            {
                result = PlayerTwo.AttackField(row, col);
            }
            else
            {
                result = PlayerOne.AttackField(row, col);
            }

            //если подбил, то не мением игрока
            if(!result)
            _orderOfPriority = !_orderOfPriority;

            return result;
        }

        public void Restart()
        {
            PlayerOne.AutoRankingShips();
            PlayerTwo.AutoRankingShips();
            _orderOfPriority = true;
        }

        #endregion

        #region Events

        //маршритизируем событие с игроков
        #region MapEventGameover

        public event EventHandler<PlayerParamEventArgs> GameOverEvent;

        protected virtual void OnGameOver(PlayerParamEventArgs e)
        {
            GameOverEvent?.Invoke(this, e);
        }

        void Player_GameOver(object sender, PlayerParamEventArgs e)
        {
            //если любой игрок проигрывает, то вызывает своим событием событие в Game
            OnGameOver(e);
        }

        #endregion

        #region MapEventDieShip

        public event EventHandler<PlayerParamEventArgs> DieShipEvent;

        protected virtual void OnDieShip(PlayerParamEventArgs e)
        {
            DieShipEvent?.Invoke(this, e);
        }

        void Player_DieShip(object sender, PlayerParamEventArgs e)
        {
            //если у любого игрока умирает корабль, то вызывает своим событием событие в Game
            OnDieShip(e);
        }

        #endregion

        #region MapEventDieShip

        public event EventHandler<PlayerParamEventArgs> HitDeckEvent;

        protected virtual void OnHitDeck(PlayerParamEventArgs e)
        {
            HitDeckEvent?.Invoke(this, e);
        }

        void Player_HitDeck(object sender, PlayerParamEventArgs e)
        {
            //если у любого игрока умирает корабль, то вызывает своим событием событие в Game
            OnHitDeck(e);
        }

        #endregion

        #endregion
    }
}
