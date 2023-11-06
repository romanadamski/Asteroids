using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI levelText;

    private Button _button;
    private uint _levelNumber;

    private void Awake()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(OnLevelButtonClick);
    }

    public void Init(uint levelNumber)
    {
        levelText.text = levelNumber.ToString();
        _levelNumber = levelNumber;
    }

    private void OnLevelButtonClick()
    {
        LevelSettingsManager.Instance.SetLevelNumber(_levelNumber);
    }
}
