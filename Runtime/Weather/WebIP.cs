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

The Original Code is Copyright (C) 2020 Voxell Technologies and Contributors.
All rights reserved.
*/

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Linq;
using Newtonsoft.Json;
using HtmlAgilityPack;

public class WebIP : MonoBehaviour
{
  public static float latitude, longitude;
  public static string city, state, country;
  public static int cityCode;
  public static string temperature;
  public static string description, humid, windSpeed, windDir;

  public void IPHtml()
  {
    // get details location of user based on public IP adreess
    HtmlWeb web = new HtmlWeb();

    // get html of the website
    var htmlDoc = web.Load("https://mylocation.org/");
    HtmlNode[] nodes = htmlDoc.DocumentNode.SelectNodes("//td").ToArray();

    // assign details to variables
    latitude = float.Parse(nodes[3].InnerHtml);
    longitude = float.Parse(nodes[5].InnerHtml);
    city = nodes[11].InnerHtml;
    state = nodes[9].InnerHtml;
    country = nodes[7].InnerHtml;
    string coords =$"Lat= {latitude} , long = {longitude}";
    string location =$"Location = {city}, {state}, {country}";
    // print(location);
  }

  public void WeatherHtml()
  {
    // get html of user's city weather condition 
    string url = "https://www.nearweather.com/location/" + cityCode;
    HtmlWeb web = new HtmlWeb();
    var htmlDoc = web.Load(url);

    // assign contents of the website to variables
    HtmlNode[] tempNode = htmlDoc.DocumentNode.SelectNodes("//*[@class = 'wn-box wn-temperature']").ToArray();
    temperature = tempNode[0].InnerText;

    HtmlNode[] detailNode = htmlDoc.DocumentNode.SelectNodes("//*[@class = 'wn-conditions-text']").ToArray();
    string[] condition = detailNode[0].InnerText.Split(' ');
    description = condition[2] + " " + condition[3];

    string[] humidity = detailNode[1].InnerText.Split(' ');
    humid = humidity[1];

    string[] windspeed = detailNode[2].InnerText.Split(' ');
    windSpeed = windspeed[2];

    string[] windDirection = detailNode[3].InnerText.Split(' ');
    windDir = windDirection[2];
    // print(temperature + " " + description + " " + humid + " " + windSpeed + " " + windDir );
  }
  public void ReadJson() // stream through citylist json file
  {
    var currentDirectory = Environment.CurrentDirectory;
    string path = currentDirectory +  @"\Assets\StreamingAssets\CityList.json";
    using(StreamReader r = new StreamReader(path))
    {
      string json = r.ReadToEnd();
      List<CityList> cityList = JsonConvert.DeserializeObject<List<CityList>>(json);
      foreach(var item in cityList)
      {
        // compare location state of user to component of cities in json file
        if(item.name == state) // city may have similar first names
        {
          // assign city id to variable when city list matches
          cityCode = item.id;
        }
      }
    }
    print(cityCode);
  }

  public void Main() // main program to run
  {
    IPHtml();
    ReadJson();
    WeatherHtml();
  }
}

  [Serializable]
  public class CityList // structure to decode from json file
  {
    public int id;
    public string name;
    public string state;
    public string country;
    // public string coord; // data type {a,b} not necessary
  }
