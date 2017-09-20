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
        public List<Point> DeckCoordinate { get; }
        public List<Point> DieDeckCoordinate { get; }
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
        public Ship(int countDeck, List<Point> deckCoordinate)
        {
            CountDeck = countDeck;
            DeckCoordinate = deckCoordinate;
            DieDeckCoordinate = new List<Point>();
            if (CountDeck!=DeckCoordinate.Count)
            {
                throw new Exception("Неправильно инициализировался корабль");
            }
        }

        public bool ContaintDeck(int row,int col)
        {
            foreach(Point p in DeckCoordinate)
            {
                if ((int)p.X == row && (int)p.Y == col)
                    return true;
            }

            return false;
        }

        public bool HitDeck(int row, int col)
        {
            foreach (Point p in DeckCoordinate)
            {
                if ((int)p.X == row && (int)p.Y == col)
                {
                    DeckCoordinate.Remove(p);
                    DieDeckCoordinate.Add(p);
                    return true;
                }

            }

            return false;
        }
    }

    //public struct Point : IEquatable<Point>
    //{
    //    private readonly int _X;
    //    private readonly int _Y;

    //    public Point(int x, int y)
    //    {
    //        _X = x;
    //        _Y = y;
    //    }

    //    public int X
    //    {
    //        get { return _X; }
    //    }

    //    public int Y
    //    {
    //        get { return _Y; }
    //    }

    //    public override int GetHashCode()
    //    {
    //        return _X ^ _Y;
    //    }

    //    public override bool Equals(object obj)
    //    {
    //        if (!(obj is Point))
    //            return false;

    //        return Equals((Point)obj);
    //    }

    //    public bool Equals(Point other)
    //    {
    //        if (_X != other._X)
    //            return false;

    //        return _Y == other._Y;
    //    }

    //    public static bool operator ==(Point point1, Point point2)
    //    {
    //        return point1.Equals(point2);
    //    }

    //    public static bool operator !=(Point point1, Point point2)
    //    {
    //        return !point1.Equals(point2);
    //    }
    //}
}
