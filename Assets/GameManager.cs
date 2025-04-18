using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
public class GameManager : MonoBehaviour
{
    public Text ScoreTxt;
    int Score;
    public GameObject StartPanel;
    public GameObject GameOverPanel;
    public AudioSource audioSource;
    public AudioClip gameoverSfx;
    public AudioClip scoreSfx;
    public GameObject spawnEffect; // Assign the prefab for the spawned object in the Inspector

    [DllImport("__Internal")]
    private static extern void SendScore(int score, int game);
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        StartPanel.SetActive(true);
        GameOverPanel.SetActive(false);
    }

    public void StartGame()
    {
        StartPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Gameover(Vector2 position,GameObject obj)
    {
        obj.SetActive(false);
        audioSource.PlayOneShot(gameoverSfx);
        GameOverPanel.SetActive(true);
         // Spawn an effect at the collision point
            SpawnAtPoint(position);
        Debug.Log("GameOver");
        SendScore(Score, 53);
    }

    private void SpawnAtPoint(Vector2 position)
    {
        // Instantiate the spawnEffect prefab at the collision point
        GameObject spawnedObject = Instantiate(spawnEffect, position, Quaternion.identity);

        // Destroy the spawned object after 1 second
        Destroy(spawnedObject, 1f);
    }

    public void AddScore()
    {
        audioSource.PlayOneShot(scoreSfx);
        Score++;
        ScoreTxt.text = Score.ToString();
    }
    public void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
