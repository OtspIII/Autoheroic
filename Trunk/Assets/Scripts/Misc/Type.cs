using System;
using UnityEngine;

namespace Cub
{
    public enum Cube
    {
        Steel = 1,
        Carbon = 2
    }

    public enum Team
    {
        Red,
        Blue
    }

    public enum Target
    {
        None = 0,
        Self = 1,
        Ally = 2,
        Enemy = 3,
        Terrain = 4
    }

    public enum Condition
    {
        None,
        Any,
        Closest,
        Is_Soldier,
        Is_Knight,
        Is_Rocket,
        Is_Medic,
        Is_Sniper,
        Is_Jerk,
        Adjacent_2,
        Almost_Dead,
        Is_Hurt,
        I_Am_Alone,
        They_Are_Alone

        //Closest,
        //Furthest,
        //Not_In_Range,
        //Is_Mage,
        //Is_Archer,
        //Is_Not_Knight,
        //Is_Not_Mage,
        //Is_Not_Archer,
        ////TwoEnemiesAdjacent,
        //Damaged,
        //Not_Damaged,

    }

    public enum Action
    {
        None = 0,
        Explore,
        Attack,
        Charge,
        Heal,
        Missile,
        Snipe,
        Follow_Ally,
        Follow_Enemy

        //Approach,
        //Fallback,
        //Blizzard,
        //Stealth
    }

    public enum Terrain
    {
        None = 0,
        Grass,
        Desert
    }

    public enum Class
    {
        None,
        Knight,
        Soldier,
        Rocket,
        Jerk, //WTF IS A JERK?!
        Sniper,
        Medic
    }

    public enum Event
    {
        None,
        Walk,
        Attack,
        TakeDamage,
        Missile,
        Snipe,//
        Heal,//
        BeHealed,//
        Die,
        GameOver//
    }

    public enum Animation
    {
        None,
        Walk,
        Attack,
        TakeDamage,
        Heal,
        BeHealed,
        Die,
        Cheer
    }

    public struct Position2
    {
        public int X;
        public int Y;

        public Position2(int _X, int _Y)
        {
            X = _X;
            Y = _Y;
        }

        public static bool operator ==(Position2 A, Position2 B)
        {
            if (A.X == B.X && A.Y == B.Y)
                return true;
            else
                return false;
        }

        public static bool operator !=(Position2 A, Position2 B)
        {
            if (A.X == B.X && A.Y == B.Y)
                return false;
            else
                return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (this.X == ((Position2)obj).X && this.Y == ((Position2)obj).Y)
                return true;
            else
                return false;
        }

        public Vector2 ToVector2()
        {
            return new Vector2(this.X, this.Y);
        }
    }

    public struct Position3
    {
        public int X;
        public int Y;
        public int Z;

        public Position3(int _X, int _Y, int _Z)
        {
            this.X = _X;
            this.Y = _Y;
            this.Z = _Z;
        }

        public static bool operator ==(Position3 A, Position3 B)
        {
            if (A.X == B.X && A.Y == B.Y && A.Z == B.Z)
                return true;
            else
                return false;
        }

        public static bool operator !=(Position3 A, Position3 B)
        {
            if (A.X == B.X && A.Y == B.Y && A.Z == B.Z)
                return false;
            else
                return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Position3))
            {
                return false;
            }
            
            if (this.X == ((Position3)obj).X && this.Y == ((Position3)obj).Y && this.Z == ((Position3)obj).Z)
                return true;
            else
                return false;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(this.X, this.Y, this.Z);
        }
    }

    public struct Cubon
    {
        public Color32 Color;
        public Position3 Position;

        public Cubon(Color32 _C, Position3 _P)
        {
            this.Color = _C;
            this.Position = _P;
        }

        public static bool operator ==(Cubon A, Cubon B)
        {
            if (A.Position.Equals(B.Position) && A.Color.Equals(B.Color))
                return true;
            else
                return false;
        }

        public static bool operator !=(Cubon A, Cubon B)
        {
            if (A.Position == B.Position && A.Color.Equals(B.Color))
                return false;
            else
                return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Debug.Log(obj.GetType());
            if (this.Position.Equals(((Cubon)obj).Position) && this.Color.Equals(((Cubon)obj).Color))
                return true;
            else
                return false;
        }
    }
}
