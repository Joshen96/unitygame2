using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int killpoint = 100;
    Animator animator;
    public string Dieani = "EnemyDie";


    public float speed = 3.0f;
    public bool die = false;
    public string direction = "left";
    public float range = 0.0f;
    Vector3 defPos;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        if (direction == "right")
        {
            transform.localScale = new Vector2(-1, 1);
        }
        defPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
       
        
        if (range > 0.0f)
        {
            if (transform.position.x < defPos.x - (range / 2))
            {
                direction = "right";
                transform.localScale = new Vector2(-1, 1);
            }
            if (transform.position.x > defPos.x + (range / 2))
            {
                direction = "left";
                transform.localScale = new Vector2(1, 1);
            }
        }
        

       
        
     
    }
    private void FixedUpdate()
    {
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();

        if (transform.GetChild(0).GetComponent<EnemyDie>().die==true)
        {
            animator.Play(Dieani);
            gameObject.layer =8;
            
            GetComponent<BoxCollider2D>().enabled = false;

            rbody.velocity = new Vector2(0, 0);

            Diemove();
            Destroy(gameObject, 1);
        }
        else
        {
        if (direction == "right")
        {
            rbody.velocity = new Vector2(speed, rbody.velocity.y);
        }
        else
        {
            rbody.velocity = new Vector2(-speed, rbody.velocity.y);
        }


        }
 
    }
    void Diemove()
    {
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (direction == "right")
        {
            direction = "left";
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            direction = "right";
            transform.localScale = new Vector2(-1, 1);
        }

        
    }

   
}
