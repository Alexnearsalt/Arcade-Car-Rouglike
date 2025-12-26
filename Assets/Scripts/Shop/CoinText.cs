using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private void Awake()
    {
        if (text == null)
            text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        var progress = ProgressController.Instance;
        if (progress == null)
            return;

        UpdateText(progress.Model.Coins);

        progress.Model.OnCoinsChanged += UpdateText;
    }

    private void OnDisable()
    {
        var progress = ProgressController.Instance;
        if (progress == null)
            return;

        progress.Model.OnCoinsChanged -= UpdateText;
    }

    private void UpdateText(int coins)
    {
        text.text = coins.ToString();
    }
}