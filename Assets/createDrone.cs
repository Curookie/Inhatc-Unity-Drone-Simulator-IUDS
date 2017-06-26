/*
 .c#
 This file is part of Inhatc Unity Drone Simulator (IUDS)

 Copyright (C) 2017 JongJin Won, Hyo Hyun Choi
 curookie AT naver DOT com, hchoi AT inhatc DOT ac DOT kr

IUDS is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

IUDS is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.

*/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class createDrone : MonoBehaviour
{

	private int createCount;
	private String actData;
	private GameObject[] drone = new GameObject[100];
	//public GUIText droneInfo;

	void Awake ()
	{
		
		//Read csvfrom.csv file
		List<Dictionary<string,object>> data = CSVReader.Read ("csvform");
		//line counts 
		createCount = Int32.Parse (data [0] ["#drone_count"].ToString ());
		for (var i = 0; i < createCount; i++) {
			GameObject prefab = Resources.Load ("Prefabs/drone") as GameObject;
			drone [i] = MonoBehaviour.Instantiate (prefab) as GameObject;
			drone [i].name = "drone" + (i + 1);
			drone [i].AddComponent<mvDrone> ();
			//droneInfo.text = drone [i].name.ToString();
			//drone [i].AddComponent<GUIText> ();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
