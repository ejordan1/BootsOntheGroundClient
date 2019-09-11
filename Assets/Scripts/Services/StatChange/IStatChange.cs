using System;
using Services.StatChange;

namespace Services.StatChange
{
	public interface IStatChange
	{
		void Apply(StatChangeData data); //this just sets up the method down there: is called in later script, IBuff.Apply
		bool Validate (StatChangeData data);
	}

	public class WrapperStatChange : IStatChange{ 
		private Action<StatChangeData> _apply;   //these are those lamda functions set in the other script. 
		private Func<StatChangeData, bool> _buffCheck;

		public WrapperStatChange(Action<StatChangeData> apply){
			//these are defined in the Buffs script, these are not called elsewhere
			//I think these are set at the beginning of the game, not created at run time.
			_apply = apply;

		}

		public WrapperStatChange(Action<StatChangeData> apply, Func<StatChangeData, bool> buffCheck){ //you do not need to pass the clear it can be null
			_apply = apply;
			_buffCheck = buffCheck;
		}


		public void Apply (StatChangeData data) //data contains the unit as well as the other info
		{
			_apply (data);  //this just calls the lamda from the action that is passed in, with data.
		}
			


		public bool Validate(StatChangeData data){  //sender / receiver 
			if (_buffCheck != null){
				if (!_buffCheck (data)) {
					return false;
				}
			}
			return data.receiver.IsAlive;
		}
	}

}

