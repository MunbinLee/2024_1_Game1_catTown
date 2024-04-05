using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    // 회전에 관한 변수
        public float rotSpeed = 200f;

        float mx = 0;
        float my = 0;

    // 스크립트 호출
    void Start()
    {
        
    }

    // 플레이어 마우스 움직임에 따른 화면 전환
    void Update()
    {

     // 마우스 입력 받음.

     float mouse_X = Input.GetAxis("Mouse X");
     float mouse_Y = Input.GetAxis("Mouse Y");

     // 회전 값 변수 누적

     mx += mouse_X * rotSpeed * Time.deltaTime;
     my += mouse_Y * rotSpeed * Time.deltaTime;

     my = Mathf.Clamp(my, -90f, 90f);

     // 회전 방향으로 물체 회전

    transform.eulerAngles = new Vector3 (-my,mx,0);
    }
}