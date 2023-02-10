using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBlock : MonoBehaviour
{
    public float length = 0.0f;
    public bool isDelete = false; // �ٴڿ� ���̸� �����������


    bool isFall = false; // �ٴ� ����

    float fadeTime = 0.5f;  //������¿��� �ð�
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D blockrigi = GetComponent<Rigidbody2D>();
        blockrigi.bodyType = RigidbodyType2D.Static;

    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float d = Vector2.Distance(transform.position, player.transform.position);
            if (length >= d)
            {
                Rigidbody2D blockrigi = GetComponent<Rigidbody2D>();
                if(blockrigi.bodyType == RigidbodyType2D.Static)
                {
                    blockrigi.bodyType = RigidbodyType2D.Dynamic;
                }
            }
        }
        if (isFall)
        {
            //������������ ������� ����  ���̵�ƿ�
            fadeTime -= Time.deltaTime;
            Color blockcolor = GetComponent<SpriteRenderer>().color;
            blockcolor.a = fadeTime;

            GetComponent<SpriteRenderer>().color = blockcolor;
            if (fadeTime <= 0.0f)
            {
                Destroy(this.gameObject);
            }

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDelete)
        {
            isFall = true;
        }
    }

}
