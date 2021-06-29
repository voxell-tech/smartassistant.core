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

namespace Voxell
{
  public static class FileUtil
  {
    public static string projectPath
    { get => Application.dataPath.Substring(0, Application.dataPath.Length-6); }

    /// <summary>
    /// Get the path of the folder given a file path
    /// </summary>
    /// <param name="filePath">full file path</param>
    /// <returns></returns>
    public static string GetFolderPath(string filePath)
    {
      string folder = "";
      string[] paths = filePath.Split(new char[]{'/', '\\'});
      for (int p=0; p < paths.Length-1; p++) folder += paths[p] + '/';
      return folder;
    }

    /// <summary>
    /// Read the file and return raw bytes from the file
    /// </summary>
    /// <param name="path">file path starting from and excluding Application.streamingAssetsPath</param>
    /// <returns>raw bytes from the file</returns>
    public static byte[] LoadStreamingAssetFile(string path)
    {
      if (!IsPathRooted(path))
        path = Path.Combine(Application.streamingAssetsPath, path);

      return File.ReadAllBytes(path);
    }

    /// <summary>
    /// Read the file and return raw string from the file
    /// </summary>
    /// <param name="path">file path starting excluding Application.streamingAssetsPath</param>
    /// <returns>raw string from the file</returns>
    public static string ReadStreamingAssetFile(string path)
    {
      if (!IsPathRooted(path))
        path = Path.Combine(Application.streamingAssetsPath, path);

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
