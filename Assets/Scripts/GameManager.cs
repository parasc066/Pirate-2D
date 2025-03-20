using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text scoreText, coinText, diamondText;
    int totalCoins, totalDiamonds, score;

    public GameObject pannel;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        TempScore();
        SetText();
    }//start

    public void SetText()
    {
        totalCoins = PlayerPrefs.GetInt("PlayerCoins");
        score = PlayerPrefs.GetInt("PlayerScore");
        totalDiamonds = PlayerPrefs.GetInt("PlayerDiamonds");

        if (totalDiamonds <= 99999)
        {
            diamondText.text = totalDiamonds.ToString("D5");
        }
        else
        {
            diamondText.text = totalDiamonds.ToString();
        }
        scoreText.text = score.ToString();
        coinText.text = totalCoins.ToString();
    }

    public void TempScore()
    {
        scoreText.text = "500";
    }

    public void IncreaseDiamond()
    {
        int tempDiamond = totalDiamonds + 5;
        PlayerPrefs.SetInt("PlayerDiamonds", tempDiamond);
        SetText();

    }//increasecoin

    public void IncreaseCoin()
    {
        int tempCoin = totalCoins + 5;
        PlayerPrefs.SetInt("PlayerCoins", tempCoin);
        SetText();

    }//increasediamond

    public void GameOver()
    {
        pannel.gameObject.SetActive(true);
    }//gameover


}
