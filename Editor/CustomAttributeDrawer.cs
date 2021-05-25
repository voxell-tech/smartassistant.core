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