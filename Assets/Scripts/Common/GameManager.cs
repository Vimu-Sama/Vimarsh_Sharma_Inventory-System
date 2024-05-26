using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool startTrackingBossFight= false;
    private AudioSource gameManagerAudioSource;
    [SerializeField] private GameObject gameStartingPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameWinPanel;
    [SerializeField] private GameObject gamePauseButton;
    [SerializeField] private EnemyAI[] bossEnemyList;
    [SerializeField] private AudioClip gameNormalBackGroundScore;
    [SerializeField] private AudioClip bossFightBackGroundScore;
    private void Start()
    {
        gameStartingPanel.SetActive(true);
        PlayerMovement.GameOver += GameOverFunction;
        gameManagerAudioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        if (startTrackingBossFight)
            CheckIfWonGame();
    }

    private void GameOverFunction()
    {
        if (this.gameObject == null)
            return;
        StartCoroutine(GameOverMainFunction());
    }

    private IEnumerator GameOverMainFunction()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        gamePauseButton.SetActive(false);
        gameManagerAudioSource.Stop();
    }
    public void CheckIfWonGame()
    {
        for(int i=0;i<bossEnemyList.Length;i++)
        {
            if (bossEnemyList[i]!=null)
            {
                return;
            }
        }
        Time.timeScale = 0f;
        gameWinPanel.SetActive(true);
        gameManagerAudioSource.Stop();
    }

    private void OnDestroy()
    {
        PlayerMovement.GameOver -= GameOverFunction;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            startTrackingBossFight = true;
            gameManagerAudioSource.clip = bossFightBackGroundScore;
            gameManagerAudioSource.Play();
            GetComponent<BoxCollider>().enabled = false;
        }
    }

}
