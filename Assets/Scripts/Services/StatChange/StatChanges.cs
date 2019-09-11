using System;
using FixMath.NET;

namespace Services.StatChange
{
	public static class StatChanges 
	{
		//ADDED VALUE
		public static readonly IStatChange AddedMoveSpeed = new WrapperStatChange(  //these are of type IBuff: are passed as parameter in factory
			data => {data.receiver.hP.addedValueChange (data.value);},
			data => {return data.receiver.hP.value > Fix64.Zero;});

		public static readonly IStatChange AddedArmor = new WrapperStatChange(
			data => {data.receiver.armor.addedValueChange(data.value);},
			data => {return data.receiver.hP.value > Fix64.Zero; }); 

		public static readonly IStatChange AddedSize = new WrapperStatChange(
			data => {data.receiver.size.addedValueChange(data.value);},
			data => {return data.receiver.hP.value > Fix64.Zero; }); 

		public static readonly IStatChange AddedWeight = new WrapperStatChange(
			data => {data.receiver.weight.addedValueChange(data.value);},
			data => {return data.receiver.hP.value > Fix64.Zero; }); 
		
		public static readonly IStatChange AddedRange = new WrapperStatChange(
			data => {data.receiver.range.addedValueChange(data.value);},
			data => {return data.receiver.hP.value > Fix64.Zero; }); 

		public static readonly IStatChange AddedHeal = new WrapperStatChange(
			data => {data.receiver.heal.addedValueChange(data.value);},
			data => {return data.receiver.hP.value > Fix64.Zero; }); 
		
		public static readonly IStatChange AddedShield = new WrapperStatChange(
			data => {data.receiver.shield.addedValueChange(data.value);},
			data => {return data.receiver.hP.value > Fix64.Zero; }); 

		public static readonly IStatChange AddedShieldReducing = new WrapperStatChange(
			data => {data.receiver.shieldReducing.addedValueChange(data.value);},
			data => {return data.receiver.shieldReducing.value > Fix64.Zero; }); 

		public static readonly IStatChange AddedMoveRange = new WrapperStatChange(
			data => {data.receiver.shield.addedValueChange(data.value);},
			data => {return data.receiver.moveRange.value > Fix64.Zero; });
		
		public static readonly IStatChange AddedMaxHP = new WrapperStatChange(
			data => {data.receiver.maxHp.addedValueChange(data.value);},
			data => {return data.receiver.moveRange.value > Fix64.Zero; });


		//MULTIPLIED VALUE
		public static readonly IStatChange MultiplierMoveSpeed = new WrapperStatChange(  //these are of type IBuff: are passed as parameter in factory
			data => {data.receiver.hP.addedValueChange (data.value);},
			data => {return data.receiver.hP.value > Fix64.Zero; });

		public static readonly IStatChange MultiplierArmor = new WrapperStatChange(
			data => {data.receiver.armor.multiplierValueChange(data.value);},
			data => {return data.receiver.hP.value > Fix64.Zero; }); 

		public static readonly IStatChange MultiplierSize = new WrapperStatChange(
			data => {data.receiver.size.multiplierValueChange(data.value);},
			data => {return data.receiver.hP.value > Fix64.Zero; }); 

		public static readonly IStatChange MultiplierWeight = new WrapperStatChange(
			data => {data.receiver.weight.multiplierValueChange(data.value);},
			data => {return data.receiver.hP.value > Fix64.Zero; }); 

		public static readonly IStatChange MultiplierRange = new WrapperStatChange(
			data => {data.receiver.range.multiplierValueChange(data.value);},
			data => {return data.receiver.hP.value > Fix64.Zero; }); 

		public static readonly IStatChange MultiplierHeal = new WrapperStatChange(
			data => {data.receiver.heal.multiplierValueChange(data.value);},
			data => {return data.receiver.hP.value > Fix64.Zero; }); 

		public static readonly IStatChange MultiplierShield = new WrapperStatChange(
			data => {data.receiver.shield.multiplierValueChange(data.value);},
			data => {return data.receiver.shield.value > Fix64.Zero; }); 

		public static readonly IStatChange MultiplierShieldReducing = new WrapperStatChange(
			data => {data.receiver.shieldReducing.multiplierValueChange(data.value);},
			data => {return data.receiver.shieldReducing.value > Fix64.Zero; }); 

		public static readonly IStatChange MultiplierMoveRange = new WrapperStatChange(
			data => {data.receiver.shield.multiplierValueChange(data.value);},
			data => {return data.receiver.moveRange.value > Fix64.Zero; });
		
		public static readonly IStatChange MultiplierMaxHP = new WrapperStatChange(
			data => {data.receiver.maxHp.multiplierValueChange(data.value);},
			data => {return data.receiver.moveRange.value > Fix64.Zero; });
		
		

	}
}


