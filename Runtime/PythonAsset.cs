using UnityEngine;
using UnityEditor.AssetImporters;

namespace SmartAssistant
{
  [ScriptedImporter(1, "py")]
  public class PythonImporter : ScriptedImporter
  {
    public override void OnImportAsset(AssetImportContext ctx)
    {
      PythonAsset pythonAsset = ScriptableObject.CreateInstance<PythonAsset>();
      pythonAsset.filePath = ctx.assetPath;
      ctx.AddObjectToAsset("pythonAsset", pythonAsset, Resources.Load<Texture2D>("PythonLogo"));
      ctx.SetMainObject(pythonAsset);
    }
  }

  [System.Serializable]
  public class PythonAsset : ScriptableObject
  {
    public string filePath;
  }
}