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
using System.Diagnostics;
using ButtleShipLib;
using WpfButtleShip.FieldView;

namespace WpfButtleShip
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FieldViewUserControl _userFieldView;
        FieldViewUserControl _computerFieldView;
        Game _gameModel;


        public MainWindow()
        {
            InitializeComponent();
            _gameModel = Game.Instanse;

            _userFieldView = new FieldViewUserControl(true,true, _gameModel.PlayerOne);
            _computerFieldView = new FieldViewUserControl(false,false, _gameModel.PlayerTwo);
            gridForUserField.Children.Add(_userFieldView);
            gridForComputerField.Children.Add(_computerFieldView);

            //subcription on Game event
            _gameModel.HitDeckEvent += Game_HidDeck;
            _gameModel.DieShipEvent += Game_DieDeck;
            _gameModel.GameOverEvent += Game_GameOver;
            _gameModel.ChangedOrderOfPriorityEvent += Game_OrderOfPriority;
            //костыль
          //  _userFieldView.IsEnabled = false;
        }

        //событие на побдитый корабль
        public void Game_HidDeck(object sender,PlayerParamEventArgs e)
        {
          //  MessageBox.Show(_gameModel.OrderOfPriority.Name+" подбил карабль "+e.Name);
        }

        //событие на убитый карабль
        public void Game_DieDeck(object sender, PlayerParamEventArgs e)
        {
            MessageBox.Show(_gameModel.OrderOfPriority.Name + " убил карабль " + e.Player.Name);
        }

        //событие на конец игры
        public void Game_GameOver(object sender, PlayerParamEventArgs e)
        {
            MessageBox.Show(_gameModel.OrderOfPriority.Name + " победил ");
            _gameModel.NewGame();
            _userFieldView.Redrawing();
            _computerFieldView.Redrawing();
        }

        //событие на смене хода игрока
        public void Game_OrderOfPriority(object sender, EventArgs e)
        {
            //переписать
           
        }
    }
}
