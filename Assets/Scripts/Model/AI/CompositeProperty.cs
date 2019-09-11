using FixMath.NET;

namespace Model.AI
{
    public class CompositeProperty
    {
        private Fix64 _addedValue = new Fix64(0);

        private Fix64 _baseValue;
        private Fix64 _multiplierValue = new Fix64(1);

        //private Fix64 maxValue = 1000000000;
        //private Fix64 minValue = 0;


		//conversion constructor: converts to fix64 if passed int
        public CompositeProperty(int initialValue) : this((Fix64) initialValue)
        {
        }

        public CompositeProperty(Fix64 initialValue)
        {
            _baseValue = initialValue;
        }

        // removed cast to int
        public Fix64 value
        {
            //get { return (int) Mathf.Clamp(((_baseValue + _addedValue)*_multiplierValue), 0, 2147000000); } 
            //clamped it to 0 before, not good. 
            get { return (_baseValue + _addedValue)*_multiplierValue; }
        }

        public void setBaseValue(Fix64 newBaseValue)
        {
            _baseValue = newBaseValue;
        }

        public void addedValueChange(Fix64 val)
        {
            _addedValue += val;
        }

        public void multiplierValueChange(Fix64 val)
        {
            _multiplierValue += val;
        }

        public Fix64 getMultiplier()
        {
            return _multiplierValue;
        }

        public Fix64 getBaseValue()
        {
            return _baseValue;
        }

        public Fix64 getAddedValue()
        {
            return _addedValue;
        }

        public Fix64 returnModifiedValue(Fix64 value)
        {
            return (value + _addedValue)*_multiplierValue; //make sure this int conversion works
        }
    }
}

//tests