using UnityEngine;
using UnityEditor;

namespace SmartAssistant.Core
{
  public class ReadOnlyAttribute : PropertyAttribute
  {
    public ReadOnlyAttribute() {}
  }

  public class SceneAttribute : PropertyAttribute
  {
    public SceneAttribute() {}
  }
}