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
    /// Get the path of the folder given a file path by excluding the filename
    /// </summary>
    /// <param name="filePath">full file path</param>
    /// <returns>Folder path of the file</returns>
    public static string GetFolderPath(string filePath)
    {
      string folder = "";
      string[] paths = filePath.Split(new char[]{'/', '\\'});
      for (int p=0; p < paths.Length-1; p++) folder += paths[p] + '/';
      return folder;
    }

    /// <summary>
    /// Get the path of the folder given a file path by excluding the filename
    /// </summary>
    /// <param name="filePath">full file path</param>
    /// <param name="separator">separator of each folder in the filePath</param>
    /// <returns>Folder path of the file</returns>
    public static string GetFolderPath(string filePath, char[] separator)
    {
      string folder = "";
      string[] paths = filePath.Split(separator);
      for (int p=0; p < paths.Length-1; p++) folder += paths[p] + '/';
      return folder;
    }

    /// <summary>
    /// Get the name of the file given the file path by excluding all of its folder paths
    /// </summary>
    /// <param name="filePath">full file path</param>
    /// <returns></returns>
    public static string GetFilename(string filePath)
    {
      string[] paths = filePath.Split(new char[]{'/', '\\'});
      return paths[paths.Length-1];
    }

    /// <summary>
    /// Get the name of the file given the file path by excluding all of its folder paths
    /// </summary>
    /// <param name="filePath">full file path</param>
    /// <param name="separator">separator of each folder in the filePath</param>
    /// <returns></returns>
    public static string GetFilename(string filePath, char[] separator)
    {
      string[] paths = filePath.Split(separator);
      return paths[paths.Length-1];
    }

    /// <summary>
    /// Read all the bytes of a given VoxellAsset
    /// </summary>
    /// <returns>Raw bytes from the file</returns>
    public static byte[] ReadAssetFileByte(VoxellAsset voxellAsset)
    {
      string path = Path.Combine(projectPath, voxellAsset.filePath);
      return File.ReadAllBytes(path);
    }

    /// <summary>
    /// Read all the string text of a given VoxellAsset
    /// </summary>
    /// <returns>Raw string from the file</returns>
    public static string ReadAssetFileText(VoxellAsset voxellAsset)
    {
      string path = Path.Combine(projectPath, voxellAsset.filePath);
      return File.ReadAllText(path);
    }

    /// <summary>
    /// Read the file and return raw bytes from the file
    /// </summary>
    /// <param name="path">file path starting from and excluding Application.streamingAssetsPath</param>
    /// <returns>Raw bytes from the file</returns>
    public static byte[] ReadStreamingAssetFileByte(string path)
    {
      path = Path.Combine(Application.streamingAssetsPath, path);
      return File.ReadAllBytes(path);
    }

    /// <summary>
    /// Read the file and return raw string from the file
    /// </summary>
    /// <param name="path">file path starting excluding Application.streamingAssetsPath</param>
    /// <returns>Raw string from the file</returns>
    public static string ReadStreamingAssetFileText(string path)
    {
      path = Path.Combine(Application.streamingAssetsPath, path);
      return File.ReadAllText(path);
    }

  }
}
