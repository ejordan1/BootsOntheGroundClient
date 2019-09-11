using System.ComponentModel;
using System.Text;
using Model.Units;
using UnityEditor;
using UnityEngine;
using View;

namespace Editor.Drawers
{
    [CustomEditor(typeof(UnitView))]
    public class UnitViewEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
		{
			EditorUtility.SetDirty( target );
            var skin = GUI.skin.GetStyle("Helpbox");

            var unitView = target as UnitView;
            var unitModel = unitView != null ? unitView.UnitModel : null;

            
		    EditorGUILayout.PropertyField(serializedObject.FindProperty("DeadMaterial"));
		    EditorGUILayout.PropertyField(serializedObject.FindProperty("DefaultRenderer"));

		    serializedObject.ApplyModifiedProperties();
            
            EditorGUILayout.BeginVertical(skin);
            ShowUnitInfo(unitModel);
            EditorGUILayout.EndVertical();
        }

        private void ShowUnitInfo(UnitModel unitModel)
        {
			
            if (unitModel == null)
            {
                EditorGUILayout.HelpBox("UnitModel not defined!", MessageType.Error);
                return;
            }

            var unitInfo = new StringBuilder();

            unitInfo.AppendLine(string.Format("Id={1}, Type={0}", unitModel.UnitData.Id, unitModel.Id));
            unitInfo.AppendLine(string.Format("Position={0}", unitModel.Position));
            unitInfo.AppendLine(string.Format("Target={0}", unitModel.GetAbilityTarget()));
			unitInfo.AppendLine(string.Format("Current HP: {0}", unitModel.hP.value));
			unitInfo.AppendLine(string.Format("Current Armor: {0}", unitModel.armor.value));
			unitInfo.AppendLine(string.Format("Current speed: {0}", unitModel.moveSpeed.value));
			unitInfo.AppendLine(string.Format("Current shield: {0}", unitModel.shield.value));
			unitInfo.AppendLine(string.Format("AliveState: {0}", unitModel.AliveState));
            
            unitInfo.AppendLine("Abilities:");
            foreach (var ability in unitModel.Abilities)
            {
                unitInfo.AppendLine(string.Format("► {0}", ability));
            }

            EditorGUILayout.TextArea(unitInfo.ToString(), GUI.skin.label);

        }
    }
}