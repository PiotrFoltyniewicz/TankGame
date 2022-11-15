using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameLoop : MonoBehaviour
{
    public static int greenPoints;
    public static int redPoints;
    public TextMeshProUGUI greenText;
    public TextMeshProUGUI redText;
    public TextMeshProUGUI countdown;
    bool ended;
    float countdownTime;
    public static bool started;

    public Transform[] tanks = new Transform[2];

    private void Start()
    {
        greenText.text = greenPoints.ToString();
        redText.text = redPoints.ToString();
        Time.timeScale = 1f;
        tanks = GameObject.Find("GameManagement").GetComponent<PlacingObjects>().PlaceTanks();
        ended = false;
        started = false;
        countdownTime = 3;
    }

    void Update()
    {
        countdownTime -= Time.deltaTime;
        countdown.text = Mathf.Ceil(countdownTime).ToString();
        if(countdownTime < 0 && !started)
        {
            countdown.gameObject.SetActive(false);
            started = true;
        }
        if (!tanks[0].gameObject.activeInHierarchy && tanks[1] is not null)
        {
            if (!ended)
            {
                redPoints++;
                redText.text = redPoints.ToString();
                ended = true;
            }
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0f, Time.deltaTime);
            started = false;
            if (Time.timeScale < 0.3f) SceneManager.LoadScene("Main");
        }
        else if (!tanks[1].gameObject.activeInHierarchy && tanks[0] is not null)
        {
            if (!ended)
            {
                greenPoints++;
                greenText.text = greenPoints.ToString();
                ended = true;
            }
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0f, Time.deltaTime);
            started = false;
            if (Time.timeScale < 0.3f) SceneManager.LoadScene("Main");
        }
    }
}
