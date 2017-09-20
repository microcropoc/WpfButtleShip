using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ButtleShipLib
{
    public enum ShipStatus { Livelong, Hit, Die };

    public class Ship
    {
        public int CountDeck { get; }
        public List<CellField> DeckCoordinate { get; }
        public List<CellField> DieDeckCoordinate { get; }
        public ShipStatus Status
        {
            get
            {
                if(DeckCoordinate.Count!=CountDeck)
                {
                    if(DeckCoordinate.Count==0)
                    {
                        return ShipStatus.Die;
                    }
                    return ShipStatus.Hit;
                }
                return ShipStatus.Livelong;
            }
        }
        public Ship(int countDeck, List<CellField> deckCoordinate)
        {
            CountDeck = countDeck;
            DeckCoordinate = deckCoordinate;
            DieDeckCoordinate = new List<CellField>();
            if (CountDeck!=DeckCoordinate.Count)
            {
                throw new Exception("Неправильно инициализировался корабль");
            }
        }

        public bool ContaintDeck(int row,int col)
        {
            foreach(CellField p in DeckCoordinate)
            {
                if ((int)p.Row == row && (int)p.Col == col)
                    return true;
            }

            return false;
        }

        public bool HitDeck(int row, int col)
        {
            foreach (CellField p in DeckCoordinate)
            {
                if ((int)p.Row == row && (int)p.Col == col)
                {
                    DeckCoordinate.Remove(p);
                    DieDeckCoordinate.Add(p);
                    return true;
                }

            }

            return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Ship p = (Ship)obj;
            return (DeckCoordinate.SequenceEqual(p.DeckCoordinate)) && (DieDeckCoordinate.SequenceEqual(p.DieDeckCoordinate));
        }
        public override int GetHashCode()
        {
            int hashCode = CountDeck;
            foreach (CellField c in DeckCoordinate.Concat(DieDeckCoordinate))
                hashCode = hashCode ^ c.GetHashCode();

            return hashCode;

        }

    //    public static bool operator ==(Ship ship1, Ship ship2)
    //    {
    //        return ship1.Equals(ship2);
    //    }

    //    public static bool operator !=(Ship ship1, Ship ship2)
    //    {

    //        return !ship1.Equals(ship2);
    //    }
    }

    public struct CellField : IEquatable<CellField>
    {
        private readonly int _row;
        private readonly int _col;

        public CellField(int x, int y)
        {
            _row = x;
            _col = y;
        }

        public int Row
        {
            get { return _row; }
        }

        public int Col
        {
            get { return _col; }
        }

        public override int GetHashCode()
        {
            return _row ^ _col;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CellField))
                return false;

            return Equals((CellField)obj);
        }

        public bool Equals(CellField other)
        {
            if (_row != other._row)
                return false;

            return _col == other._col;
        }

        public static bool operator ==(CellField point1, CellField point2)
        {
            return point1.Equals(point2);
        }

        public static bool operator !=(CellField point1, CellField point2)
        {
            return !point1.Equals(point2);
        }
    }
}
