using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public ScoreCalculator scoreCalculator;
    public GameObject gameOverUI;
    public GameObject restartButton;
    public TextMeshProUGUI GameResult;

    void Update()
    {
        if (scoreCalculator.scoreMoney < 0f)
        {
            GameOver();
        }
        GameResult.text =  $"獲得枚数：{scoreCalculator.breadNum}つ";
    }

    void GameOver()
    {
        Time.timeScale = 0; // ゲームを一時停止
        gameOverUI.SetActive(true);
        restartButton.SetActive(true); // リスタートボタンを表示
    }

    public void RestartGame()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene("InitGameScene");
        scoreCalculator.scoreMoney = 0;
    }
    public void StartGame(){
        Time.timeScale = 1; 
        SceneManager.LoadScene("MainGameScene");
    }
}
