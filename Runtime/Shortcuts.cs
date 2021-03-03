using UnityEditor;
using System.Reflection;

static class Shortcuts
{
  [MenuItem ("Tools/Clear Console %#d")] // CMD + SHIFT + D
  public static void ClearConsole()
  {
    var assembly = Assembly.GetAssembly(typeof(SceneView));
    var type = assembly.GetType("UnityEditor.LogEntries");
    var method = type.GetMethod("Clear");
    method.Invoke(new object(), null);
  }
}