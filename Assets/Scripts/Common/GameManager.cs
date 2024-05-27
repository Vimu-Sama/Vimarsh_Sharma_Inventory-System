using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool startTrackingBossFight = false;
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
        // this initiates the check, the bool is changed when
        // the player enters the trigger area for final boss fight
        if (startTrackingBossFight)
            CheckIfWonGame();
    }

    //when game's over
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
        //this loop checks if the main three enemy bosses are killed or not
        for (int i = 0; i < bossEnemyList.Length; i++)
        {
            if (bossEnemyList[i] != null)
            {
                return;
            }
        }
        Time.timeScale = 0f;
        gameWinPanel.SetActive(true);
        gameManagerAudioSource.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        //tracks the trigger for boss fight and make changes to the vibe accordingly
        if (other.gameObject.tag == "Player")
        {
            startTrackingBossFight = true;
            gameManagerAudioSource.clip = bossFightBackGroundScore;
            gameManagerAudioSource.Play();
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    //de-initializing
    private void OnDestroy()
    {
        PlayerMovement.GameOver -= GameOverFunction;
    }


}
