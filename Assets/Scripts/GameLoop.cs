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
    bool ended;

    public Transform[] tanks = new Transform[2];

    private void Start()
    {
        greenText.text = greenPoints.ToString();
        redText.text = redPoints.ToString();
        Time.timeScale = 1f;
        tanks = GameObject.Find("GameManagement").GetComponent<PlacingObjects>().PlaceTanks();
        ended = false;
    }

    void Update()
    {
        if (!tanks[0].gameObject.activeInHierarchy && tanks[1] is not null)
        {
            if (!ended)
            {
                redPoints++;
                redText.text = redPoints.ToString();
                ended = true;
            }
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0f, Time.deltaTime);
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
            if (Time.timeScale < 0.3f) SceneManager.LoadScene("Main");
        }
    }

    public static IEnumerator SlowTime()
    {
        Debug.Log(Time.timeScale);
        while (Time.timeScale > 0.1f)
        {
            Debug.Log(Time.timeScale);
            Time.timeScale -= 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        SceneManager.LoadScene("Main");
    }
}
