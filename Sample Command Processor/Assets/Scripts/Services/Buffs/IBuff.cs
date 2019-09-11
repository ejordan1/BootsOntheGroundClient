using System;

namespace Services.Buffs
{
	public interface IBuff
	{
		void Apply(BuffData data); //this just sets up the method down there: is called in later script, IBuff.Apply
		void Clear(BuffData data);
		bool Validate (BuffData data);
	}

	public class WrapperBuff : IBuff{ 
		private Action<BuffData> _apply;   //these are those lamda functions set in the other script. 
		private Action<BuffData> _clear;
		private Func<BuffData, bool> _buffCheck;

		public WrapperBuff(Action<BuffData> apply, Action<BuffData> clear = null){
			//these are defined in the Buffs script, these are not called elsewhere
			//I think these are set at the beginning of the game, not created at run time.
			_apply = apply;
			_clear = clear;
		}

		public WrapperBuff(Action<BuffData> apply, Action<BuffData> clear, Func<BuffData, bool> buffCheck){ //you do not need to pass the clear it can be null
			_apply = apply;
			_clear = clear;
			_buffCheck = buffCheck;
		}

		#region IBuff implementation  //maybe that is what this does: 

		public void Apply (BuffData data) //data contains the unit as well as the other info
		{
			_apply (data);  //this just calls the lamda from the action that is passed in, with data.
		}

		public void Clear (BuffData data)
		{
			if (_clear != null) {
				_clear (data);
			}
		}

		#endregion

		public bool Validate(BuffData data){  //sender / receiver 
			if (_buffCheck != null){
				if (!_buffCheck (data)) {
					return false;
				}
			}
			return data.receiver.isAlive;
		}
	}

}

