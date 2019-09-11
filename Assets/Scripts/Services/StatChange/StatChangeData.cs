using System.Collections;
using System.Collections.Generic;
using FixMath.NET;
using UnityEngine;
using Model.Units;

namespace Services.StatChange
{

	public struct StatChangeData
	{

		public UnitModel sender;
		public UnitModel receiver;
        public Fix64 value;


	}
}
