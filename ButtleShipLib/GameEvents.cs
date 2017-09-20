using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtleShipLib
{
    public partial class Game
    {
        //маршритизируем события игроков
        #region RoutedEventGameover

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

        #region RoutedEventDieShip

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

        #region RoutedEventHitDeck

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

        #region RoutedEventNotHitDeck

        public event EventHandler<PlayerParamEventArgs> NotHitDeckEvent;

        protected virtual void OnNotHitDeck(PlayerParamEventArgs e)
        {
            NotHitDeckEvent?.Invoke(this, e);
        }

        void Player_NotHit(object sender, PlayerParamEventArgs e)
        {
            OnNotHitDeck(e);
        }

        #endregion

        #region EventOrderOfPriority

        public event EventHandler ChangedOrderOfPriorityEvent;

        protected virtual void OnChangedOrderOfPriority()
        {
            ChangedOrderOfPriorityEvent?.Invoke(OrderOfPriority, new EventArgs());
        }

        //void Game_ChangedOrderOfPriority(object sender, EventArgs e)
        //{
        //    if(OrderOfPriority==PlayerOne && IsOnePlayerAsAI)
        //    {
        //        Course()
        //    }
        //}
        #endregion
    }
}
