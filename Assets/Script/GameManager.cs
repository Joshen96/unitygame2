using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //ui���� 
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // ui�� ���� �̹����� ��ư
    public GameObject mainImage; //���ӽ�ŸƮ ����Ŭ���� ���ӿ����� ���� ui�� ���ӿ�����Ʈ 
    public Sprite gameOver;    // mainImage������Ʈ ��������Ʈ�� ���� ���ӿ�����������Ʈ
    public Sprite gameClear;   // mainImage������Ʈ ��������Ʈ�� ���� ����Ŭ���������Ʈ
    public GameObject panel;
    public GameObject restartButton;
    public GameObject nextButton;
    public GameObject timeBar;
    public GameObject timeText;

    // ������ ���˱����� 
    public string lastScene;
    
    //
   
    string oldGameState;

    //Ÿ����Ʈ�ѷ�
    TimeController timeCnt;


    public GameObject scoreText; //�����ؽ�Ʈ
    public static int totalScore; // �������� ����ƽ����
    public int stageScore = 0; //������������



    void Start()
    {
        Invoke("InactiveImage", 1.0f); //

        panel.SetActive(false);

       // oldGameState = PlayerControllor.gameState;

        timeCnt = GetComponent<TimeController>();

        if(timeCnt != null)
        {
            if(timeCnt.gameTime == 0.0f)
            {
                timeBar.SetActive(false);
            }
        }

        UpdateScore(); //�ǽð� ����������Ʈ

    }
    void UpdateScore()
    {
        int score = stageScore + totalScore;
        scoreText.GetComponent<Text>().text = score.ToString();

    }
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
    
    void Update()
    {
        Debug.Log("���� ����"+PlayerControllor.gameState); //����Ȯ�� �� �ܼ� 
        if (PlayerControllor.gameState == "gameClear") //����Ŭ�����
        {
            mainImage.SetActive(true);
            panel.SetActive(true);

            Button btReStart = restartButton.GetComponent<Button>();  //btReStart��ư�� ������Ʈ�߰�
            btReStart.interactable = false; // ��ư ��ȣ�ۿ� �ż���  ��Ʈ���ͺ� Ŭ����� ����۹�ư ��Ȱ��
            mainImage.GetComponent<Image>().sprite = gameClear;
            PlayerControllor.gameState = "gameend";

            //�ð�

            if (timeCnt != null) //���� Ŭ����� �ð�����
            {
                timeCnt.isTimeOver = true;

                int time = (int)timeCnt.displayTime;
                totalScore += time * 10;  //����ð� ���� ��Ż��
            }
            totalScore += stageScore;
            stageScore = 0;
            UpdateScore();
            if(SceneManager.GetActiveScene().name == lastScene) //�ٽ� ���� �ϱ�����

            {
                totalScore = 0;
            }
        }
        else if(PlayerControllor.gameState == "gameOver") //���ӿ�����
        {
            mainImage.SetActive(true);
            panel.SetActive(true);
            Button btNext = nextButton.GetComponent<Button>();
            btNext.interactable = false;  //���� ������ ������ư�� �������� ��Ȱ��ȭ
            mainImage.GetComponent<Image>().sprite = gameOver;
            PlayerControllor.gameState = "gameend";

            if (timeCnt != null)  //���� ������ �ð�����
            {
                timeCnt.isTimeOver = true;
            }


        }
        else if(PlayerControllor.gameState == "playing")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerControllor playerControllor = player.GetComponent<PlayerControllor>();
            if(timeCnt != null)
            {
                if (timeCnt.gameTime > 0.0f)
                {
                    int time = (int)timeCnt.displayTime;
                    timeText.GetComponent<Text>().text = time.ToString();


                    if(time == 0)
                    {
                        playerControllor.GameStop();
                        
                        
                    }
                }
            }

            if(playerControllor.score != 0)
            {
                stageScore += playerControllor.score;
                playerControllor.score = 0;
                UpdateScore();
            }
        }
    }
}
