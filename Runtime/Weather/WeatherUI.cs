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
using System.Diagnostics;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeatherUI : MonoBehaviour
{
  [HideInInspector] public int skystate;
  public TextMeshProUGUI location;
  public TextMeshProUGUI temperature;
  public TextMeshProUGUI description;
  public TextMeshProUGUI humidity;
  public TextMeshProUGUI windSpeed;
  public TextMeshProUGUI windDirection;

  public Image weatherIcon;
  public Sprite[] Morning;
  public Sprite[] noon;
  public Sprite[] night;

  public void UpdateSkyState() // declare sky condition based on time
  {
     //0- Morning, 1- Noon, 2-Night
    string h24 = DateTime.Now.ToString("HH");
    int h = int.Parse(h24);
    if(h >= 5 && h < 12) skystate = 0;
    else if(h >= 12 && h < 19) skystate = 1;
    else if(h >= 19 || h < 5) skystate = 2;
    // else print("Time preset error");
  }

  public void UpdateText() // display variables to text on screen
  {
    temperature.text = WebIP.temperature;
    description.text = WebIP.description;
    humidity.text = WebIP.humid;
    windSpeed.text = WebIP.windSpeed + "km/h";
    windDirection.text = WebIP.windDir;
    location.text = $"{WebIP.state}, {WebIP.city}, {WebIP.country}"; // display Ip info
  }

  //Clear[0], Clouds[1], Drizzle[2], Rain[3], Thunderstorm[4], Snow[5]
  public void IconChange() // decides weather icon to be displayed 
  {
    string main = WebIP.description; // switch cuz theres to many to compare
    if(skystate == 0) // morning
    {
      switch (main)
      {
        case "clear sky":
          weatherIcon.sprite = Morning[0];
          break;
        case "few clouds":
        case "broken clouds":
        case "scattered clouds":
        case "overcast clouds":
          weatherIcon.sprite = Morning[1];
          break;
        case "light rain":
          weatherIcon.sprite = Morning[2];
          break;
        case "rain":
          weatherIcon.sprite = Morning[3];
          break;
        case "light snow":
        case "snow":
          weatherIcon.sprite = Morning[5];
          break;
        default:
          print("weather state not found");
          break;
      }
    }

    if(skystate == 1) // noon
    {
      switch (main)
      {
        case "clear sky":
          weatherIcon.sprite = noon[0];
          break;
        case "few clouds":
        case "broken clouds":
        case "scattered clouds":
        case "overcast clouds":
          weatherIcon.sprite = noon[1];
          break;
        case "light rain":
          weatherIcon.sprite = noon[2];
          break;
        case "rain":
          weatherIcon.sprite = noon[3];
          break;
        case "light snow":
        case "snow":
          weatherIcon.sprite = noon[5];
          break;
        default:
          print("weather state not found");
          break;
      }
    }

    if(skystate == 2) // night
    {
      switch (main)
      {
        case "clear sky":
          weatherIcon.sprite = night[0];
          break;
        case "few clouds":
        case "broken clouds":
        case "scattered clouds":
        case "overcast clouds":
          weatherIcon.sprite = night[1];
          break;
        case "light rain":
          weatherIcon.sprite = night[2];
          break;
        case "rain":
          weatherIcon.sprite = night[3];
          break;
        case "light snow":
        case "snow":
          weatherIcon.sprite = night[5];
          break;
        default:
          print("weather state not found");
          break;
      }
    }
  }
    
    // if(skystate == 0) //Morning
    // {
    //   if(main == "clear sky") weatherIcon.sprite = Morning[0];
    //   else if(main == "few clouds" 
    //   ||main == "broken clouds"
    //   || main == "scattered clouds"
    //   || main == "overcast clouds") weatherIcon.sprite = Morning[1];
    //   else if(main == "light rain") weatherIcon.sprite = Morning[2];
    //   else if(main == "rain") weatherIcon.sprite = Morning[3];
    //   // else if(main == "Thunderstorm") weatherIcon.sprite = Morning[4];
    //   else if(main == "light snow" || main == "snow") weatherIcon.sprite = Morning[5];
    //   else Debug.Log("Weather State not found");
    // }


  // refresh button call
  // void UpdateWeather() => weatherAPI.GetRealTimeWeather(ref temperature, ref windspeed, ref description ,ref pressure);

} 