using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public bool isCountDown = true;

    public float gameTime = 0f;
    public bool isTimeOver = false;// true 일때는 타임정지
    public float displayTime = 0f;  //표시시간
    float curTime = 0f;  //현재 시간;


    void Start()
    {
        if (isCountDown)
        {
            displayTime = gameTime;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(isTimeOver == false)
        {
            curTime += Time.deltaTime; //플레이시간 누적

            Debug.Log("현재시간 : "+curTime+"남은시간" + displayTime);
            
            if (isCountDown)
            {
                displayTime = gameTime - curTime;
                if (displayTime <= 0.0f)
                {
                    displayTime = 0.0f;
                    isTimeOver = true;
                }
            }
            else
            {
                displayTime = curTime;
                if (displayTime >= gameTime)
                {
                    displayTime = gameTime;
                    isTimeOver = true;
                }
                
            }
            
        }

    }
}
