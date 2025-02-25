using System;
using Game.Scripts.Animals;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Editor.CustomEditors
{
    [CustomEditor(typeof(AnimalSettings))]
    public class AnimalSettingsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var settings = (AnimalSettings)target;

            settings.Name = EditorGUILayout.TextField("Animal Name", settings.Name);
            settings.Model = (GameObject)EditorGUILayout.ObjectField(
                "Animal Model", 
                settings.Model, 
                typeof(GameObject),
                false);
            settings.FoodChainPosition = (FoodChainPosition)EditorGUILayout.EnumPopup("Food Chain Position", settings.FoodChainPosition);
            settings.AnimalMoveType = (AnimalMoveType)EditorGUILayout.EnumPopup("Move Type", settings.AnimalMoveType);
            settings.MoveDirection = EditorGUILayout.Vector2Field("Move Direction", settings.MoveDirection);

            switch (settings.AnimalMoveType)
            {
                case AnimalMoveType.Linear:
                    settings.Speed = EditorGUILayout.FloatField("Linear Speed", settings.Speed);
                    break;
                case AnimalMoveType.Jump:
                    settings.JumpForce = EditorGUILayout.FloatField("Jump Force", settings.JumpForce);
                    settings.JumpInterval = EditorGUILayout.FloatField("Jump Interval", settings.JumpInterval);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}