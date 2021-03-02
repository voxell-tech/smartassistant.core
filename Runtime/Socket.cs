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

using System;
using System.Net; 
using System.Net.Sockets; 
using System.Text; 
using System.Threading; 
using UnityEngine;  

public class Socket : MonoBehaviour
{
  #region Socket Settings
  [Header("Socket Settings")]
  public static string ipAddress = "localhost";
  public static int port = 8052;
  #endregion

  #region TCP Connections
  // Background thread for TcpServer workload. 
  public static Thread tcpListenerThread;
  // TCPListener to listen for incomming TCP connection requests.
  public static TcpListener tcpListener;
  // Create handle to connected tcp client.
  public static TcpClient connectedTcpClient;
  #endregion

  void Awake ()
  {
    // Start TcpServer background thread
    tcpListenerThread = new Thread (new ThreadStart(StartListening));
    tcpListenerThread.IsBackground = true;
    tcpListenerThread.Start();
  }

  void OnDisable()
  {
    tcpListenerThread.Abort();
  }

  void Update ()
  {
    if (Input.GetKeyDown(KeyCode.Space)) SendMessage();
  }
  
  /// <summary> 
  /// Runs in background TcpServerThread; Handles incomming TcpClient requests 
  /// </summary> 
  public static void StartListening()
  {
    try
    {
      // Create listener on localhost port 8052. 
      tcpListener = new TcpListener(IPAddress.Parse(ipAddress), 8052); 
      tcpListener.Start();              
      Debug.Log("Server is listening");              
      Byte[] bytes = new Byte[1024];  
      while (true)
      {
        using (connectedTcpClient = tcpListener.AcceptTcpClient())
        {
          // Get a stream object for reading 
          using (NetworkStream stream = connectedTcpClient.GetStream())
          {
            int length;
            // Read incomming stream into byte arrary.
            while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
              var incommingData = new byte[length];
              Array.Copy(bytes, 0, incommingData, 0, length);
              // Convert byte array to string message.
              string clientMessage = Encoding.ASCII.GetString(incommingData);
              Debug.Log("client message received as: " + clientMessage);
            }
          }
        }
      }
    }
    catch (SocketException socketException)
    {
      Debug.Log("SocketException " + socketException.ToString());
    }
  }
  /// <summary> 
  /// Send message to client using socket connection. 
  /// </summary> 
  private void SendMessage()
  {
    if (connectedTcpClient == null || connectedTcpClient.Available != 0) return;
    // print();
    
    try
    {
      // Get a stream object for writing.
      NetworkStream stream = connectedTcpClient.GetStream();
      if (stream.CanWrite)
      {
        string serverMessage = "This is a message from your server.";
        // Convert string message to byte array.
        byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(serverMessage);
        // Write byte array to socketConnection stream.
        stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
        Debug.Log("Server sent his message - should be received by client");
      }
    }
    catch (SocketException socketException)
    {
      Debug.Log("Socket exception: " + socketException);
    }
  } 
}