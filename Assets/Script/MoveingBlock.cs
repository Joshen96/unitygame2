using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveingBlock : MonoBehaviour
{
    public float move_X = 0.0f;  
    public float move_Y = 0.0f;
    public float times = 0.0f;
    public float wait_t = 0.0f; //정지시간

    public bool isMoveWhenOn = false; // 


    public bool isCanMove = true;

    float perDx; // 1프레임당 x 이동값;
    float perDy; // 1프레임당 y 이동값;

    Vector3 defpos; // 오브젝트 초기위치 
    bool isReverse = false;





    void Start()
    {
        defpos = this.transform.position; // 오브젝트 위치받기;
        //1프레임에 걸리는시간 == 이동시간
        float timeStep = Time.fixedDeltaTime;
        //1프레임의 x이동값

        perDx = move_X / (1.0f / timeStep * times);
        //1프레임의y이동값
        perDy = move_Y / (1.0f / timeStep * times);



    }


    private void FixedUpdate()
    {
        if (isCanMove)
        {
            float x = transform.position.x;
            float y = transform.position.y;
            bool endX = false;
            bool endY = false;
            
            if (isReverse)
            {

                if ((perDx >= 0.0f && x <= defpos.x + move_X) || perDx < 0.0f && x >= defpos.x + move_X)
                {
                    endX = true;
                }
                if ((perDy >= 0.0f && y <= defpos.y + move_Y) || perDy < 0.0f && y >= defpos.y + move_Y)
                {
                    endY = true;
                }


                Vector3 v = new Vector3(-perDx, -perDy, defpos.z);
                transform.Translate(v);
            }
            else
            {

                if ((perDx >= 0.0f && x >= defpos.x + move_X) || perDx < 0.0f && x <= defpos.x + move_X)
                {
                    endX = true;
                }
                if ((perDy >= 0.0f && y >= defpos.y + move_Y) || perDy < 0.0f && y <= defpos.y + move_Y)
                {
                    endY = true;
                }
                Vector3 v = new Vector3(perDx, perDy, defpos.z);
                transform.Translate(v);
            }
            
            


            if (endX && endY)
            {
                if (isReverse)
                {
                    transform.position = defpos;
                }
                isReverse = !isReverse;

                isCanMove = false;
                if (isMoveWhenOn == false)
                {
                    Invoke("Move", wait_t);
                }
            }

        }
    }
    private void Move()
    {
        isCanMove = true;
    }

    private void Stop()
    {
        isCanMove = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(this.transform);  //이오브젝트의 포지션을 충돌한 플레이어의 부모로세팅
            if (isMoveWhenOn)
            {
                isCanMove = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) //접촉이 떨어지면 실행
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null); //플레이어 포지션 값 부모(블록) 자식(플레이어) 해제 
        }
    }


    void Update()
    {
        
    }
}
