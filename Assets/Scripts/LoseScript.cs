using TMPro;
using UnityEngine;

public class LoseScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI totalScoreText;

    void Start()
    {
        if (totalScoreText != null)
        {
            totalScoreText.text = "Total Score: " + GameData.totalScore;
        }
    }
}
