using System;
using Model.Units;

namespace Model.AI
{
	public class CompositeProperty
	{

		private float _baseValue;
		private float _addedValue = 0;
		private float _multiplierValue = 1;

		public CompositeProperty (float initialValue)
		{
			
			_baseValue = initialValue;
		}

		public void setBaseValue(float newBaseValue){
			_baseValue = newBaseValue;
		}

		public void addedValueChange(float value){
			_addedValue += value;
		
		}

		public void multiplierValueChange(float value){
			_addedValue += value;

		}

		public float value {
		    get { return (int) ((_baseValue + _addedValue)*_multiplierValue); }
		}

		public float getMultiplier(){
			return _multiplierValue;
		}
		public float getBaseValue(){
			return _baseValue;
		}
		public float getAddedValue(){
			return _addedValue;
		}
	}
}

//tests