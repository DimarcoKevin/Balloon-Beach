using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class App_Initialize : MonoBehaviour
{

    public GameObject inMenuUI;
    public GameObject inGameUI;
    public GameObject gameOverUI;
    public GameObject adButton;
    public GameObject restartButton;
    public GameObject player;
    private bool hasGameStarted = false;
    private bool hasSeenRewardedAd = false;
   
    void Awake()
    {
        Shader.SetGlobalFloat("_Curvature", 2.0f);
        Shader.SetGlobalFloat("_Trimming", 0.1f);
        Application.targetFrameRate = 60;
    }

    void Start() {
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        inMenuUI.gameObject.SetActive(true);
        inGameUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(false);

    }
    public void PlayButton() {
        if (hasGameStarted) {
            StartCoroutine(StartGame(1.0f));
        } else {
            StartCoroutine(StartGame(0.0f));
            hasGameStarted = true;
        }
        
    }

    public void PauseButton() {
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        inMenuUI.gameObject.SetActive(true);
        inGameUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(false);
    }

    public void GameOver() {
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        hasGameStarted = false;
        inMenuUI.gameObject.SetActive(false);
        inGameUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(true);
        restartButton.GetComponent<Animator>().enabled = false;

        if (hasSeenRewardedAd) {
            adButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            adButton.GetComponent<Button>().enabled = false;
            adButton.GetComponent<Animator>().enabled = false;
            restartButton.GetComponent<Animator>().enabled = true;
        }
    }
    public void RestartGame() {
        SceneManager.LoadScene(0); // loads scene at index 0 (re-runs main scene)
    }

    public void ShowAd() {
        if (Advertisement.IsReady("rewardedVideo")) {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result) {
        switch (result) {
            case ShowResult.Finished:
                Debug.Log("Ad was successfully shown.");
                hasSeenRewardedAd = true;
                StartCoroutine(StartGame(1.5f));
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before completion");
                break;
            case ShowResult.Failed:
                Debug.Log("The ad has failed");
                break;
        }
    }

    IEnumerator StartGame(float waitTime) {
        inMenuUI.gameObject.SetActive(false);
        inGameUI.gameObject.SetActive(true);
        gameOverUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
    }
    
}
