using Model.Units;
using UnityEngine;
using Utils;
using Zenject;

namespace View
{
    public class UnitView : MonoBehaviour
    {
        private UnitModel _unitModel;

        [Inject]
        public void Construct(UnitModel unitModel)
        {
            _unitModel = unitModel;
            transform.position = _unitModel.Position.ToVector3();
        }

        public class Factory : KeyViewResourceFactory<UnitModel, UnitView>
        {
            public Factory(DiContainer container) : base(container, "Units/{0}")
            {
            }
        }

        protected void Update()
        {
            transform.position = _unitModel.Position.ToVector3();
        }

        private void OnDrawGizmosSelected()
        {
			Debug.Log (_unitModel.hP.getAddedValue());
			Debug.Log (_unitModel.armor.getAddedValue());

            /*if (_unitModel.Target == null) return;

			Debug.Log ("me!");

            GizmoUtility.DrawArrow(transform.position, _unitModel.Target.Position.ToVector3(), Color.red);
			GizmoUtility.DrawCircle(transform.position, _unitModel.moveRange.value(), Color.cyan);*/
        }
    }
}