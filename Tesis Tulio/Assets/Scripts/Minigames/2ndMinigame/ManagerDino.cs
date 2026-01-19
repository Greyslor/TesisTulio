using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerDino : MonoBehaviour
{
    public GameObject panel;
    private AudioSource deadSound;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        deadSound = GetComponent<AudioSource>();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        panel.SetActive(true);

        deadSound.Play();

    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
