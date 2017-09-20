using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;

namespace ButtleShipLib
{
    /// <summary>
    /// Класс игрока.
    /// </summary>
    public class Player
    {
        public string Name { get; set; }

        internal AIButtleShip AIAttack { get; set; }

        public List<Ship> ships { get; }
        
        public Player(string name)
        {
            Name = name;
            ships = new List<Ship>();
            AutoRankingShips();
        }
        //Методы
        #region Methods
        /// <summary>
        /// Автоматическая расстановка караблей у игрока.
        /// </summary>
        /// <returns></returns>
        public bool AutoRankingShips()
        {
            ships.Clear();
            int[,] field = new int[12, 12];
            Random rand = new Random();
            int x, y;
            //есди все false, то все корабли расставлены
            #region checkRankShip
            bool exitRank4ship = true;
            bool exitRank3ship = true;
            bool exitRank2ship = true;
            bool exitRank1ship = true;
            #endregion

            while (exitRank4ship || exitRank3ship || exitRank2ship || exitRank1ship)
            {
                //расстановка 4-х палубного
                #region rank4deck

                for (int i = 0; i < 10 && exitRank4ship; i++)
                {
                    x = rand.Next(1, 11);
                    y = rand.Next(1, 11);

                    //расстановка первой палубы
                    if ((field[x, y] == 0) && (field[x - 1, y] == 0) && (field[x + 1, y] == 0) && (field[x + 1, y + 1] == 0) && (field[x + 1, y - 1] == 0)
                        && (field[x - 1, y - 1] == 0) && (field[x - 1, y + 1] == 0) && (field[x, y - 1] == 0) && (field[x, y + 1] == 0))
                    {
                        //расстановка последующих палуб
                        #region rank321deck

                        for (int j = 0; j < 10 && exitRank4ship; j++)
                        {


                            switch (rand.Next(1, 5))
                            {
                                case 1:
                                    //расстановка вниз
                                    #region rankDown4deck
                                    if (((y + 2) < 12) && (field[x + 1, y + 2] == 0) && (field[x, y + 2] == 0) && (field[x - 1, y + 2] == 0))
                                    {
                                        if (((y + 3) < 12) && (field[x + 1, y + 3] == 0) && (field[x, y + 3] == 0) && (field[x - 1, y + 3] == 0))
                                        {
                                            if (((y + 4) < 12) && (field[x + 1, y + 4] == 0) && (field[x, y + 4] == 0) && (field[x - 1, y + 4] == 0))
                                            {

                                                field[x, y] = 1;
                                                field[x, y + 1] = 1;
                                                field[x, y + 2] = 1;
                                                field[x, y + 3] = 1;
                                                ships.Add(new Ship(4, new List<CellField> { new CellField(x, y), new CellField(x, y + 1), new CellField(x, y + 2), new CellField(x, y + 3) }));
                                                exitRank4ship = false;
                                            }
                                        }
                                    }
                                    #endregion
                                    break;

                                case 2:
                                    //расстановка вверх
                                    #region rankTop4deck
                                    if (((y - 2) >= 0) && (field[x + 1, y - 2] == 0) && (field[x, y - 2] == 0) && (field[x - 1, y - 2] == 0))
                                    {
                                        if (((y - 3) >= 0) && (field[x + 1, y - 3] == 0) && (field[x, y - 3] == 0) && (field[x - 1, y - 3] == 0))
                                        {
                                            if (((y - 4) >= 0) && (field[x + 1, y - 4] == 0) && (field[x, y - 4] == 0) && (field[x - 1, y - 4] == 0))
                                            {
                                                field[x, y] = 1;
                                                field[x, y - 1] = 1;
                                                field[x, y - 2] = 1;
                                                field[x, y - 3] = 1;
                                                ships.Add(new Ship(4, new List<CellField> { new CellField(x, y), new CellField(x, y - 1), new CellField(x, y - 2), new CellField(x, y - 3) }));
                                                exitRank4ship = false;
                                            }
                                        }
                                    }
                                    #endregion
                                    break;

                                case 3:
                                    //расстановка вправо
                                    #region rankRight4deck
                                    if (((x + 2) < 12) && (field[x + 2, y - 1] == 0) && (field[x + 2, y] == 0) && (field[x + 2, y + 1] == 0))
                                    {
                                        if (((x + 3) < 12) && (field[x + 3, y - 1] == 0) && (field[x + 3, y + 1] == 0) && (field[x + 3, y] == 0))
                                        {
                                            if (((x + 4) < 12) && (field[x + 4, y - 1] == 0) && (field[x + 4, y] == 0) && (field[x + 4, y - 1] == 0))
                                            {
                                                field[x, y] = 1;
                                                field[x + 1, y] = 1;
                                                field[x + 2, y] = 1;
                                                field[x + 3, y] = 1;
                                                ships.Add(new Ship(4, new List<CellField> { new CellField(x, y), new CellField(x + 1, y), new CellField(x + 2, y), new CellField(x + 3, y) }));
                                                exitRank4ship = false;
                                            }
                                        }
                                    }
                                    #endregion
                                    break;
                                case 4:
                                    //расстановка влево
                                    #region rankLeft4deck
                                    if (((x - 2) >= 0) && (field[x - 2, y - 1] == 0) && (field[x - 2, y] == 0) && (field[x - 2, y + 1] == 0))
                                    {
                                        if (((x - 3) >= 0) && (field[x - 3, y - 1] == 0) && (field[x - 3, y + 1] == 0) && (field[x - 3, y] == 0))
                                        {
                                            if (((x - 4) >= 0) && (field[x - 4, y - 1] == 0) && (field[x - 4, y] == 0) && (field[x - 4, y + 1] == 0))
                                            {
                                                field[x, y] = 1;
                                                field[x - 1, y] = 1;
                                                field[x - 2, y] = 1;
                                                field[x - 3, y] = 1;
                                                ships.Add(new Ship(4, new List<CellField> { new CellField(x, y), new CellField(x - 1, y), new CellField(x - 2, y), new CellField(x - 3, y) }));
                                                exitRank4ship = false;
                                            }
                                        }
                                    }
                                    #endregion
                                    break;
                            }
                        }
                        #endregion
                    }


                }
                #endregion

                //расстановка двух 3-х палубных
                #region rank3deck

                for (int i = 0; i < 10 && exitRank3ship; i++)
                {
                    for (int col = 0; col < 2;)
                    {
                        exitRank3ship = true;

                        x = rand.Next(1, 11);
                        y = rand.Next(1, 11);

                        //расстановка первой палубы
                        if ((field[x, y] == 0) && (field[x - 1, y] == 0) && (field[x + 1, y] == 0) && (field[x + 1, y + 1] == 0) && (field[x + 1, y - 1] == 0)
                            && (field[x - 1, y - 1] == 0) && (field[x - 1, y + 1] == 0) && (field[x, y - 1] == 0) && (field[x, y + 1] == 0))
                        {
                            //расстановка последующих палуб
                            #region rank21deck

                            for (int j = 0; j < 10 && exitRank3ship; j++)
                            {


                                switch (rand.Next(1, 5))
                                {
                                    case 1:
                                        //расстановка вниз
                                        #region rankDown3deck
                                        if (((y + 2) < 12) && (field[x + 1, y + 2] == 0) && (field[x, y + 2] == 0) && (field[x - 1, y + 2] == 0))
                                        {
                                            if (((y + 3) < 12) && (field[x + 1, y + 3] == 0) && (field[x, y + 3] == 0) && (field[x - 1, y + 3] == 0))
                                            {

                                                field[x, y] = 1;
                                                field[x, y + 1] = 1;
                                                field[x, y + 2] = 1;
                                                ships.Add(new Ship(3, new List<CellField> { new CellField(x, y), new CellField(x, y + 1), new CellField(x, y + 2) }));
                                                exitRank3ship = false;
                                                col++;

                                            }
                                        }
                                        #endregion
                                        break;

                                    case 2:
                                        //расстановка вверх
                                        #region rankTop3deck
                                        if (((y - 2) >= 0) && (field[x + 1, y - 2] == 0) && (field[x, y - 2] == 0) && (field[x - 1, y - 2] == 0))
                                        {
                                            if (((y - 3) >= 0) && (field[x + 1, y - 3] == 0) && (field[x, y - 3] == 0) && (field[x - 1, y - 3] == 0))
                                            {
                                                field[x, y] = 1;
                                                field[x, y - 1] = 1;
                                                field[x, y - 2] = 1;
                                                ships.Add(new Ship(3, new List<CellField> { new CellField(x, y), new CellField(x, y - 1), new CellField(x, y - 2) }));
                                                exitRank3ship = false;
                                                col++;

                                            }
                                        }
                                        #endregion
                                        break;

                                    case 3:
                                        //расстановка вправо
                                        #region rankRight3deck
                                        if (((x + 2) < 12) && (field[x + 2, y - 1] == 0) && (field[x + 2, y] == 0) && (field[x + 2, y + 1] == 0))
                                        {
                                            if (((x + 3) < 12) && (field[x + 3, y - 1] == 0) && (field[x + 3, y + 1] == 0) && (field[x + 3, y] == 0))
                                            {

                                                field[x, y] = 1;
                                                field[x + 1, y] = 1;
                                                field[x + 2, y] = 1;
                                                ships.Add(new Ship(3, new List<CellField> { new CellField(x, y), new CellField(x + 1, y), new CellField(x + 2, y) }));
                                                exitRank3ship = false;
                                                col++;

                                            }
                                        }
                                        #endregion
                                        break;
                                    case 4:
                                        //расстановка влево
                                        #region rankLeft3deck
                                        if (((x - 2) >= 0) && (field[x - 2, y - 1] == 0) && (field[x - 2, y] == 0) && (field[x - 2, y + 1] == 0))
                                        {
                                            if (((x - 3) >= 0) && (field[x - 3, y - 1] == 0) && (field[x - 3, y + 1] == 0) && (field[x - 3, y] == 0))
                                            {

                                                field[x, y] = 1;
                                                field[x - 1, y] = 1;
                                                field[x - 2, y] = 1;
                                                ships.Add(new Ship(3, new List<CellField> { new CellField(x, y), new CellField(x - 1, y), new CellField(x - 2, y) }));
                                                exitRank3ship = false;
                                                col++;
                                            }
                                        }
                                        #endregion
                                        break;
                                }
                            }
                            #endregion
                        }

                    }
                }
                #endregion

                //расстановка трёх 2-х палубных
                #region rank2deck

                for (int i = 0; i < 10 && exitRank2ship; i++)
                {
                    for (int col = 0; col < 3;)
                    {
                        exitRank2ship = true;

                        x = rand.Next(1, 11);
                        y = rand.Next(1, 11);

                        //расстановка первой палубы
                        if ((field[x, y] == 0) && (field[x - 1, y] == 0) && (field[x + 1, y] == 0) && (field[x + 1, y + 1] == 0) && (field[x + 1, y - 1] == 0)
                            && (field[x - 1, y - 1] == 0) && (field[x - 1, y + 1] == 0) && (field[x, y - 1] == 0) && (field[x, y + 1] == 0))
                        {
                            //расстановка последующих палуб
                            #region rank1deck

                            for (int j = 0; j < 10 && exitRank2ship; j++)
                            {


                                switch (rand.Next(1, 5))
                                {
                                    case 1:
                                        //расстановка вниз
                                        #region rankDown2deck
                                        if (((y + 2) < 12) && (field[x + 1, y + 2] == 0) && (field[x, y + 2] == 0) && (field[x - 1, y + 2] == 0))
                                        {
                                            field[x, y] = 1;
                                            field[x, y + 1] = 1;
                                            ships.Add(new Ship(2, new List<CellField> { new CellField(x, y), new CellField(x, y + 1) }));
                                            exitRank2ship = false;
                                            col++;
                                        }
                                        #endregion
                                        break;

                                    case 2:
                                        //расстановка вверх
                                        #region rankTop2deck
                                        if (((y - 2) >= 0) && (field[x + 1, y - 2] == 0) && (field[x, y - 2] == 0) && (field[x - 1, y - 2] == 0))
                                        {
                                            field[x, y] = 1;
                                            field[x, y - 1] = 1;
                                            ships.Add(new Ship(2, new List<CellField> { new CellField(x, y), new CellField(x, y - 1) }));
                                            exitRank2ship = false;
                                            col++;
                                        }
                                        #endregion
                                        break;

                                    case 3:
                                        //расстановка вправо
                                        #region rankRight2deck
                                        if (((x + 2) < 12) && (field[x + 2, y - 1] == 0) && (field[x + 2, y] == 0) && (field[x + 2, y + 1] == 0))
                                        {
                                            field[x, y] = 1;
                                            field[x + 1, y] = 1;
                                            ships.Add(new Ship(2, new List<CellField> { new CellField(x, y), new CellField(x + 1, y) }));
                                            exitRank2ship = false;
                                            col++;
                                        }
                                        #endregion
                                        break;
                                    case 4:
                                        //расстановка влево
                                        #region rankLeft2deck
                                        if (((x - 2) >= 0) && (field[x - 2, y - 1] == 0) && (field[x - 2, y] == 0) && (field[x - 2, y + 1] == 0))
                                        {
                                            field[x, y] = 1;
                                            field[x - 1, y] = 1;
                                            ships.Add(new Ship(2, new List<CellField> { new CellField(x, y), new CellField(x - 1, y) }));
                                            exitRank2ship = false;
                                            col++;
                                        }
                                        #endregion
                                        break;
                                }
                            }
                            #endregion
                        }

                    }
                }
                #endregion

                //расстановка четырёх 1 палубных
                #region rank1deck

                for (int i = 0; i < 10 && exitRank1ship; i++)
                {
                    for (int col = 0; col < 4;)
                    {
                        exitRank1ship = true;

                        x = rand.Next(1, 11);
                        y = rand.Next(1, 11);

                        //расстановка первой палубы
                        if ((field[x, y] == 0) && (field[x - 1, y] == 0) && (field[x + 1, y] == 0) && (field[x + 1, y + 1] == 0) && (field[x + 1, y - 1] == 0)
                            && (field[x - 1, y - 1] == 0) && (field[x - 1, y + 1] == 0) && (field[x, y - 1] == 0) && (field[x, y + 1] == 0))
                        {
                            field[x, y] = 1;
                            ships.Add(new Ship(1, new List<CellField> { new CellField(x, y) }));
                            exitRank1ship = false;
                            col++;
                        }

                    }
                }
                #endregion

            }

            //изменение отсчёта с 1 на 0
            #region UpdateWhisOneToZero

            foreach (Ship s in ships)
            {
                List<CellField> tempList = new List<CellField>();
                foreach (CellField c in s.DeckCoordinate)
                {
                    tempList.Add(new CellField(c.Row - 1, c.Col - 1));
                }
                s.DeckCoordinate.Clear();
                foreach (CellField c in tempList)
                {
                    s.DeckCoordinate.Add(c);
                }
            }

            #endregion

            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public bool AttackField(CellField cell)
        {
            Ship hitShip = ships.FirstOrDefault(p => p.ContaintDeck(cell.Row, cell.Col));
            if (hitShip!=null)
            {
                hitShip.HitDeck(cell.Row, cell.Col);

                #region InvokeEvents

                //check Player if game over then invoke Player 
                if (ships.All(p => p.Status == ShipStatus.Die))
                {
                    OnPlayerGameOver(new PlayerParamEventArgs(this, cell, hitShip));
                    return true;
                }

                //check Player if ship die then invoke Player 
                if (hitShip.Status == ShipStatus.Die)
                {
                    OnDieShipAtPlayer(new PlayerParamEventArgs(this, cell, hitShip));
                    return true;
                }

                //check Player if hit ship then invoke Player 
                OnHitDeckAtPlayer(new PlayerParamEventArgs(this, cell, hitShip));
                return true;

                #endregion

            }
            OnNotHitAtPlayer(new PlayerParamEventArgs(this,cell,null));
            return false;
        }


        #endregion
        //События
        #region Events

        //event for Player Game Over
        #region eventGameOver
        /// <summary>
        /// Событие срабатывает когда все караби убиты.
        /// </summary>
        public event EventHandler<PlayerParamEventArgs> PlayerGameOverEvent;
        protected virtual void OnPlayerGameOver(PlayerParamEventArgs e)
        {
            PlayerGameOverEvent?.Invoke(this, e);
        }

        #endregion

        //event for hit deck at Player
        #region eventHitDeck
        /// <summary>
        /// Событие сробатывает когда у игрока подбивают карабль.
        /// </summary>
        public event EventHandler<PlayerParamEventArgs> HitDeckAtPlayerEvent;

        protected virtual void OnHitDeckAtPlayer(PlayerParamEventArgs e)
        {
            HitDeckAtPlayerEvent?.Invoke(this, e);
        }

        #endregion

        //event for not hit deck at Player
        #region eventNotHit
        /// <summary>
        /// Событие срабатывает когда в игрока не попадают
        /// </summary>
        public event EventHandler<PlayerParamEventArgs> NotHitAtPlayerEvent;

        protected virtual void OnNotHitAtPlayer(PlayerParamEventArgs e)
        {
            NotHitAtPlayerEvent?.Invoke(this, e);
        }

        #endregion

        //event for die ship at Player
        #region eventDieShip
        /// <summary>
        /// Событие сробатывает когда у игрока убивают карабль.
        /// </summary>
        public event EventHandler<PlayerParamEventArgs> DieShipAtPlayerEvent;

        protected virtual void OnDieShipAtPlayer(PlayerParamEventArgs e)
        {
            DieShipAtPlayerEvent?.Invoke(this, e);
        }

        #endregion

        #endregion
   
    }

    //for eventArgs
    public class PlayerParamEventArgs:EventArgs
    {
        public readonly Player Player;

        public readonly CellField HitDeck;

        public readonly Ship DieShip;

        public PlayerParamEventArgs(Player player, CellField hitDeck, Ship dieShip)
        {
            Player = player;
            HitDeck = hitDeck;
            DieShip = dieShip;
        }
    }
}
