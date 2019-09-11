using Model.Units;
using UniRx;
using UnityEngine;
using Utils;
using Zenject;

namespace View
{
    public class UnitView : MonoBehaviour
    {
        [SerializeField] protected Material DeadMaterial;
        [SerializeField] protected Renderer DefaultRenderer;

        private UnitModel _unitModel;
        private Material _aliveMaterial;

        public UnitModel UnitModel
        {
            get { return _unitModel; }
        }

        [Inject]
        public void Construct(UnitModel unitModel)
        {
            _unitModel = unitModel;
            transform.position = UnitModel.Position.ToVector3();

            _aliveMaterial = DefaultRenderer.sharedMaterial;
            
            _unitModel
                .AliveState
                .Subscribe(value =>
                {
                    if (_unitModel.IsDead)
                    {
                        DefaultRenderer.sharedMaterial = DeadMaterial;
                    }
                    else
                    {
                        DefaultRenderer.sharedMaterial = _aliveMaterial;
                    }
                })
                .AddTo(this);
        }

        public class Factory : KeyViewResourceFactory<UnitModel, UnitView>
        {
            public Factory(DiContainer container) : base(container, "Units/{0}")
            {
            }
        }

        protected void Update()
        {
            transform.position = UnitModel.Position.ToVector3();
        }

        private void OnDrawGizmosSelected()
        {
//			Debug.Log (UnitModel.hP.getAddedValue());
			//Debug.Log (_unitModel.armor.getAddedValue());

            /*if (_unitModel.Target == null) return;

			Debug.Log ("me!");

            GizmoUtility.DrawArrow(transform.position, _unitModel.Target.Position.ToVector3(), Color.red);
			GizmoUtility.DrawCircle(transform.position, _unitModel.moveRange.value(), Color.cyan);*/
        }
    }
}