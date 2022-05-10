using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI highlevel;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject confirm;
    [SerializeField] Slider slider;

    [SerializeField] Image theFirstColor;
    [SerializeField] Image theSecondColor;
    [SerializeField] Image theThirdColor;
    [SerializeField] Image theFourthColor;

    // Start is called before the first frame update
    void Start()
    {
        gameOver.SetActive(false);
        confirm.SetActive(false);
    }


    public void SetSlider(float maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = maxValue;
    }

    public void UpdateSlider(float value)
    {
        slider.value = value;
    }

    public void UpdateLevel(int level)
    {
        this.level.text = level.ToString();
        highlevel.text = PlayerPrefs.GetInt("highestlevel").ToString();
    }

    public void UpdateColor(Color[] color)
    {
        theFirstColor.color = color[0];
        theSecondColor.color = color[1];
        theThirdColor.color = color[2];
        theFourthColor.color = color[3];
    }

    public void ChoosenColor(int index)
    {
        GamePlayController.Instance.ChoosenColor(index);
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }

    public void ShowConfirm(bool isShow)
    {
        Time.timeScale = (isShow)?0:1;
        confirm.SetActive(isShow);
    }

    public void StartNewGame()
    {
        GetComponent<SceneController>().StartGame();
    }

    public void BackToMenu()
    {
        GetComponent<SceneController>().BackToMenu();
    }
}
