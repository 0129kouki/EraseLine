using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    //�X�R�A�֘A
    public Text ScoreText;
    private int score;

    public int currentScore;
    //�N���A�����X�R�A
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

    //�Q�[���J�n�O�̏�Ԃɖ߂�
    private void Initialize()
    {
        //�X�R�A��0�ɖ߂�
        score = 0;
    }
    //�X�R�A�̒ǉ�
    public void AddScore()
    {
        score += 100;
        currentScore += score;
        ScoreText.text = "Score:" + currentScore.ToString();

        Debug.Log("currentScore");
        //�Q�[���N���A
        if (currentScore >= clearScore)
        {
            GameClear();
        }
    }
    //�Q�[���I�[�o�[����
    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //�Q�[���N���A����
    public void GameClear()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}