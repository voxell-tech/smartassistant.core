using UnityEngine;
using UnityEditor;

public class EditorBase : Editor
{
  public GUIStyle centeredLabelStyle, foldoutStyle, subFoldoutStyle, notes, box;

  public const int spaceA = 30, spaceB = 10;

  public void EnsureStyles()
  {
    centeredLabelStyle = new GUIStyle(GUI.skin.GetStyle("Label"));
    centeredLabelStyle.alignment = TextAnchor.UpperCenter;
    centeredLabelStyle.fontStyle = FontStyle.Bold;
    centeredLabelStyle.fontSize = 12;

    foldoutStyle = new GUIStyle(EditorStyles.foldout);
    foldoutStyle.fontStyle = FontStyle.Bold;
    foldoutStyle.fontSize = 14;
    foldoutStyle.normal.textColor = Color.gray;
    foldoutStyle.onNormal.textColor = new Color(0.7f, 1f, 1f, 1f);

    subFoldoutStyle = new GUIStyle(EditorStyles.foldout);
    subFoldoutStyle.fontStyle = FontStyle.Bold;
    subFoldoutStyle.fontSize = 12;
    subFoldoutStyle.normal.textColor = Color.gray * Color.cyan;
    subFoldoutStyle.onNormal.textColor = Color.cyan;

    notes = new GUIStyle(GUI.skin.GetStyle("label"));
    notes.fontStyle = FontStyle.Italic;
    notes.fontSize = 10;
    notes.alignment = TextAnchor.MiddleRight;

    box = GUI.skin.box;
    box.padding = new RectOffset(10, 10, 10, 10);
  }
}