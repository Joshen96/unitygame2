using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveingBlock : MonoBehaviour
{
    public float move_X = 0.0f;  
    public float move_Y = 0.0f;
    public float times = 0.0f;
    public float wait_t = 0.0f; //�����ð�

    public bool isMoveWhenOn = false; // 


    public bool isCanMove = true;

    float perDx; // 1�����Ӵ� x �̵���;
    float perDy; // 1�����Ӵ� y �̵���;

    Vector3 defpos; // ������Ʈ �ʱ���ġ 
    bool isReverse = false;





    void Start()
    {
        defpos = this.transform.position; // ������Ʈ ��ġ�ޱ�;
        //1�����ӿ� �ɸ��½ð� == �̵��ð�
        float timeStep = Time.fixedDeltaTime;
        //1�������� x�̵���

        perDx = move_X / (1.0f / timeStep * times);
        //1��������y�̵���
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
            collision.transform.SetParent(this.transform);  //�̿�����Ʈ�� �������� �浹�� �÷��̾��� �θ�μ���
            if (isMoveWhenOn)
            {
                isCanMove = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) //������ �������� ����
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null); //�÷��̾� ������ �� �θ�(���) �ڽ�(�÷��̾�) ���� 
        }
    }


    void Update()
    {
        
    }
}
