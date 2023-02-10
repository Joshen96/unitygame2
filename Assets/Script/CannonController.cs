using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject objPrefab;

    public float deleyTime = 3.0f;
    public float fireSpeedX = -4.0f;

    public float fireSpeedY = 0.0f;

    public float length = 8.0f;

    GameObject player;  //플레이어받기위해
    GameObject gateObj;
    float passedTimes = 0.0f;


    void Start()
    {
        Transform tr = transform.Find("Gate");
        gateObj = tr.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        passedTimes += Time.deltaTime;

        if (CheckLength(player.transform.position))
        {
            if (passedTimes > deleyTime)
            {
                passedTimes = 0;

                Vector3 pos = new Vector3(gateObj.transform.position.x, gateObj.transform.position.y, gateObj.transform.position.z);

                GameObject objBullet = Instantiate(objPrefab, pos, Quaternion.identity);


                Rigidbody2D bulletrigi = objBullet.GetComponent<Rigidbody2D>();
                Vector2 v = new Vector2(fireSpeedX, fireSpeedY);

                bulletrigi.AddForce(v, ForceMode2D.Impulse);
            }
        }
    }

    bool CheckLength(Vector2 targetPos)
    {
        bool ret = false;

        float d = Vector2.Distance(transform.position, targetPos);
        if (length >= d)
        {
            ret = true;
        }
        return ret;
    }
}
