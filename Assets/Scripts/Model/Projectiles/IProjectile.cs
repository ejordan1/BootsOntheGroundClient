using System;
using Data;
using FixMath.NET;
using View;

namespace Model.Projectiles
{
	public interface IProjectile : IResource
	{
		bool MoveLogic(Fix64 tickTime);
		bool ExecuteLogic();

		WorldPosition Position {get; }
	}
}