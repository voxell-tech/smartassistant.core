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

namespace SmartAssistant
{
  public enum LogStyle
  {
    Log,
    Warning,
    Error
  }

  public enum LogImportance
  {
    Normal,
    Important,
    Crucial,
    Critical
  }

  public class Logging
  {
    public LogImportance debugLevel;

    public Logging(LogImportance debugLevel)
    {
      this.debugLevel = debugLevel;
    }

    public void ConditionalLog(object message, LogImportance importance, LogStyle logStyle)
    {
      if (importance >= debugLevel)
      {
        if (logStyle == LogStyle.Log)
          Debug.Log(message);
        if (logStyle == LogStyle.Warning)
          Debug.LogWarning(message);
        if (logStyle == LogStyle.Error)
          Debug.LogError(message);
      }
    }
  }
}