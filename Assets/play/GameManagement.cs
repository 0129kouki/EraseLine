using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    //スコア関連
    public Text ScoreText;
    private int score;

    public int currentScore;
    //クリア条件スコア
    public int clearScore = 1500;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //ゲーム開始前の状態に戻す
    private void Initialize()
    {
        //スコアを0に戻す
        score = 0;
    }
    //スコアの追加
    public void AddScore()
    {
        score += 100;
        currentScore += score;
        ScoreText.text = "Score:" + currentScore.ToString();

        Debug.Log("currentScore");
        //ゲームクリア
        if (currentScore >= clearScore)
        {
            GameClear();
        }
    }
    //ゲームオーバー処理
    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //ゲームクリア処理
    public void GameClear()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}