using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllor : MonoBehaviour
{
    Animator animator;
    public string idleAni = "playeridle";
    public string moveAni = "playermove";
    public string jumpAni = "playerjump";
    public string overAni = "playerover";
    public string clearAni = "playerclear";
    // Start is called before the first frame update
    public float speed = 3.0f;
    Rigidbody2D player_rigi;
    //bool player_turn = false;
    float axisH = 0.0f; //이동

    //점프

    public float jump = 9.0f;
    public LayerMask groundLayer; //착지 레이어 판별 
    bool goJump = false;          //점프중 체크
    bool onGround = false;        //지면인지 체크 

    public static string gameState = "playing";
    

    
    

    string nowAni = "";
    string oldAni = "";

    void Start()
    {
        player_rigi = this.GetComponent<Rigidbody2D>();
        player_rigi.constraints = RigidbodyConstraints2D.FreezeRotation; //회전방지
        animator =GetComponent<Animator>();
        nowAni = idleAni;
        oldAni = idleAni;
        animator.Play(nowAni);
    }

    
    void Update()
    {

        
        axisH = Input.GetAxisRaw("Horizontal");
       
        if (axisH > 0.0f) //오른쪽이동
        {
            
            transform.localScale = new Vector2(1, 1); //플립대신 사용
        }
        else if(axisH < 0.0f) //왼쪽이동
        {
            
            transform.localScale = new Vector2(-1, 1); //x축 반전
        }

        //점프

        if (Input.GetButtonDown("Jump")/*||Input.GetKey("space")*/)
        {
            Jump();
        }

    }

    private void FixedUpdate()
    {
        if (gameState != "playing")
        {
            return;
        }

        //이동 점프 
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up *0.1f), groundLayer);  //땅인지판별
        //오브젝트의 0,0으로부터 라인을 쏴서 탐지 (0,-0.1)즉 아래로 탐지함
        if (onGround || axisH != 0) // 이동
        {
            player_rigi.velocity = new Vector2(axisH * speed, player_rigi.velocity.y);
        }
        if(onGround && goJump) //점프
        {
            Vector2 JumpPw = new Vector2(0, jump);
            player_rigi.AddForce(JumpPw, ForceMode2D.Impulse);
            goJump = false; //점프종료 체크
        }


        if (onGround)
        {
            Debug.Log("땅에서있음");
            if (axisH == 0)
            {
                nowAni = idleAni;
            }
            else
            {
                nowAni = moveAni;
            }
        }
        else
        {
            nowAni = jumpAni;
        }
        if (nowAni != oldAni)
        {
            oldAni = nowAni;
            animator.Play(nowAni);
        }
        animator.Play(nowAni);
    }





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            Goal();
        }
        if(collision.gameObject.tag =="Dead")
        {
            Dead();
        }
    }
    void Goal()
    {
        animator.Play(clearAni);
        gameState = "gameClear";
        GameStop();
    }
    
    void Dead()
    {
        animator.Play(overAni);
        gameState = "gameOver";
        GameStop();
        GetComponent<CapsuleCollider2D>().enabled = false; //충동감지못함
        player_rigi.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);

    }
    void GameStop()
    {
        player_rigi.velocity = new Vector2(0, 0);
    }
    void Jump()
    {
        goJump = true;

    }
}
