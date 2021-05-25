using UnityEditor;
using UnityEngine;

namespace SmartAssistant.Core.Inspector
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
    private SceneAsset newScene;
    private SceneAsset oldScene;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      if (oldScene == null)
      {
        if (property.stringValue != "")
        {
          string[] paths = AssetDatabase.FindAssets(property.stringValue);
          if (paths.Length > 0)
          {
            for (int p=0; p < paths.Length; p++)
            {
              string path = AssetDatabase.GUIDToAssetPath(paths[p]);
              SceneAsset searchScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
              if (searchScene != null && searchScene.name == property.stringValue) oldScene = searchScene;
            }
          }
        }
      }

      newScene = EditorGUI.ObjectField(position, label, oldScene, typeof(SceneAsset), false) as SceneAsset;
      oldScene = newScene;
      if (newScene != null)
      {
        property.stringValue = newScene.name;
      } else property.stringValue = "";
    }
  }

  [CustomPropertyDrawer(typeof(ButtonAttribute))]
  public class ButtonAttributeDrawer : DecoratorDrawer
  {
    public override void OnGUI(Rect position)
    {
      if (GUI.Button(position, "Test"))
      {
        Debug.Log("Button Pressed");
      }
    }
  }
}