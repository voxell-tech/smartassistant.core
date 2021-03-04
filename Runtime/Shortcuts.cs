using UnityEditor;
using System.Reflection;

static class Shortcuts
{
  [MenuItem ("Tools/Clear Console %#d")] // CMD + SHIFT + D
  public static void ClearConsole()
  {
    Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
    System.Type type = assembly.GetType("UnityEditor.LogEntries");
    MethodInfo method = type.GetMethod("Clear");
    method.Invoke(new object(), null);
  }
}