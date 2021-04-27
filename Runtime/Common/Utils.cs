using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.Collections;
using Unity.Mathematics;

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

    public static void SetArray<T>(ref T[] array, T defaultValue)
    {
      for (int i=0; i < array.Length; i++) array[i] = defaultValue;
    }

    /// <summary>
    /// Create a copy of native array from a reference normal array
    /// </summary>
    public static NativeArray<T> CopyNativeArray<T>(in T[] array, Allocator allocator) where T : struct
    {
      NativeArray<T> outNativeArray = new NativeArray<T>(array.Length, allocator);
      for (int i=0; i < array.Length; i++) outNativeArray[i] = array[i];
      return outNativeArray;
    }
    /// <summary>
    /// Create a copy of native array from a normal array
    /// </summary>
    public static NativeArray<T> CopyNativeArray<T>(T[] array, Allocator allocator) where T : struct
    {
      NativeArray<T> outNativeArray = new NativeArray<T>(array.Length, allocator);
      for (int i=0; i < array.Length; i++) outNativeArray[i] = array[i];
      return outNativeArray;
    }
  }

  public static class MeshUtils
  {
    /// <summary>
    /// creates a deep copy of the mesh
    /// </summary>
    /// <param name="originMesh">source of mesh to copy from</param>
    /// <param name="newMesh">location of copied mesh</param>
    public static void CopyMesh(in Mesh originMesh, out Mesh newMesh)
    {
      newMesh = new Mesh();
      newMesh.vertices = originMesh.vertices;
      newMesh.triangles = originMesh.triangles;
      newMesh.uv = originMesh.uv;
      newMesh.normals = originMesh.normals;
      newMesh.colors = originMesh.colors;
      newMesh.tangents = originMesh.tangents;
    }

    /// <summary>
    /// get native array of mesh vertices
    /// </summary>
    /// <param name="meshData">mesh data</param>
    /// <param name="allocator">allocation type</param>
    /// <returns></returns>
    public static NativeArray<float3> NativeGetVertices(in Mesh.MeshData meshData, Allocator allocator)
    {
      int vertexCount = meshData.vertexCount;
      var vertices = new NativeArray<float3>(vertexCount, allocator);
      meshData.GetVertices(vertices.Reinterpret<Vector3>());
      return vertices;
    }

    /// <summary>
    /// get native array of mesh normals
    /// </summary>
    /// <param name="meshData">mesh data</param>
    /// <param name="allocator">allocation type</param>
    /// <returns></returns>
    public static NativeArray<float3> NativeGetNormals(in Mesh.MeshData meshData, Allocator allocator)
    {
      int indexCount = meshData.vertexCount;
      var normals = new NativeArray<float3>(indexCount, allocator);
      meshData.GetNormals(normals.Reinterpret<Vector3>());
      return normals;
    }
    
    /// <summary>
    /// get native array of triangle indices
    /// </summary>
    /// /// <param name="meshData">mesh data</param>
    /// <param name="allocator">allocation type</param>
    public static NativeArray<int> NativeGetIndices(in Mesh.MeshData meshData, Allocator allocator, int submesh=0)
    {
      int indexCount = meshData.GetSubMesh(submesh).indexCount;
      var triangles = new NativeArray<int>(indexCount, allocator);
      meshData.GetIndices(triangles, submesh);
      return triangles;
    }
  }
}