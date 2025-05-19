using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instense;

    [SerializeField]
    Transform player;
    [SerializeField]
    GameObject rock;
    [SerializeField]
    TextMeshProUGUI txt_score;
    [SerializeField]
    Image[] lifes;
    [SerializeField]
    GameObject gameOverPanel;
    Queue<GameObject> rocks = new Queue<GameObject>();

    int score;

    private void Awake()
    {
        instense = this;
    }

    private void Start()
    {
        StartCoroutine(SpawnObstacles(50));
    }

    public void Score()
    {
        score++;
        RefreshScore();
    }

    void RefreshScore()
    {
        txt_score.text = "Score:" + score;
    }

    IEnumerator SpawnObstacles(int pos)
    {
        GameObject tempRock;
        if (rocks.Count <= 10)
        {
            tempRock = Instantiate(rock);
        }
        else
        {
            tempRock = rocks.Dequeue();
        }

        tempRock.transform.position = new Vector2(pos, -2);

        rocks.Enqueue(tempRock);

        if (rocks.Count > 10)
            yield return new WaitUntil(()=>player.position.x > tempRock.transform.position.x);

        StartCoroutine(SpawnObstacles(pos+Random.Range(20,50)));
    }

    public void RockHit()
    {
        for(int i = lifes.Length-1; i >=0 ; i--)
        {
            if (lifes[i].color.a == 1)
            {
                Color tempColor=lifes[i].color;
                tempColor.a = 0.5f;
                lifes[i].color = tempColor;
                if (i == 0)
                    GameOver();
                break;
            }
        }
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
