using System;
using Model.Projectiles;
using UnityEngine;
using Model.Units;
using Data;
using Zenject;
using Services.Buffs;
using Services.Commands;
using Data;
using Utils;
using Model.Projectiles;
using Services;
using Model.AI;
using System.Collections.Generic;
using FixMath.NET;
using Services.StatChange;


namespace Model.Projectiles
{
	public class SplashAllies : ProjectileModel //look up how base works later: inhereitance of classes, read about abstract classes
	{
		
		private readonly WorldModel _world;
		private readonly ProjectileManager _projectileManager;
		private readonly CommandProcessor _command;
		private readonly IFactory<StatChangeData, HPChangeCommand> _hPChangeFactory;



		public SplashAllies(ProjectileData projectileData, UnitModel sender, ITarget receiver, WorldPosition position, 
			WorldModel world, ProjectileManager projectileManager, CommandProcessor commandProcessor,

			IFactory<StatChangeData, HPChangeCommand> hPChangeFactory
		): base(projectileData, sender, receiver, position) 
		//override constructor: calls constructor of parent class with the parameters
		{
			_world = world;

			_projectileManager = projectileManager;
			_command = commandProcessor;
			_hPChangeFactory = hPChangeFactory;

		}


		#region IProjectile implementation

		public override bool ExecuteLogic ()
		{
			Debug.Log ("arrow execute  logic");
			var targets = _world.GetAllyUnitsTo (_sender.Alliance); //this is enemy right now
			if (targets == null) {
				Debug.Log ("(don't have allies) regular situation");
			}
			List<UnitModel> targs = targets.GetAllDistProjectile1 (this, (Fix64) 10);
			foreach (UnitModel targ in targs) {

				var hpPlus = _hPChangeFactory.Create(new StatChangeData {value = damage.value, receiver = targ, sender = _sender});
				_command.AddCommand (hpPlus);
				Debug.Log ("Im a target" + targ);
			}

		    return true;
		}
		#endregion

	}
}

