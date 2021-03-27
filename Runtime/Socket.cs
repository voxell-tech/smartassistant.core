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

using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using Newtonsoft.Json;

namespace SmartAssistant.Core
{
  public class Socket : MonoBehaviour
  {
    #region Socket Settings
    [Header("Socket Settings")]
    public string ipAddress = "localhost";
    public int port = 8052;
    public LogImportance debugLevel;

    public readonly int headerSize = 10;
    private Logging logging;
    #endregion

    #region TCP Connections
    public static Thread tcpListenerThread;
    public static TcpListener tcpListener;
    public static TcpClient connectedTcpClient;
    #endregion

    #region Socket Tasks
    public static readonly int maxTaskType = 3;
    public static Action[] socketActions;
    public static Action<string>[] socketInputActions;
    public static Func<string, string>[] socketFuncs;
    #endregion

    void Awake()
    {
      // Start TcpServer background thread
      tcpListenerThread = new Thread (new ThreadStart(SocketListen));
      tcpListenerThread.IsBackground = true;
      tcpListenerThread.Start();

      logging = new Logging(debugLevel);
    }

    void OnDisable()
    {
      tcpListenerThread.Abort();
    }

    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space)) SocketSend("Testing");
    }
    
    /// <summary> 
    /// Runs in background TcpServerThread; Handles incomming TcpClient requests 
    /// </summary> 
    public void SocketListen()
    {
      try
      {
        // Create listener on localhost port 8052. 
        tcpListener = new TcpListener(IPAddress.Parse(ipAddress), 8052);
        tcpListener.Start();
        logging.ConditionalLog("Server is listening", LogImportance.Important, LogStyle.Log);
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
                byte[] incommingData = new byte[length];
                Array.Copy(bytes, 0, incommingData, 0, length);
                string clientSocketMessage = Encoding.UTF8.GetString(incommingData);
                SocketMessage socketMessage = JsonConvert.DeserializeObject<SocketMessage>(clientSocketMessage);
                logging.ConditionalLog(
                  $"task received from client\nTask: {socketMessage.task}, Argument: {socketMessage.argument}",
                  LogImportance.Normal, LogStyle.Log
                );

                string methodOutput;
                ExecuteSocketTask(socketMessage, out methodOutput);

                if (methodOutput != null)
                {
                  // TODO: send message back to python
                }
              }
            }
          }
        }
      }
      catch (SocketException socketException)
      {
        logging.ConditionalLog(
          "SocketException " + socketException.ToString(),
          LogImportance.Crucial, LogStyle.Warning
        );
      }
    }

    /// <summary>
    /// Send SocketMessage to client using socket connection. 
    /// </summary>
    /// <param name="serverSocketMessage">SocketMessage to send to the client</param>
    private void SocketSend(string serverSocketMessage)
    {
      if (connectedTcpClient == null || connectedTcpClient.Available != 0) return;
      
      try
      {
        // Get a stream object for writing.
        NetworkStream stream = connectedTcpClient.GetStream();
        if (stream.CanWrite)
        {
          // string serverSocketMessage = "This is a SocketMessage from your server.";
          // Convert string SocketMessage to byte array.
          byte[] serverSocketMessageAsByteArray = Encoding.ASCII.GetBytes(serverSocketMessage);
          // Write byte array to socketConnection stream.
          stream.Write(serverSocketMessageAsByteArray, 0, serverSocketMessageAsByteArray.Length);
          logging.ConditionalLog(
            "Server sent his SocketMessage - should be received by client",
            LogImportance.Normal, LogStyle.Log);
        }
      }
      catch (SocketException socketException)
      {
        logging.ConditionalLog(
          "SocketException " + socketException.ToString(),
          LogImportance.Crucial, LogStyle.Warning
        );
      }
    }

    #region Socket Message Execution
    /// <summary>
    /// Executes task based on socket message
    /// </summary>
    /// <param name="socketMessage">socket message</param>
    /// <param name="methodOutput">output of the task carried</param>
    private void ExecuteSocketTask(SocketMessage socketMessage, out string methodOutput)
    {
      methodOutput = null;
      // bunch of returns to avoid errors
      if (socketMessage.taskType > maxTaskType)
      {
        logging.ConditionalLog(
          $"taskType recieved from client: {socketMessage.taskType} exceeds the number of task types: {maxTaskType}",
          LogImportance.Critical, LogStyle.Error);
        return;
      }

      if (socketMessage.taskType > 0 && socketMessage.argument == null)
      {
        logging.ConditionalLog(
          $"argument recieved from client is null but is required for taskType: {socketMessage.taskType}",
          LogImportance.Critical, LogStyle.Error);
        return;
      }

      switch(socketMessage.taskType)
      {
        case 0:
          if (CheckTaskViability(socketMessage.task, socketActions.Length))
            socketActions[socketMessage.task].Invoke();
          return;

        case 1:
          if (CheckTaskViability(socketMessage.task, socketInputActions.Length))
            socketInputActions[socketMessage.task].Invoke(socketMessage.argument);
          return;

        case 2:
          if (CheckTaskViability(socketMessage.task, socketFuncs.Length))
            methodOutput = socketFuncs[socketMessage.task].Invoke(socketMessage.argument);
          return;
      }
    }

    /// <summary>
    /// Checks if task index is within range of maximum task available
    /// </summary>
    /// <param name="task">task index called</param>
    /// <param name="maxTask">maximum number of task available</param>
    /// <returns>Returns true if task index is not more than the available number of task else false</returns>
    private bool CheckTaskViability(in int task, in int maxTask)
    {
      if (task > maxTask)
      {
        logging.ConditionalLog(
        $"task recieved from client: {task} exceeds the number of tasks: {maxTask}",
        LogImportance.Critical, LogStyle.Error);
        return false;
      }
      return true;
    }
    #endregion
  }
}