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

using UnityEditor;
using System.Reflection;

namespace SmartAssistant.Core
{
  public static class Shortcuts
  {
    [MenuItem ("Tools/Shortcuts/Clear Console %#d")] // CTRL + SHIFT + D
    public static void ClearConsole()
    {
      Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
      System.Type type = assembly.GetType("UnityEditor.LogEntries");
      MethodInfo method = type.GetMethod("Clear");
      method.Invoke(new object(), null);
    }
  }
}