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

public class mvDrone : MonoBehaviour {

	//[SerializeField] [Range(-50,50)] private float Position_x1 = 0f;
	//[SerializeField] [Range(0,50)] private float Position_y1 = 13.7f;
	//[SerializeField] [Range(-50,50)] private float Position_z1 = 0f;

	//[SerializeField] [Range(-50,50)] private float Position_x2 = 0f;
	//[SerializeField] [Range(0,50)] private float Position_y2 = 13.7f;
	//[SerializeField] [Range(-50,50)] private float Position_z2 = 0f;

	//[SerializeField] [Range(0,10)] private int speed = 3;

	private List<Vector3> mv = new List<Vector3>();
	private List<int> mvSpeed = new List<int> ();
	private List<float> mvStopTime = new List<float> ();

	private Vector3 fc = new Vector3(0,0,0);
	private Transform zc;
	private float stopTimer=0.0f;
	private float stopTime=3.0f;
	private int mvStack = 0;
	private int mvIndex = 0;
	private bool stopState = true;
	private string actData;
	private string[] actDataSplit = new string[5];

    void Start () {
		zc = this.transform;
		stopTimer = 0.0f;

		//Read csvfrom.csv file
		List<Dictionary<string,object>> data = CSVReader.Read("csvform");
		//line counts 
		for (var i = 0; i < data.Count; i++) {
			if(data [i] ["" + this.gameObject.name].Equals(null) || data[i] ["" + this.gameObject.name].ToString()=="q") break;
			actData = data [i] ["" + this.gameObject.name].ToString();
			actDataSplit = actData.Split (' ');
			//Debug.Log ("act데이터"+actData);
			//first position setting
			if (i == 0) {
				fc.Set (float.Parse (actDataSplit [0]), float.Parse (actDataSplit [1]), float.Parse (actDataSplit [2]));
				this.transform.position = fc;
				mvStopTime.Add (float.Parse (actDataSplit [4]));
				stopTime = mvStopTime [0];
				//Debug.Log("First Position : "+fc);
			} else {
				mv.Add (new Vector3 (float.Parse (actDataSplit [0]), float.Parse (actDataSplit [1]), float.Parse (actDataSplit [2])));
				mvSpeed.Add (int.Parse (actDataSplit [3]));
				mvStopTime.Add (float.Parse (actDataSplit [4]));
				mvStack++;
			}
			Debug.Log("index " + (i).ToString() + " : " + this.gameObject.name + " " + actDataSplit [0] + " " + actDataSplit [1] + " " + actDataSplit [2] + " " + actDataSplit [3] + " " + actDataSplit [4]);
			if (i != 0) {
				//Debug.Log ("** index " + (i).ToString () + " : " + this.gameObject.name + " " + mv [i-1]);
			}
		}


    }



    void Update () {
		Debug.Log (this.gameObject.name + " mvST:" + mvStopTime [mvIndex + 1] + " mvIndex:" + mvIndex);
        moveDrone ();
		checkDrone ();
		//Debug.Log ("this.gameObject.name : "+this.gameObject.name+", mvStack : " + mvStack + ", mvSpeed : " + mvSpeed[mvIndex]); 
    }



    void moveDrone()

    {
		if (mvStack > 0 && mvStack > mvIndex ) {
			//Debug.Log (this.gameObject.name + " mvStack : " + mvStack + ", mvIndex : " + mvIndex);
			if (stopState == false) {
				this.transform.position = Vector3.MoveTowards (this.transform.position, mv [mvIndex], mvSpeed [mvIndex] * Time.deltaTime);
				//Debug.Log ("***"+this.gameObject.name + " mvStack : " + mvStack + ", mvIndex : " + mvIndex+", mv[myIndex] : "+mv[mvIndex]+", myIndex : "+mvIndex);
			} else if(stopTime <= stopTimer) {
				stopTimer = 0.0f;
				stopState = false;
				stopTime = mvStopTime[mvIndex+1];
			}
		}
    }

	void checkDrone() {
		if (mvStack > 0 && this.transform.position == mv [mvIndex] && stopState == false) {
			stopState = true;
			mvIndex++;
		} else if (stopState == true) {
			stopTimer += Time.deltaTime;
		}
	}
}
