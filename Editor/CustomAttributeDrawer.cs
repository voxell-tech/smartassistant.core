using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SmartAssistant.Core
{
  [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
  public class ReadOnlyAttributeDrawer : PropertyDrawer
  {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      GUI.enabled = false;
      EditorGUI.PropertyField(position, property, label, true);
      GUI.enabled = true;
    }
  }

  [CustomPropertyDrawer(typeof(SceneAttribute))]
  public class SceneAttributeDrawer : PropertyDrawer
  {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      // base.OnGUI(position, property, label);
      EditorGUI.PropertyField(position, property, label, true);
      // SceneAsset sceneAsset;
      // sceneAsset.name = "";
      // var newScene = EditorGUILayout.ObjectField(label, sceneAsset, )
    }
  }
}