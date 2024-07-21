using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashVFX;


    private void Start() {
        Debug.Log("Collider script active");
    }
    void OnCollisionEnter(Collision other)
    {
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        //disbled player movement control
        GetComponent<Movement>().enabled = false;

        //Crash Effects
        crashVFX.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        //Other option for gameover scene - panel that color the screen red when crashed into something
        //gameOverPanel.SetActive(true);

        //wait for few seconds before reseting the current level
        StartCoroutine(ReloadLevel(loadDelay));
    }

    IEnumerator ReloadLevel(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

    }
}
