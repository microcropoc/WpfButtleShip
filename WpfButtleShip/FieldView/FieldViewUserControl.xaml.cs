using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ButtleShipLib;

namespace WpfButtleShip.FieldView
{
    /// <summary>
    /// Interaction logic for FieldViewUserControl.xaml
    /// </summary>
    /// 
    public enum StateBtnCellField { Unknow, Sea, LiveDeck, HitDeck, DieDeck, NotHit}
    class BtnCellField:Button
    {
        public StateBtnCellField State
        {
            get
            {
                return _state;
            }
            set
            {
                switch(value)
                {
                    case StateBtnCellField.Unknow:
                        ViewUnknowState();
                        break;

                    case StateBtnCellField.Sea:
                        ViewSeaState();
                        break;

                    case StateBtnCellField.LiveDeck:
                        ViewLiveDeckState();
                        break;

                    case StateBtnCellField.HitDeck:
                        ViewHitDeckState();
                        break;

                    case StateBtnCellField.DieDeck:
                        ViewDieDeckState();
                        break;
                    case StateBtnCellField.NotHit:
                        ViewNotHitState();
                        break;
                }
                _state = value;
            }
           
        }

        StateBtnCellField _state;
        public BtnCellField(StateBtnCellField state)
        {
            State = state;
        }

        #region ViewState

        void ViewUnknowState()
        {
            Background = Brushes.Pink;
            Content = "?";
        }

        void ViewSeaState()
        {
            Background = Brushes.Blue;
            Content = "~";
        }

        void ViewLiveDeckState()
        {
            Background = Brushes.Green;
            Content = "=";
        }

        void ViewHitDeckState()
        {
            Background = Brushes.Red;
            Content = "*";
        }

        void ViewDieDeckState()
        {
            Background = Brushes.Black;
            Content = "X";
        }

        void ViewNotHitState()
        {
            Background = Brushes.Brown;
            Content = "~";
        }

        #endregion

    }

    public partial class FieldViewUserControl : UserControl
    {        

        BtnCellField[,] FieldCells;
        public List<Ship> ships { get; private set; }
        Game _gameModel = Game.Instanse;
        bool UserField { get; }
        Player _currentPlayer { get; }
        //ai
        bool IsAIAttack;

        AIButtleShip AIFieldAttack;


        public FieldViewUserControl(bool userField,bool isAIFieldAttack,Player player)
        {
            InitializeComponent();
            UserField = userField;
            _currentPlayer = player;
            ships = player.ships;
            //подключение AI для атаки на это поле во время хода противника
            IsAIAttack = isAIFieldAttack;
            if(IsAIAttack)
            {
                AIFieldAttack = new AIButtleShip();
            }

            #region Proverky

            if (ships.Count != 10)
            {
                throw new Exception("Не верное колличество караблей");
            }

            if (ships.Where(p => p.CountDeck == 1).Count() != 4)
            {
                throw new Exception("Не верное колличество 1 палубных кораблей");
            }

            #endregion

            #region initFieldCells

            FieldCells = new BtnCellField[10, 10];
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    BtnCellField b = new BtnCellField(userField ? StateBtnCellField.Sea:StateBtnCellField.Unknow);
                    Grid.SetRow(b, i);
                    Grid.SetColumn(b, j);
                    gridField.Children.Add(b);
                    if (!userField)
                        b.Click += ButtonClick;
                    FieldCells[i, j] = b;
                }
            this.ships = ships;
            if (userField)
            {
                foreach (Ship s in ships)
                {
                    foreach(CellField p in s.DeckCoordinate)
                    {
                        FieldCells[(int)p.Row, (int)p.Col].Background = Brushes.Green;
                    }
                }
            }


            #endregion

            #region Substruction

            _currentPlayer.HitDeckAtPlayerEvent += Player_HitDeck;
            _currentPlayer.DieShipAtPlayerEvent += Player_DieShip;
            _currentPlayer.PlayerGameOverEvent += Player_GameOver;
            _gameModel.ChangedOrderOfPriorityEvent += Game_OrderByPriority;

            #endregion

        }
        //forUserAttack
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            int col = Grid.GetColumn(sender as UIElement);
            int row = Grid.GetRow(sender as UIElement);
            BtnCellField _currentButton = sender as BtnCellField;
            if (!_gameModel.Course(new CellField(row, col)) && FieldCells[row, col].State == StateBtnCellField.Unknow)
            {
                FieldCells[row, col].State = StateBtnCellField.Sea;
                _currentButton.State = StateBtnCellField.Sea;
            }
        }

        public void Player_HitDeck(object sender,PlayerParamEventArgs e)
        {
            FieldCells[e.HitDeck.Row, e.HitDeck.Col].State = StateBtnCellField.HitDeck;
            if(IsAIAttack)
            {
                AIFieldAttack.ResultCourse(e.HitDeck, StatusCell.HitDeck);
            }
        }

        public void Player_DieShip(object sender, PlayerParamEventArgs e)
        {
            foreach(CellField c in e.DieShip.DieDeckCoordinate)
            {
                FieldCells[c.Row, c.Col].State = StateBtnCellField.DieDeck;
                if (IsAIAttack)
                {
                    AIFieldAttack.ResultCourse(e.HitDeck, StatusCell.DieDeck);
                }
            }
        }

        public void Player_GameOver(object sender, PlayerParamEventArgs e)
        {
            //Player_DieShip(sender, e);
            if(IsAIAttack)
            {
                AIFieldAttack = new AIButtleShip();
            }
        }

        public void Redrawing()
        {
            for(int i=0;i<10;i++)
                for(int j=0;j<10;j++)
                {
                    if(UserField)
                    {
                        FieldCells[i, j].State = StateBtnCellField.Sea;
                    }
                    else
                    {
                        FieldCells[i, j].State = StateBtnCellField.Unknow;
                    }
                }
            if(UserField)
            {
                foreach (Ship s in ships)
                {
                    foreach (CellField p in s.DeckCoordinate)
                    {
                        FieldCells[(int)p.Row , (int)p.Col ].Background = Brushes.Green;
                    }
                }
            }
        }

        public void Game_OrderByPriority(object sender, EventArgs e)
        {
            if (!((sender as Player) == _currentPlayer))
            {
                if (IsAIAttack)
                {
                    CellField cellAttack = AIFieldAttack.Course();
                    if (!_gameModel.Course(cellAttack) && FieldCells[cellAttack.Row, cellAttack.Col].State==StateBtnCellField.Sea)
                    {
                        FieldCells[cellAttack.Row, cellAttack.Col].State = StateBtnCellField.NotHit;
                        AIFieldAttack.ResultCourse(cellAttack, StatusCell.Sea);
                    }

                }
            }
        }
    }
}
