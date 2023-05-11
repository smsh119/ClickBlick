using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Box related
    [SerializeField] float speed = 10f;
    //[SerializeField] float xDir = 1f;
    //[SerializeField] float yDir = 1f;
    [SerializeField] float noEffectRadius = 2f;
    [SerializeField] int score = 0;
    [SerializeField] float spawnWaitTime = 1f;
    [SerializeField] float waitTimeReduceFactor = 0.1f;
    [SerializeField] float speedIncreaseFactor = 5f;
    [SerializeField] float difficultyTransitionTime = 5f;


    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI TimerText;
    [SerializeField] GameObject boxPrefab;
    [SerializeField] SpriteRenderer noEffectRadiusSprite;

    //items of gameover panel
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI gameOverScore;
    [SerializeField] Button restartButton;
    [SerializeField] Button quitButton;

    bool spawnBoxes = true;
    
    void Start()
    {
        scoreText.text = score.ToString();
        TimerText.text = "0";
        noEffectRadiusSprite.transform.localScale = new Vector2(noEffectRadius*2f, noEffectRadius*2f);
        gameOverPanel.SetActive(false);
        StartCoroutine(SpawnBox());
         
    }

    void Update()
    {
        if (spawnBoxes)
        {
            TimerText.text = Time.timeSinceLevelLoad.ToString("F2");
            ProcessDifficulty();
        }
    }

    private void ProcessDifficulty()
    {
        float chckTime = difficultyTransitionTime;
        if (Time.time> chckTime)
        {
            difficultyTransitionTime += Time.time;
            spawnWaitTime -= waitTimeReduceFactor;
            speed += speedIncreaseFactor;
        }
    }

    IEnumerator SpawnBox()
    {
       
        while (true)
        {
            if (spawnBoxes)
            {
                Instantiate(boxPrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(spawnWaitTime);
            }
            else yield return null;
        }
        
    }


    public void ProcessGameOver()
    {
        spawnBoxes = false;
        BoxScript[] bx = FindObjectsOfType<BoxScript>();
        foreach(BoxScript b in bx)
        {
            Destroy(b.gameObject);
        }
        scoreText.gameObject.SetActive(false);
        TimerText.gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = false;
        noEffectRadiusSprite.gameObject.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
        gameOverScore.text = "Score : "+score.ToString();
        gameOverPanel.SetActive(true);

    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
        Application.Quit();
    }


    public float getSpeed()
    {
        return speed;
    }
    /*public float getXDir()
    {
        return xDir;
    }
    public float getYDir()
    {
        return yDir;
    }*/
    public float getNoEffectRadius()
    {
        return noEffectRadius;
    }
    



    public void setScore(int sc)
    {
        score += sc;
        scoreText.text = score.ToString();
    }

}
