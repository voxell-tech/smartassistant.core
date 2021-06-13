/*
This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software Foundation,
Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301, USA.

The Original Code is Copyright (C) 2020 Voxell Technologies and Contributors.
All rights reserved.
*/

using UnityEditor;
using UnityEngine;

namespace SmartAssistant.Core.Inspector
{
  [CustomPropertyDrawer(typeof(InspectOnlyAttribute))]
  public class InspectOnlyDrawer : PropertyDrawer
  {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      GUI.enabled = false;
      EditorGUI.PropertyField(position, property, label, true);
      GUI.enabled = true;
    }
  }

  [CustomPropertyDrawer(typeof(SceneAttribute))]
  public class SceneDrawer : PropertyDrawer
  {
    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
      if (property.propertyType == SerializedPropertyType.String)
      {
        SceneAsset sceneObject = GetSceneObject(property.stringValue);
        SceneAsset scene = EditorGUI.ObjectField(position, label, sceneObject, typeof(SceneAsset), true) as SceneAsset;
        if (scene == null)
        {
          property.stringValue = "";
        } else if (scene.name != property.stringValue)
        {
          SceneAsset sceneObj = GetSceneObject(scene.name);
          if (sceneObj == null)
            Debug.LogWarning($"The scene {scene.name} cannot be used. To use this scene add it to the build settings for the project");
          else property.stringValue = scene.name;
        }
      }
      else EditorGUI.LabelField (position, label.text, "Use [Scene] with strings.");
    }

    protected SceneAsset GetSceneObject(string sceneObjectName)
    {
      if (string.IsNullOrEmpty(sceneObjectName)) return null;

      foreach (EditorBuildSettingsScene editorScene in EditorBuildSettings.scenes)
      {
        if (editorScene.path.IndexOf(sceneObjectName) != -1)
          return AssetDatabase.LoadAssetAtPath<SceneAsset>(editorScene.path);
      }
      Debug.LogWarning($"Scene [{sceneObjectName}] cannot be used. Add this scene to the 'Scenes in the Build' in build settings.");
      return null;
    }
  }

  [CustomPropertyDrawer(typeof(ButtonAttribute))]
  public class ButtonDrawer : PropertyDrawer
  {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      if (GUI.Button(position, "Test"))
      {
        Debug.Log("Button Pressed");
      }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
      return 16f;
    }
  }
}