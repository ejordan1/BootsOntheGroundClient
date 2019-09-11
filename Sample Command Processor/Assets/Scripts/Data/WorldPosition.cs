using Model.AI;
using Model.Units;
using UnityEngine;

namespace Data
{
    public struct WorldPosition
    {
        private readonly float _x;
        private readonly float _y;

        public float X
        {
            get { return _x; }
        }

        public float Y
        {
            get { return _y; }
        }

        public WorldPosition(float x, float y)
        {
            _x = x;
            _y = y;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(_x, 0, _y);
        }

        public WorldPosition Offset(float offsetX, int offsetY)
        {
            return new WorldPosition(_x + offsetX, _y + offsetY);
        }

        public static float DistanceSq(WorldPosition one, WorldPosition two)
        {
            return (one.X - two.X)*(one.X - two.X) + (one.Y - two.Y)*(one.Y - two.Y);
        }

        public static float Distance(WorldPosition one, WorldPosition two)
        {
            return Mathf.Sqrt((one.X - two.X)*(one.X - two.X) + (one.Y - two.Y)*(one.Y - two.Y));
        }

        public float GetMagnitude()
        {
            return Mathf.Sqrt(_x*_x + _y*_y);
        }

        public bool IsInRange(WorldPosition other, float range)
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

        public static WorldPosition operator *(WorldPosition a, float f)
        {
            return new WorldPosition(a._x*f, a._y*f);
        }

        public static WorldPosition operator /(WorldPosition a, float f)
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
    }

    public class WorldPositionTarget : ITarget
    {
        private readonly WorldPosition _position;

        public WorldPositionTarget(WorldPosition position)
        {
            _position = position;
        }

        public WorldPosition GetPosition()
        {
            return _position;
        }

        public UnitModel GetUnitModel()
        {
            return null;
        }
    }
}