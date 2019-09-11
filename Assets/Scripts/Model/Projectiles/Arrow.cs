using System;
using Model.Projectiles;
using UnityEngine;
using Model.Units;
using Data;
using Zenject;
using Services.Buffs;
using Services.Commands;

using Utils;

using Services;
using Model.AI;
using System.Collections.Generic;
using FixMath.NET;
using Services.StatChange;


namespace Model.Projectiles
{
	public class Arrow : ProjectileModel //look up how base works later: inhereitance of classes, read about abstract classes
	{

		private readonly WorldModel _world;
		private readonly ProjectileManager _projectileManager;
		private readonly CommandProcessor _command;
		private readonly IFactory<StatChangeData, HPChangeCommand> _hPChangeFactory;

	  

	    public Arrow(ProjectileData projectileData, UnitModel sender, ITarget receiver, WorldPosition position, 
			WorldModel world, ProjectileManager projectileManager, CommandProcessor commandProcessor,
		
			IFactory<StatChangeData, HPChangeCommand> hPChangeFactory
		): base(projectileData, sender, receiver, position) 
		//override constructor: calls constructor of parent class with the parameters
		{
			_world = world;

			_projectileManager = projectileManager;
			_command = commandProcessor;
			_hPChangeFactory = hPChangeFactory;
		    Target = (UnitTarget)receiver;        //this is BAD

		}


		#region IProjectile implementation

	    public override bool ExecuteLogic ()
		{
			var hpMinus = _hPChangeFactory.Create(new StatChangeData {value = (Fix64) damage.value, receiver = Target.UnitModel, sender = _sender});
			_command.AddCommand (hpMinus);

		    return true;
		}
		#endregion


		public UnitTarget Target { get; private set; }



	}
}

