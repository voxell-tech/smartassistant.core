using UnityEngine;
using System;

namespace SmartAssistant.Core.Inspector
{
  public class ReadOnlyAttribute : PropertyAttribute
  {
    public ReadOnlyAttribute() {}
  }

  public class SceneAttribute : PropertyAttribute
  {
    public SceneAttribute() {}
  }

  public class ButtonAttribute : Attribute
  {
    public ButtonAttribute(string buttonName="") {}
  }
}