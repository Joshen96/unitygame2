using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //ui사용시 
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // ui에 사용될 이미지와 버튼
    public GameObject mainImage; //게임스타트 게임클리어 게임오버를 담을 ui의 게임오브젝트 
    public Sprite gameOver;    // mainImage오브젝트 스프라이트에 담을 게임오버스프라이트
    public Sprite gameClear;   // mainImage오브젝트 스프라이트에 담을 게임클리어스프라이트
    public GameObject panel;
    public GameObject restartButton;
    public GameObject nextButton;
    public GameObject timeBar;
    public GameObject timeText;

    // 마지막 씬알기위함 
    public string lastScene;
    
    //
   
    string oldGameState;

    //타임컨트롤러
    TimeController timeCnt;


    public GameObject scoreText; //점수텍스트
    public static int totalScore; // 점수총합 스태틱변수
    public int stageScore = 0; //스테이지점수



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

        UpdateScore(); //실시간 점수업데이트

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
        Debug.Log("현재 상태"+PlayerControllor.gameState); //상태확인 용 콘솔 
        if (PlayerControllor.gameState == "gameClear") //게임클리어시
        {
            mainImage.SetActive(true);
            panel.SetActive(true);

            Button btReStart = restartButton.GetComponent<Button>();  //btReStart버튼에 컴포넌트추가
            btReStart.interactable = false; // 버튼 상호작용 매서드  인트렉터블 클리어시 재시작버튼 비활성
            mainImage.GetComponent<Image>().sprite = gameClear;
            PlayerControllor.gameState = "gameend";

            //시간

            if (timeCnt != null) //게임 클리어시 시간멈춤
            {
                timeCnt.isTimeOver = true;

                int time = (int)timeCnt.displayTime;
                totalScore += time * 10;  //멈춘시간 정산 토탈에
            }
            totalScore += stageScore;
            stageScore = 0;
            UpdateScore();
            if(SceneManager.GetActiveScene().name == lastScene) //다시 점수 하기위해

            {
                totalScore = 0;
            }
        }
        else if(PlayerControllor.gameState == "gameOver") //게임오버시
        {
            mainImage.SetActive(true);
            panel.SetActive(true);
            Button btNext = nextButton.GetComponent<Button>();
            btNext.interactable = false;  //게임 오버시 다음버튼이 못눌리게 비활성화
            mainImage.GetComponent<Image>().sprite = gameOver;
            PlayerControllor.gameState = "gameend";

            if (timeCnt != null)  //게임 오버시 시간멈춤
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
