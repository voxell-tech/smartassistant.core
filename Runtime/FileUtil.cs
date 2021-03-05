using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace SmartAssistant
{
  public static class FileUtil
  {
    /// <summary>
    /// Read the file and return raw bytes from the file
    /// </summary>
    /// <param name="path">file path starting from and excluding Application.streamingAssetsPath</param>
    /// <returns>raw bytes from the file</returns>
    public static byte[] LoadFile(string path)
    {
      if (!IsPathRooted(path))
      {
        path = Path.Combine(Application.streamingAssetsPath, path);
      }

      if (Application.platform != RuntimePlatform.Android)
      {
        path = "file://" + path;
      }

      UnityWebRequest request = UnityWebRequest.Get(path);
      request.SendWebRequest();
      while (!request.isDone)
      {
      }
      return request.downloadHandler.data;
    }

    /// <summary>
    /// Read the file and return raw string from the file
    /// </summary>
    /// <param name="path">file path starting from and excluding Application.streamingAssetsPath</param>
    /// <returns>raw string from the file</returns>
    public static string ReadFile(string path)
    {
      if (!IsPathRooted(path))
      {
        path = Path.Combine(Application.streamingAssetsPath, path);
      }

      return File.ReadAllText(path);
    }

    static bool IsPathRooted(string path)
    {
      if (path.StartsWith("jar:file:"))
      {
        return true;
      }
      return Path.IsPathRooted(path);
    }
  }
}
