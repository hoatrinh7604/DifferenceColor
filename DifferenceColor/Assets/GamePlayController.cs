using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    private int currentLevel;
    private int highLevel;
    [SerializeField] float timeInALevel;

    private float time;
    private UIController uIController;
    private int theRightIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        uIController = GetComponent<UIController>();

        Reset();
        StartNewTurn();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time < 0)
        {
            GameOver();
        }

        UpdateSlider();
    }

    public void StartNewTurn()
    {
        currentLevel++;

        if(highLevel < currentLevel)
        {
            highLevel = currentLevel;
            PlayerPrefs.SetInt("highestlevel", highLevel);
        }

        HandleSpawColor();
        UpdateUI();
    }

    public void HandleSpawColor()
    {
        Color[] randomColor = new Color[4];

        float R = Random.Range(0f, 1f);
        float G = Random.Range(0f, 1f);
        float B = Random.Range(0f, 1f);

        float A1, A2;
        A1 = Random.Range(6, 11);
        A2 = Random.Range(6, 11);
        while(A1 == A2)
        {
            A2 = Random.Range(6, 11);
        }

        Color rightColor = new Color(R, G, B, A1/10);
        Color otherColor = new Color(R, G, B, A2/10);

        theRightIndex = Random.Range(0, 4);
        randomColor[theRightIndex] = rightColor;

        for(int i = 0; i < randomColor.Length; i++)
        {
            if (theRightIndex == i) continue;
            randomColor[i] = otherColor;
        }

        uIController.UpdateColor(randomColor);
    }

    public void ChoosenColor(int index)
    {
        if(theRightIndex == index)
        {
            //Start next level
            StartNewTurn();
        }
        else
        {
            //Game over
            GameOver();
        }
    }

    public void Reset()
    {
        Time.timeScale = 1;
        currentLevel = 0;
        highLevel = PlayerPrefs.GetInt("highestlevel");
        time = timeInALevel;
        UpdateUI();
    }

    public void UpdateUI()
    {
        time = timeInALevel;
        uIController.SetSlider(timeInALevel);
        uIController.UpdateLevel(currentLevel);
    }

    public void UpdateSlider()
    {
        uIController.UpdateSlider(time);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        uIController.ShowGameOver();
    }
}
