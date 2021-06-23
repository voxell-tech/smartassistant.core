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

using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.Collections;

namespace Voxell.Mathx
{
  public static class VectorUtil
  {
    /// <summary>
    /// Perform a math operation to all floats in their respective axes
    /// </summary>
    /// <param name="vec1">first vector</param>
    /// <param name="vec2">second vector</param>
    /// <param name="operation">operation to perform (insert a function that takes in 2 floats and outputs a float)</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 MathOperation(
      Vector3 vec1,
      Vector3 vec2,
      System.Func<float, float, float> operation)
    {
      float finalx, finalY, finalZ;
      finalx = operation(vec1.x, vec2.x);
      finalY = operation(vec1.y, vec2.y);
      finalZ = operation(vec1.z, vec2.z);

      return new Vector3(finalx, finalY, finalZ);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Div(Vector3 vec1, Vector3 vec2)
    {
      float finalx, finalY, finalZ;
      finalx = vec1.x / vec2.x;
      finalY = vec1.y / vec2.y;
      finalZ = vec1.z / vec2.z;

      return new Vector3(finalx, finalY, finalZ);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Mul(Vector3 vec1, Vector3 vec2)
    {
      float finalx, finalY, finalZ;
      finalx = vec1.x * vec2.x;
      finalY = vec1.y * vec2.y;
      finalZ = vec1.z * vec2.z;

      return new Vector3(finalx, finalY, finalZ);
    }

    /// <summary>
    /// Create a random vector based on the given range
    /// </summary>
    public static Vector3 RandomVector(float minCoor, float maxCoor)
    {
      float x = UnityEngine.Random.Range(minCoor, maxCoor);
      float y = UnityEngine.Random.Range(minCoor, maxCoor);
      float z = UnityEngine.Random.Range(minCoor, maxCoor);
      return new Vector3(x, y, z);
    }

    /// <summary>
    /// Create a random vector based on the given range
    /// </summary>
    public static Vector3 RandomVector(Vector3 minCoor, Vector3 maxCoor)
    {
      float x = UnityEngine.Random.Range(minCoor.x, maxCoor.x);
      float y = UnityEngine.Random.Range(minCoor.y, maxCoor.y);
      float z = UnityEngine.Random.Range(minCoor.z, maxCoor.z);
      return new Vector3(x, y, z);
    }
  }
}