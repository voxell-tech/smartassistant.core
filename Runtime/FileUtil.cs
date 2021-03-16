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

The Original Code is Copyright (C) 2020 Voxell Technologies.
All rights reserved.
*/

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