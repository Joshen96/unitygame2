using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBlock : MonoBehaviour
{
    public float length = 0.0f;
    public bool isDelete = false; // 바닥에 닿이면 사라질것인지


    bool isFall = false; // 바닥 감지

    float fadeTime = 0.5f;  //사라지는연출 시간
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
            //떨어짐감지후 사라지는 연출  페이드아웃
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
