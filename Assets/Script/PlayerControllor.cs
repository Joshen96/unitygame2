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
    float axisH = 0.0f; //�̵�

    //����

    public float jump = 9.0f;
    public LayerMask groundLayer; //���� ���̾� �Ǻ� 
    bool goJump = false;          //������ üũ
    bool onGround = false;        //�������� üũ 

    public static string gameState = "playing";
    

    
    

    string nowAni = "";
    string oldAni = "";

    void Start()
    {
        player_rigi = this.GetComponent<Rigidbody2D>();
        player_rigi.constraints = RigidbodyConstraints2D.FreezeRotation; //ȸ������
        animator =GetComponent<Animator>();
        nowAni = idleAni;
        oldAni = idleAni;
        animator.Play(nowAni);
    }

    
    void Update()
    {

        
        axisH = Input.GetAxisRaw("Horizontal");
       
        if (axisH > 0.0f) //�������̵�
        {
            
            transform.localScale = new Vector2(1, 1); //�ø���� ���
        }
        else if(axisH < 0.0f) //�����̵�
        {
            
            transform.localScale = new Vector2(-1, 1); //x�� ����
        }

        //����

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

        //�̵� ���� 
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up *0.1f), groundLayer);  //�������Ǻ�
        //������Ʈ�� 0,0���κ��� ������ ���� Ž�� (0,-0.1)�� �Ʒ��� Ž����
        if (onGround || axisH != 0) // �̵�
        {
            player_rigi.velocity = new Vector2(axisH * speed, player_rigi.velocity.y);
        }
        if(onGround && goJump) //����
        {
            Vector2 JumpPw = new Vector2(0, jump);
            player_rigi.AddForce(JumpPw, ForceMode2D.Impulse);
            goJump = false; //�������� üũ
        }


        if (onGround)
        {
            Debug.Log("����������");
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
        GetComponent<CapsuleCollider2D>().enabled = false; //�浿��������
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
