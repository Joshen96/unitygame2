using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //카메라 영역 범위 제한하기 
    public float leftLimit= 0.0f;
    public float rightLimit = 0.0f;
    public float topLimit = 0.0f;
    public float bottomLimit = 0.0f;
    public GameObject subBackScreen; 
    //화면밖으로 나가면 죽는 것 구현 ex타임어택 이동


    public bool isForceScrollX = false; //x축 강제 스크롤 플래그
    public float forceScrollSpeedX = -0.5f; //1초간 움직일 x의 거리
    public bool isForceScrollY = false; //y축 강제 스크롤 플래그
    public float forceScrollSpeedY = 0.5f; //1초간 움직일 y의 거리

    private void Start()
    {
        isForceScrollX = true;
    }
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player == null) //검수코드
        {
            Debug.LogError("플레이어를 찾을수없음");
            return; //중지
        }
        float x = player.transform.position.x;  //플레이어 오브젝트의 위치 넘겨받기
        float y = player.transform.position.y;
        float z = player.transform.position.z;

        if (isForceScrollX)
        {
            x = transform.position.x + (forceScrollSpeedX * Time.deltaTime); //1초당 forceScrollSpeedX(+면 오른쪽 -면 왼쪽)만큼 x축으로 이동
        }

        if (isForceScrollY)
        {
            x = transform.position.y + (forceScrollSpeedY * Time.deltaTime); //1초당 forceScrollSpeedY(+면 위로 -면 아래)만큼 y축으로 이동
        }


        if (x < leftLimit)
        {
            x = leftLimit;
        }
        else if(x>rightLimit)
        {
            x = rightLimit;
        }

        if (y < bottomLimit)
        {
            y = bottomLimit;
        }
        else if (y > topLimit)
        {
            y = topLimit;
        }

        Vector3 v3 = new Vector3(x, y, -10);  //플레이어 영역제한한 움직임 x,y,z받고


        transform.position = v3; //카메라에 포지션 넣기

        if(subBackScreen != null)
        {
            y = subBackScreen.transform.position.y;
            z = subBackScreen.transform.position.z;

            Vector3 vSub = new Vector3(x / 2.0f, y, z);
            subBackScreen.transform.position = vSub;

        }



    }
}
