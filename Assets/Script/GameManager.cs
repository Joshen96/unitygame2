using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //ui���� 
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
        Debug.Log("���� ����"+PlayerControllor.gameState); //����Ȯ�� �� �ܼ� 
        if (PlayerControllor.gameState == "gameClear") //����Ŭ�����
        {
            mainImage.SetActive(true);
            panel.SetActive(true);

            Button btReStart = restartButton.GetComponent<Button>();  //btReStart��ư�� ������Ʈ�߰�
            btReStart.interactable = false; // ��ư ��ȣ�ۿ� �ż���  ��Ʈ���ͺ� Ŭ����� ����۹�ư ��Ȱ��
            mainImage.GetComponent<Image>().sprite = gameClear;
            PlayerControllor.gameState = "gameend";
        }
        else if(PlayerControllor.gameState == "gameOver") //���ӿ�����
        {
            mainImage.SetActive(true);
            panel.SetActive(true);
            Button btNext = nextButton.GetComponent<Button>();
            btNext.interactable = false;  //���� ������ ������ư�� �������� ��Ȱ��ȭ
            mainImage.GetComponent<Image>().sprite = gameOver;
            PlayerControllor.gameState = "gameend";

        }
        else if(PlayerControllor.gameState == "playing")
        {

        }
    }
}
