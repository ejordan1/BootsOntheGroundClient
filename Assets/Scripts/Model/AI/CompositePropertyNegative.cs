using System;
using FixMath.NET;
using Model.Units;
using UnityEngine;

namespace Model.AI
{
	public class CompositePropertyNegative
	{

		private Fix64 _baseValue;
		private Fix64 _addedValue = Fix64.Zero;
		private Fix64 _multiplierValue = Fix64.One;

        //private Fix64 maxValue = 1000000000;
        //private Fix64 minValue = 0;

	    public CompositePropertyNegative(int initialValue) : this ((Fix64) initialValue)
	    {
	        
	    }

        public CompositePropertyNegative (Fix64 initialValue)
		{

			_baseValue = initialValue;
		}

		public void setBaseValue(Fix64 newBaseValue){
			_baseValue = newBaseValue;
		}

		public void addedValueChange(Fix64 value){
			_addedValue += value;

		}

		public void multiplierValueChange(Fix64 value){
			_multiplierValue += value;

		}

        // removed cast to int
		public Fix64 value {
			//get { return (int) Mathf.Clamp(((_baseValue + _addedValue)*_multiplierValue), 0, 2147000000); } 

			get { return (_baseValue + _addedValue)*_multiplierValue; }  //not clamped at zero.
		}

		public Fix64 getMultiplier(){
			return _multiplierValue;
		}
		public Fix64 getBaseValue(){
			return _baseValue;
		}
		public Fix64 getAddedValue(){
			return _addedValue;
		}
	}
}

//tests