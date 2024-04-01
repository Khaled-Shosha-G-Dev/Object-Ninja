using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> targets;
    private float spawnRate = 1.0f;
    //to condition to stop game if the player lose
    public bool isGameActive = true;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI chances;
    [SerializeField] private Button restartButton;
    private int scorePoints = 0;
    public int chanceNum = 3;


    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject volume;

    private AudioSource audioSource;
    [SerializeField] private AudioClip crashSound;

    [SerializeField] private GameObject gamePaused;
    private bool paused;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     
    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int randomIndex= Random.Range(0, targets.Count);
            Instantiate(targets[randomIndex]);

        }
    }
    public void UpdateScore(int  scoreToAdd)
    {
        audioSource.PlayOneShot(crashSound, 1.0f);
        scorePoints += scoreToAdd;
        scoreText.text = "Score : " + scorePoints;
    }
    public void UpdateChances()
    {
        if(isGameActive)
            chanceNum--;
        chances.text = "Chances : "+chanceNum;
    }
    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);    
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        chances.text = "Chances : " + 3;
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        UpdateScore(scorePoints);
        titleScreen.SetActive(false);
        volume.SetActive(false);
    }    

    public void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            gamePaused.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused=false;
            gamePaused.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
