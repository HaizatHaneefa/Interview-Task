using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private TextMeshProUGUI textDialogue;

    [SerializeField] private string dialogue;

    [SerializeField] private float typingSpeed;

    [SerializeField] public Image chatBackground, endBackground, startBackground, enemyHealthBar, playerHealthBar;

    void Start()
    {
        Time.timeScale = 0f;

        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        enemyStats = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyStats>();

        chatBackground.gameObject.SetActive(false);
        startBackground.gameObject.SetActive(true);
        endBackground.gameObject.SetActive(false);
    }


    public void _StartGame()
    {
        Time.timeScale = 1f;

        startBackground.gameObject.SetActive(false);

    }
    public void _PopChat()
    {
        Time.timeScale = 0;
        textDialogue.text = "";
        chatBackground.gameObject.SetActive(true);

        StartCoroutine(TypeEffect());
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        endBackground.gameObject.SetActive(true);
    }

    public void _CloseChat()
    {
        Time.timeScale = 1;
        chatBackground.gameObject.SetActive(false);
    }

    IEnumerator TypeEffect()
    {
        foreach (char letter in dialogue.ToCharArray())
        {
            textDialogue.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }

    private void Update()
    {
        Health();
    }

    void Health()
    {
        playerHealthBar.fillAmount = Mathf.Clamp(playerStats._playerHealth / 100, 0, 1f);
        enemyHealthBar.fillAmount = Mathf.Clamp(enemyStats._enemyHealth / 100, 0, 1f);
    }

    public void _Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void _ExitGame()
    {
        Application.Quit();
    }
}
