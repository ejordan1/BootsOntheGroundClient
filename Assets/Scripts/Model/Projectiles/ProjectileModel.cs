using System.Collections.Generic;
using System.Linq;
using Data;
using FixMath.NET;
using Model.Abilities;
using View;
using Model.AI;
using Model.Projectiles;
using UnityEngine;
using Model.Units;

namespace Model.Projectiles
{
	public abstract class ProjectileModel : IResource, IProjectile
	{

		public CompositeProperty hP; //add composite property instead of multiplier addition etc. 
		public CompositePropertyNegative damage; 
		public CompositeProperty moveSpeed;
		public CompositeProperty weight;
		public CompositeProperty range;
		public UnitModel _sender;
		public ITarget _receiver;

		public ProjectileData _projectileData;
        //CC EFFECTS:

        private bool _isHit;

        public ProjectileModel(ProjectileData projectileData, UnitModel sender, ITarget receiver, WorldPosition position) //constructor for the unit, takes unit data, sets it equal in the model.
		{
			_sender = sender;
			_receiver = receiver;
			SetPosition (position);
			_projectileData = projectileData;

			hP =  new CompositeProperty (0); //hp starts at 0; this is done throught he added value: I DONT GET THIS AT ALL WHY I WROTE THIS
			damage = new CompositePropertyNegative(projectileData.int1);
		//	Debug.Log ("Damage is " + damage);

			moveSpeed  = new CompositeProperty (projectileData.speed.GetValue());

//			Debug.Log ("speed is " + moveSpeed.value);
			weight = new CompositeProperty (projectileData.projectileHP);
			range = new CompositeProperty (projectileData.range.GetValue());
//			Debug.Log (range.value + " this is the range should be 1 ");
			_projectileData = projectileData;

			//hP.setBaseValue (unitData.initialHP);
			//			armor.setBaseValue (unitData.initialArmor);



		}
		//move unit data to here

		public WorldPosition Position { get; private set; }

		public ProjectileData projectileData { get; private set;}

		public string ResourceId
		{
			get { return _projectileData.resourceID; } //needs to return resource iD
		}

		public void SetPosition(WorldPosition position)
		{
			Position = position;
		}


		public WorldPosition GetPosition()
		{
			return Position;
		}
			

		#region IProjectile implementation

	    public virtual bool MoveLogic(Fix64 tickTime)
	    {
            if (_isHit) return true;

            var forward = _receiver.GetPosition() - Position;
            var distance = forward.GetMagnitude();
            var forwardNormalized = forward / distance;

            var delta = forwardNormalized * Fix64.Min(tickTime * moveSpeed.value, distance);

            this.SetPosition(this.Position + delta);

            _isHit = Position.IsInRange(_receiver.GetPosition(), (Fix64) .5f);

            return _isHit;
        }


		public abstract bool ExecuteLogic (); //abstract methods: will need to be overriden later


		#endregion
	}
}
