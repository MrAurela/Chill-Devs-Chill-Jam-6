using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Image raccoonImageField;
    [SerializeField] Sprite raccoon;
    [SerializeField] float delay = 1;

    bool processRunning = false;

    public void StartGame()
    {
        if (!processRunning) StartCoroutine(StartGameProcess());
    }

    private IEnumerator StartGameProcess()
    {
        Debug.Log("Start Game Process");
        processRunning = true;
        raccoonImageField.sprite = raccoon;
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(FindObjectOfType<Fade>().FadeOut());
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
