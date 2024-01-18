using UnityEngine;
using TMPro;

public class ScoreCalculator : MonoBehaviour
{
    public PrefabManager prefabManager;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI BreadNumText;
    public int breadNum = 0;

    public int scoreMoney = 0;

    private void Update()
    {
        scoreMoney = CalculateScore();
        moneyText.text = $"お金：{scoreMoney}円";

        breadNum = prefabManager.correctCount+prefabManager.incorrectCount;

        BreadNumText.text = $"もらえるパン：{breadNum}つ";
    }

    private int CalculateScore()
    {
        int correctScore = prefabManager.correctCount * 50;
        int incorrectScore = prefabManager.incorrectCount * -100;
        return correctScore + incorrectScore;
    }
}
