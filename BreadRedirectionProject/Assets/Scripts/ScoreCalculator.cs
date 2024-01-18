using UnityEngine;
using TMPro;

public class ScoreCalculator : MonoBehaviour
{
    public PrefabManager prefabManager;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI BreadNumText;

    [SerializeField] int breadNum;

    private void Update()
    {
        int score = CalculateScore();
        moneyText.text = $"お金：{score}円";

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
