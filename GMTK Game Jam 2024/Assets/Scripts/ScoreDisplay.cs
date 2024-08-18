using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class ScoreDisplay : MonoBehaviour
{
    public bool startGame = false;
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI highScore;
    public GameObject scoreCounter;
    public GameObject startScreen;
    public static ScoreDisplay instance;
    int currentScore;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        currentScore = ScoreManager.instance.score;
        SaveHighScore(ScoreManager.instance.score);
    }

    public void PlayerDeath(float delay)
    {
        PlayerMovement.player.enabled = false;
        Invoke("playerDeath", delay);
    }
    void playerDeath()
    {   
        //Reset Game Scene
        SceneManager.UnloadScene(0);
        SceneManager.LoadScene(0, LoadSceneMode.Additive);
        startGame = false;

        //Set Final Score Text
        finalScore.text = $"SCORE: {currentScore})";


        finalScore.gameObject.SetActive(currentScore > 0? true : false);
        scoreCounter.SetActive(false);
        startScreen.SetActive(true);
        
        SaveHighScore(currentScore);

        //Reset ScoreCounter
        ScoreManager.instance.score = 0;
        ScoreManager.instance.SetText();
    }
    private void Update()
    {
        currentScore = ScoreManager.instance.score;
        if (startGame)
        {
            scoreCounter.SetActive(true);
            finalScore.gameObject.SetActive(false);
            startScreen.SetActive(false);
            SpawnManager.instance.Spawn();
        }
    }

    private void SaveHighScore(int currentScore)
    {
        string fileName = Application.persistentDataPath + "/HighScore.txt";
        if (File.Exists(fileName))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(fileName, FileMode.Open);
            int oldHighScore = (int)formatter.Deserialize(stream);


            if (oldHighScore > currentScore)
            {
                highScore.text = $"HIGHSCORE: {oldHighScore}";
                stream.Close();

            }
            else
            {
                highScore.text = $"HIGHSCORE: {currentScore}";
                stream.Close();
                FileStream save = new FileStream(fileName, FileMode.Create);
                formatter.Serialize(save, currentScore);
                save.Close();


            }

        }
        else
        {
            if(currentScore < 0) { highScore.text = "HIGHSCORE: NONE"; return; }

            highScore.text = currentScore.ToString();
            FileStream save = new FileStream(fileName, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(save, currentScore);
            save.Close();

        }
    }
}
