﻿using System;
using FixMath.NET;

namespace Services.Buffs
{
	public static class Buffs 
	{
		
		public static readonly IBuff MaxHpBuff = new WrapperBuff(  //these are of type IBuff: are passed as parameter in factory
			//PARAMETER 1
			data => {data.receiver.maxHp.addedValueChange ((Fix64)data.strength);},
			//PARAMETER 2
			data => {data.receiver.maxHp.addedValueChange ((Fix64) (-data.strength));});

		public static readonly IBuff MaxArmorBuff = new WrapperBuff(
			data => {data.receiver.armor.addedValueChange((Fix64)data.strength);},
			data => {data.receiver.armor.addedValueChange ((Fix64) (-data.strength));},
			data => {return data.strength > 0;}); //lambda one line functino will be called by wrapper buff

	}
}

