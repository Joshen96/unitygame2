using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    public bool die = false;
    public int killpoint = 100;

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.tag == "Player")
        {
            gameObject.layer = 8;
            GetComponent<BoxCollider2D>().enabled = false;
            die = true;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerControllor>().score=killpoint;
            

}
    }
}
