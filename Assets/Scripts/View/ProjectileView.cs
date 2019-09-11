using Model.Units;
using UnityEngine;
using Utils;
using Zenject;
using Model.Projectiles;

namespace View
{
	public class ProjectileView : MonoBehaviour
	{
		private IProjectile _iProjectile;

		[Inject]
		public void Construct(IProjectile projectileModel)
		{
			_iProjectile = projectileModel;
			transform.position = _iProjectile.Position.ToVector3();
		}

		public class Factory : KeyViewResourceFactory<IProjectile, ProjectileView>
		{
			public Factory(DiContainer container) : base(container, "Projectiles/{0}")
			{
			}
		}

		protected void Update()
		{
			transform.position = _iProjectile.Position.ToVector3();
		}
			
	}
}