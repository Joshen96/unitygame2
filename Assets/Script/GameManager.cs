using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //ui사용시 
public class GameManager : MonoBehaviour
{
    public GameObject mainImage;
    public Sprite gameOver;
    public Sprite gameClear;
    public GameObject panel;
    public GameObject restartButton;
    
    public GameObject nextButton;

    Image titleImage;

  
    void Start()
    {
        Invoke("InactiveImage", 1.0f); //

        panel.SetActive(false);
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
        }
        else if(PlayerControllor.gameState == "gameOver") //게임오버시
        {
            mainImage.SetActive(true);
            panel.SetActive(true);
            Button btNext = nextButton.GetComponent<Button>();
            btNext.interactable = false;  //게임 오버시 다음버튼이 못눌리게 비활성화
            mainImage.GetComponent<Image>().sprite = gameOver;
            PlayerControllor.gameState = "gameend";

        }
        else if(PlayerControllor.gameState == "playing")
        {

        }
    }
}
