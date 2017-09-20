using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ButtleShipLib
{
    public enum StatusCell { Unknow, Sea, HitDeck, DieDeck }
    public class AIButtleShip
    {
        StatusCell[,] field { get; set; }
        //убитые карабли
        List<Ship> enemyDieShips;
        //неизвестные ячейки
        List<CellField> unknowCell;
        //гипотетическое расположение палуб
        List<CellField> hypoCellWhisDeck;
        //текущие убитые палубы
        List<CellField> currentHitDeck;
        //есть ли подбитые карабли
        bool isHitDeck;

        public AIButtleShip()
        {
            field = new StatusCell[10, 10];
            unknowCell = new List<CellField>(100);
            for(int i=0;i<10;i++)
                for(int j=0;j<10;j++)
                {
                    field[i, j] = StatusCell.Unknow;
                    unknowCell.Add(new CellField(i,j));
                }
            enemyDieShips = new List<Ship>();
            hypoCellWhisDeck = new List<CellField>();
            currentHitDeck = new List<CellField>();
        }

        
        public CellField Course()
        {
            Random rand = new Random();
            CellField selectCell;

            #region RandomSearch

            bool check = true;
            do
            {
                if (isHitDeck)
                {
                    
                    selectCell = hypoCellWhisDeck[rand.Next(0, hypoCellWhisDeck.Count)];
                    hypoCellWhisDeck.Remove(selectCell);
                }
                else
                {
                    isHitDeck = false;
                    selectCell = unknowCell[rand.Next(0,unknowCell.Count)];
                    unknowCell.Remove(selectCell);
                }
                switch (field[selectCell.Row, selectCell.Col])
                {
                    case StatusCell.Unknow:
                        //проверяем не стоит ли рядом убитые палубы
                        if((selectCell.Row==0 || field[selectCell.Row-1,selectCell.Col]!=StatusCell.DieDeck)&& (selectCell.Row == 9 ||  field[selectCell.Row + 1, selectCell.Col] != StatusCell.DieDeck)&& (selectCell.Col == 0 || field[selectCell.Row, selectCell.Col-1] != StatusCell.DieDeck) && (selectCell.Col == 9 || field[selectCell.Row, selectCell.Col+1] != StatusCell.DieDeck)&&(selectCell.Row==0 || selectCell.Col==0 || field[selectCell.Row-1, selectCell.Col - 1] != StatusCell.DieDeck) && (selectCell.Row == 9 || selectCell.Col == 0 || field[selectCell.Row + 1, selectCell.Col - 1] != StatusCell.DieDeck) && (selectCell.Row == 9 || selectCell.Col == 9 || field[selectCell.Row + 1, selectCell.Col + 1] != StatusCell.DieDeck) && (selectCell.Row == 0 || selectCell.Col == 9 || field[selectCell.Row - 1, selectCell.Col + 1] != StatusCell.DieDeck))
                        {
                            check = false;
                        }
                        else
                        {
                            //если они рядом стоят, то значит это море)
                            field[selectCell.Row, selectCell.Col] = StatusCell.Sea;
                        }
                        break;
                    case StatusCell.Sea:
                      //  isHitDeck = false;
                        break;
                    case StatusCell.HitDeck:
                        break;
                    case StatusCell.DieDeck:
                        break;
                }

            } while (check);

            #endregion

            return selectCell;
        }

        public void ResultCourse(CellField cell, StatusCell status)
        {
            field[cell.Row, cell.Col] = status;
            if (status == StatusCell.HitDeck)
            {
                #region ifHitDeck
                isHitDeck = true;
                currentHitDeck.Add(cell);
                //если одна одна ячейка подбита, то 4 точки предположительно являютя следющими палубами

                if (currentHitDeck.Count == 1)
                {
                    hypoCellWhisDeck.Clear();
                    #region ifOneHitDeck
                    //если ячейка не находится в последней колонке
                    if (cell.Col < 9)
                    {
                        hypoCellWhisDeck.Add(new CellField(cell.Row, cell.Col + 1));
                    }
                    //если ячейка не находится в первой колонке
                    if (cell.Col > 0)
                    {
                        hypoCellWhisDeck.Add(new CellField(cell.Row, cell.Col - 1));
                    }
                    //если ячейка не находится в последней строке
                    if (cell.Row < 9)
                    {
                        hypoCellWhisDeck.Add(new CellField(cell.Row + 1, cell.Col));
                    }
                    //если ячейка не находится в первой строке
                    if (cell.Row > 0)
                    {
                        hypoCellWhisDeck.Add(new CellField(cell.Row - 1, cell.Col));
                    }
                    #endregion
                }
                else
                {
                    hypoCellWhisDeck.Clear();
                    #region ifMoreOneDeck

                    CellField firstCell;
                    CellField endCell;

                    //проверяет на горизонтальное положение
                    if (currentHitDeck.All(p => p.Row == currentHitDeck[0].Row))
                    {
                        #region HorizontalShip
                        firstCell = currentHitDeck.OrderBy(p=>p.Col).First();
                        endCell = currentHitDeck.OrderByDescending(p=>p.Col).First();
                        //ставим левее гипотетическое положение палубы
                        if (firstCell.Col > 0)
                        {
                            hypoCellWhisDeck.Add(new CellField(firstCell.Row, firstCell.Col - 1));
                        }
                        //ставим правее гипотетическое положение палубы
                        if (endCell.Col < 9)
                        {
                            hypoCellWhisDeck.Add(new CellField(endCell.Row, endCell.Col + 1));
                        }
                        #endregion
                    }
                    else
                    {
                        //переменная для нахождения минимальной ячеки и макс соответственно
                        #region Vertical

                        firstCell = currentHitDeck.OrderBy(p => p.Row).First();
                        endCell = currentHitDeck.OrderByDescending(p => p.Row).First();

                        //ставим выше гипотетическое положение палубы
                        if (firstCell.Row > 0)
                        {
                            hypoCellWhisDeck.Add(new CellField(firstCell.Row - 1, firstCell.Col));
                        }
                        //ставим ниже гипотетическое положение палубы
                        if (endCell.Row < 9)
                        {
                            hypoCellWhisDeck.Add(new CellField(endCell.Row + 1, endCell.Col));
                        }
                        #endregion

                        #region old
                        ////ставим значение море левее карабля
                        //if (randomCell.Col > 0)
                        //{
                        //    foreach (CellField c in currentHitDeck)
                        //    {
                        //        field[c.Row , c.Col-1] = StatusCell.Sea;
                        //    }
                        //}
                        ////ставим значение море правее карабля
                        //if (randomCell.Col < 9)
                        //{
                        //    foreach (CellField c in currentHitDeck)
                        //    {
                        //        field[c.Row, c.Col + 1] = StatusCell.Sea;
                        //    }
                        //}
                        #endregion
                    }

                    #endregion

                }

                #endregion
            }
            else
            if (status == StatusCell.DieDeck)
            {
                #region ifDieDeck
                isHitDeck = false;
                //добавление нового убитого карабля
                #region AddingNewDieShip
                List<CellField> tempList = new List<CellField>();
                foreach (CellField c in currentHitDeck)
                {
                    tempList.Add(c);
                }
                enemyDieShips.Add(new Ship(tempList.Count, tempList));
                #endregion
                //нужно отбраковать ячейки рядом с кораблями, так как там не должно быть караблей
                ////удаление из листа с неизветстными ячейками, ячейки убитого карабля
                //foreach(CellField c in currentHitDeck)
                //{
                //    unknowCell.Remove(c);
                //}
                currentHitDeck.Clear();
                hypoCellWhisDeck.Clear();
                #endregion
            }
            
        }
    }
}
