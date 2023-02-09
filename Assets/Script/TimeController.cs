using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public bool isCountDown = true;

    public float gameTime = 0f;
    public bool isTimeOver = false;// true �϶��� Ÿ������
    public float displayTime = 0f;  //ǥ�ýð�
    float curTime = 0f;  //���� �ð�;


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
            curTime += Time.deltaTime; //�÷��̽ð� ����

            Debug.Log("����ð� : "+curTime+"�����ð�" + displayTime);
            
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
