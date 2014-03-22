using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View
{
    public class CubonAltt
    {
        public CubeType CubeType { get; set; }
        public Position3 Position { get; set; }

        public CubonAltt()
        {

        }

        public CubonAltt(CubeType _Color, Position3 _Position)
        {
            this.CubeType = _Color;
            this.Position = _Position;
        }

        public static bool operator ==(CubonAltt A, CubonAltt B)
        {
            if (A.CubeType.Equals(B.CubeType) && A.Position.Equals(B.Position))
                return true;
            else
                return false;
        }

        public static bool operator !=(CubonAltt A, CubonAltt B)
        {
            if (A.CubeType.Equals(B.CubeType) && A.Position.Equals(B.Position))
                return false;
            else
                return true;
        }

        public override bool Equals(object obj)
        {
            if (this.CubeType.Equals(((CubonAltt)obj).CubeType) && this.Position.Equals(((CubonAltt)obj).Position))
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
