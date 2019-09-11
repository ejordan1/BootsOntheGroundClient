using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEditor;
using UnityEngine;
using Utils.Extensions;

namespace Editor.Drawers
{
    [CustomPropertyDrawer(typeof(AbilityIdAttribute))]
    public class AbilityIdDrawer : PropertyDrawer
    {
        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var registry = AbilityVariablesRegistry.InternalRef;

            if (registry != null && registry.Data != null)
            {
                var abilityId = property.stringValue;
                var abilityIds = registry.Data.Select(data => data.Id).ToArray();
                var currentIdx = Array.IndexOf(abilityIds, abilityId);
                if (currentIdx < 0) currentIdx = 0;
                var selectedIdx = EditorGUI.Popup(position, label, currentIdx, abilityIds.Select(id => new GUIContent(id)).ToArray());
                if (selectedIdx >= 0)
                {
                    property.stringValue = abilityIds[selectedIdx];
                }
            }
            else
            {
                // something went wrong
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}