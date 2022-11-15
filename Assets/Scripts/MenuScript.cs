using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    void Start()
    {
        GameLoop.redPoints = 0;
        GameLoop.greenPoints = 0;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
}
