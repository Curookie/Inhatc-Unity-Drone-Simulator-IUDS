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

public class mvCamera : MonoBehaviour {
 
 public Transform m_kTarget;
 public float  m_fDistance = 10.0f;
 public float  m_fxSpeed = 250.0f;
 public float  m_fySpeed = 120.0f;

 public float  m_fyMinLimit = -90f;
 public float  m_fyMaxLimit = 90f;
 
 private float x = 0.0f;
 private float y = 0.0f;
 
 void Start () {
    Vector3 angles = transform.eulerAngles;
       x = angles.y;
      y = angles.x;
	
 }

 public void Update () {
 
 }
 public void LateUpdate()
 {
  x += Input.GetAxis("Mouse X") * m_fxSpeed * 0.02f;
     y -= Input.GetAxis("Mouse Y") * m_fySpeed * 0.02f;
   
   y = ClampAngle(y, m_fyMinLimit, m_fyMaxLimit);
          
      Quaternion rotation = Quaternion.Euler(y, x, 0);
  Vector3 position = transform.position;
  if (m_kTarget) {
   position = rotation * new Vector3(0.0f, 0.0f, -m_fDistance);
         position += m_kTarget.position;
       
    }
    else
    {
   if (Input.GetKey(KeyCode.W))
   {
	position += (rotation * new Vector3(0.0f, 0.0f, 1.0f));
   }
   if(Input.GetKey(KeyCode.S))
   {
	position += (rotation * new Vector3(0.0f, 0.0f, -1.0f));
   }
   if(Input.GetKey(KeyCode.D))
   {
	position += (rotation * new Vector3(1.0f, 0.0f, 0));
   }
   if(Input.GetKey(KeyCode.A))
   {
	position += (rotation * new Vector3(-1.0f, 0.0f, 0));
   }
   
    }
  transform.rotation = rotation;
    transform.position = position;
 }
 public float ClampAngle (float angle ,float min,  float max) {
  if (angle < -360)
   angle += 360;
  if (angle > 360)
   angle -= 360;
  return Mathf.Clamp (angle, min, max);
 }
}