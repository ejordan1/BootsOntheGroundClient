using FixMath.NET;
using Model.AI;
using Model.Units;
using UnityEngine;
using System;

namespace Data
{
	[Serializable]
	public struct WorldPosition 
    {
		[SerializeField]	
		private  Fix64 _x;

		[SerializeField]	
		private  Fix64 _y;


        public Fix64 X
        {
            get { return _x; }
        }

        public Fix64 Y
        {
            get { return _y; }
        }

        public WorldPosition(int x, int y) : this ((Fix64) x, (Fix64) y)
        {
            
        }

        public WorldPosition(float x, float y) : this((Fix64)x, (Fix64)y)
        {
            
        }

        public WorldPosition(Fix64 x, Fix64 y)
        {
            _x = x;
            _y = y;
        }

        public Vector3 ToVector3()
        {
            return new Vector3((float) _x, 0, (float) _y);
        }

        public WorldPosition Offset(Fix64 offsetX, Fix64 offsetY)
        {
            return new WorldPosition(_x + offsetX, _y + offsetY);
        }

        public static Fix64 DistanceSq(WorldPosition one, WorldPosition two)
        {
            return (one.X - two.X)*(one.X - two.X) + (one.Y - two.Y)*(one.Y - two.Y);
        }

        public static Fix64 Distance(WorldPosition one, WorldPosition two)
        {
            return Fix64.Sqrt((one.X - two.X)*(one.X - two.X) + (one.Y - two.Y)*(one.Y - two.Y));
        }

        public Fix64 GetMagnitude()
        {
            return Fix64.Sqrt(_x*_x + _y*_y);
        }

        public bool IsInRange(WorldPosition other, int range)
        {
            return IsInRange(other, (Fix64) range);
        }

        public bool IsInRange(WorldPosition other, Fix64 range)
        {
            return DistanceSq(this, other) <= range*range;
        }

        public static WorldPosition operator -(WorldPosition a, WorldPosition b)
        {
            return new WorldPosition(a._x - b._x, a._y - b._y);
        }

        public static WorldPosition operator +(WorldPosition a, WorldPosition b)
        {
            return new WorldPosition(a._x + b._x, a._y + b._y);
        }

        public static WorldPosition operator *(WorldPosition a, Fix64 f)
        {
            return new WorldPosition(a._x*f, a._y*f);
        }

        public static WorldPosition operator /(WorldPosition a, Fix64 f)
        {
            return new WorldPosition(a._x/f, a._y/f);
        }

        public static bool operator ==(WorldPosition a, WorldPosition b)
        {
            return (a._x == b._x) && (a._y == b._y);
        }

        public static bool operator !=(WorldPosition a, WorldPosition b)
        {
            return !(a == b);
        }

        private bool Equals(WorldPosition other)
        {
            return _x.Equals(other._x) && _y.Equals(other._y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is WorldPosition && Equals((WorldPosition) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _x.GetHashCode();
                hashCode = (hashCode*397) ^ _y.GetHashCode();
                return hashCode;
            }
        }

        public WorldPosition GetFowardVect(WorldPosition a)
        {
            var vect = a - this;
            return vect/vect.GetMagnitude();
        }

        public override string ToString()
        {
            return string.Format("[WP x={0:F2}, y={1:F2}]", (float) _x, (float) _y);
        }
    }

    
}