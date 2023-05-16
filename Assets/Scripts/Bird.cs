using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    private Rigidbody2D rigibody;
    public float jumpForce;
    private bool levelStart;
    public GameObject gameController;
    private int score;
    public Text scoreText;
    public GameObject imageToDestroy;

    public Text highScoreText;
    private int highScore = 0;
    private int playerPrefsHighScore;
    public bool isPaused = false;
    public GameObject pauseIcon;
    


    // Start is called before the first frame update
    private void Awake()
    {
        rigibody = this.gameObject.GetComponent<Rigidbody2D>();
        levelStart = false;
        rigibody.gravityScale = 0;
        score = 0;
        scoreText.text = score.ToString();

        playerPrefsHighScore = PlayerPrefs.GetInt("HighScore", 0);
        highScore = playerPrefsHighScore;
     


    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            Destroy(imageToDestroy);
            SoundController.instance.PlayThisSound("wing", 0.5f);
            if (levelStart == false)
            {
                levelStart = true;
                rigibody.gravityScale = 6;
                gameController.GetComponent<PipeMuti>().enableGenratePipe = true;

            }


            BirdMoveUp();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused)
            { 
                
                isPaused = true;
                Time.timeScale = 0; // dừng tất cả các hoạt động của game



            }
            else
            {
                isPaused = false;
                Time.timeScale = 1; // tiếp tục hoạt động của game
               

            }
        }

        if (!isPaused)
        {
            // các hoạt động của game sẽ tiếp tục khi biến isPaused bằng false
        }
    }
    private void BirdMoveUp()
    {
        rigibody.velocity = Vector2.up * jumpForce;
    }
    private IEnumerator OnCollisionEnter2D(Collision2D collision)

    {
        if (collision.gameObject.CompareTag("Column"))
        {
           SoundController.instance.PlayThisSound("hit", 0.5f);
           yield return new WaitForSeconds(0.4f);

        }

        reloadScene();
        score = 0;
        scoreText.text = score.ToString();
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ScoreZone"))
        {
            Destroy(collision.gameObject, 1f);
            score += 1;
            scoreText.text = score.ToString();
            SoundController.instance.PlayThisSound("point", 0.5f);
        }

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }


    }

    public void reloadScene()
    {
        SceneManager.LoadScene("gameplay");
    }
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore.ToString();
    }


}
