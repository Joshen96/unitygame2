using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //ī�޶� ���� ���� �����ϱ� 
    public float leftLimit= 0.0f;
    public float rightLimit = 0.0f;
    public float topLimit = 0.0f;
    public float bottomLimit = 0.0f;
    public GameObject subBackScreen; 
    //ȭ������� ������ �״� �� ���� exŸ�Ӿ��� �̵�


    public bool isForceScrollX = false; //x�� ���� ��ũ�� �÷���
    public float forceScrollSpeedX = -0.5f; //1�ʰ� ������ x�� �Ÿ�
    public bool isForceScrollY = false; //y�� ���� ��ũ�� �÷���
    public float forceScrollSpeedY = 0.5f; //1�ʰ� ������ y�� �Ÿ�

    private void Start()
    {
        isForceScrollX = true;
    }
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player == null) //�˼��ڵ�
        {
            Debug.LogError("�÷��̾ ã��������");
            return; //����
        }
        float x = player.transform.position.x;  //�÷��̾� ������Ʈ�� ��ġ �Ѱܹޱ�
        float y = player.transform.position.y;
        float z = player.transform.position.z;

        if (isForceScrollX)
        {
            x = transform.position.x + (forceScrollSpeedX * Time.deltaTime); //1�ʴ� forceScrollSpeedX(+�� ������ -�� ����)��ŭ x������ �̵�
        }

        if (isForceScrollY)
        {
            x = transform.position.y + (forceScrollSpeedY * Time.deltaTime); //1�ʴ� forceScrollSpeedY(+�� ���� -�� �Ʒ�)��ŭ y������ �̵�
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

        Vector3 v3 = new Vector3(x, y, -10);  //�÷��̾� ���������� ������ x,y,z�ް�


        transform.position = v3; //ī�޶� ������ �ֱ�

        if(subBackScreen != null)
        {
            y = subBackScreen.transform.position.y;
            z = subBackScreen.transform.position.z;

            Vector3 vSub = new Vector3(x / 2.0f, y, z);
            subBackScreen.transform.position = vSub;

        }



    }
}
