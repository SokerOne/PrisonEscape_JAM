using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int playerhealth;
    private int health;

    private int score = 0;
    public bool paused = false;

    public GameObject PauseMenu;
    public GameObject GameOverMenu;
    public Text scoreText;
    public Image[] liveBullets;
    public GameObject transitionBegin;
    public GameObject transitionEnd;
    public GameObject alarmobject;
    private AudioSource buttonClic;


    public bool GetPaused()
    {
        return paused;
    }


    public void addScore(int _score)
    {
        score += _score;
        scoreText.text = score.ToString();
    }

	public int GetScore()
	{
		return score;
	}

	public void damage(int _damage)
    {

        health -= _damage;
        alarmobject.SetActive(true);
        switch (health)
        {
            case 0:
                liveBullets[0].enabled = false;
                GameOver();
               
                break;
            case 1:
                liveBullets[1].enabled = false;
                break;
            case 2:
                liveBullets[2].enabled = false;
                break;
            case 3:
                liveBullets[3].enabled = false;
                break;
            default:
                break;
        }
    }

    public void botonVolver()
    {
        transitionEnd.SetActive(true);
    }

    public float GetHealth()
	{
        return health;
	}

	void Start()
    {
        Time.timeScale = 1;
        addScore(0);
        PauseMenu.SetActive(false);
        GameOverMenu.SetActive(false);
        transitionBegin.SetActive(true);
        buttonClic = gameObject.GetComponent<AudioSource>();
        transitionEnd.SetActive(false);
        health = 4;

        for (int i = 0; i < liveBullets.Length - 1; ++i)
        {
            liveBullets[i].enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(health > 0)
		{
			if (Input.GetKeyDown(KeyCode.Space))

			{
				if (!paused)
				{
					PauseMenu.SetActive(true);
                    buttonClic.PlayScheduled(0.4);
                    Time.timeScale = 0;
					paused = true;
				}
				else if (paused)
				{
					PauseMenu.SetActive(false);
                    buttonClic.PlayScheduled(0.4);
					Time.timeScale = 1;
					paused = false;
				}
			}
		}
    }

    void GameOver()
    {
        float maxscore = PlayerPrefs.GetFloat("score", 0.0f);
        if(score>maxscore)
        {
        PlayerPrefs.SetFloat("score", score);
        }

        Time.timeScale = 0;
        GameOverMenu.SetActive(true);
        paused = false;
    }
}



