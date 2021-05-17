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

namespace SmartAssistant
{
  public static class MathUtils
  {
    /// <summary>
    /// Calculate the least amount of split from the total size given a split size
    /// </summary>
    /// <param name="totalSize">total amount of size available</param>
    /// <param name="splitSize">maximum size of each divisions after the split</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CalculateSplit(int totalSize, int splitSize) => (totalSize + splitSize - 1) / splitSize;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CalculateGrids(uint totalThreads, uint grpSize) => (int)((totalThreads + grpSize - 1) / grpSize);

    public static void SetArray<T>(ref T[] array, T value)
    { for (int i=0; i < array.Length; i++) array[i] = value; }

    /// <summary>
    /// Shuffles an array
    /// </summary>
    public static void ShuffleArray<T>(ref T[] decklist)
    {
      for (int i = 0; i < decklist.Length; i++)
      {
        int randomIdx = UnityEngine.Random.Range(0, decklist.Length);
        T tempItem = decklist[randomIdx];
        decklist[randomIdx] = decklist[i];
        decklist[i] = tempItem;
      }
    }

    /// <summary>
    /// Perform a math operation to all floats in their respective axes
    /// </summary>
    /// <param name="vec1">first vector</param>
    /// <param name="vec2">second vector</param>
    /// <param name="operation">operation to perform (insert a function that takes in 2 floats and outputs a float)</param>
    /// <returns></returns>
    public static Vector3 VectorMathOperation(
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

    public static Vector3 VectorMathDiv(Vector3 vec1, Vector3 vec2)
    {
      float finalx, finalY, finalZ;
      finalx = vec1.x / vec2.x;
      finalY = vec1.y / vec2.y;
      finalZ = vec1.z / vec2.z;

      return new Vector3(finalx, finalY, finalZ);
    }

    public static Vector3 VectorMathMul(Vector3 vec1, Vector3 vec2)
    {
      float finalx, finalY, finalZ;
      finalx = vec1.x * vec2.x;
      finalY = vec1.y * vec2.y;
      finalZ = vec1.z * vec2.z;

      return new Vector3(finalx, finalY, finalZ);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint ExpandBits(uint v)
    {
      v = (v * 0x00010001u) & 0xFF0000FFu;
      v = (v * 0x00000101u) & 0x0F00F00Fu;
      v = (v * 0x00000011u) & 0xC30C30C3u;
      v = (v * 0x00000005u) & 0x49249249u;
      return v;
    }

    /// <summary>
    /// Calculates a 30-bit Morton code for the given 3D point located within the unit cube [0,1]
    /// </summary>
    /// <param name="x">x coordinate</param>
    /// <param name="y">y coordinate</param>
    /// <param name="z">z coordinate</param>
    /// <returns>morton code</returns>
    public static uint Morton3D(float x, float y, float z)
    {
      x = Mathf.Min(Mathf.Max(x * 1024.0f, 0.0f), 1023.0f);
      y = Mathf.Min(Mathf.Max(y * 1024.0f, 0.0f), 1023.0f);
      z = Mathf.Min(Mathf.Max(z * 1024.0f, 0.0f), 1023.0f);
      uint xx = ExpandBits((uint)x);
      uint yy = ExpandBits((uint)y);
      uint zz = ExpandBits((uint)z);
      return xx * 4 + yy * 2 + zz;
    }

    /// <summary>
    /// Calculates a 30-bit Morton code for the given 3D point located within the unit cube [0,1]
    /// </summary>
    /// <param name="point">3D coordinate</param>
    /// <returns></returns>
    public static uint Morton3D(Vector3 point) => Morton3D(point.x, point.y, point.z);

    /// <summary>
    /// Create a random vector based on a range
    /// </summary>
    public static Vector3 RandomVector(float minCoor, float maxCoor)
    {
      float x = UnityEngine.Random.Range(minCoor, maxCoor);
      float y = UnityEngine.Random.Range(minCoor, maxCoor);
      float z = UnityEngine.Random.Range(minCoor, maxCoor);
      return new Vector3(x, y, z);
    }

    public static uint __clz(uint x)
    {
      //do the smearing
      x |= x >> 1; 
      x |= x >> 2;
      x |= x >> 4;
      x |= x >> 8;
      x |= x >> 16;
      //count the ones
      x -= x >> 1 & 0x55555555;
      x = (x >> 2 & 0x33333333) + (x & 0x33333333);
      x = (x >> 4) + x & 0x0f0f0f0f;
      x += x >> 8;
      x += x >> 16;
      return 32 - (x & 0x0000003f); //subtract # of 1s from 32
    }

    /// <summary>
    /// Create a copy of native array from a reference normal array
    /// </summary>
    public static NativeArray<T> CopyToNativeArray<T>(ref T[] array, Allocator allocator) where T : struct
    {
      NativeArray<T> outNativeArray = new NativeArray<T>(array.Length, allocator);
      for (int i=0; i < array.Length; i++) outNativeArray[i] = array[i];
      return outNativeArray;
    }
  }
}