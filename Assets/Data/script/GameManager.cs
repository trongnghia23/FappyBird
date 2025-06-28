using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : NghiaMono
{
    protected static GameManager instance;
    public static GameManager Instance { get => instance; }
    [SerializeField] protected Animator animator;
    public Player player;
    public TMP_Text scoretext;
    public GameObject HignScoreBoard;
    public GameObject playbutton;
    public GameObject gameOver;
    public GameObject Starter;
    public int Score;
    public GameState state;

    [SerializeField] private GameObject scoreItemPrefab;   // Prefab chứa TMP_Text
    [SerializeField] private Transform scoreListContainer; // Content của ScrollView hoặc Panel chứa danh sách
    protected override void Awake()
    {
        base.Awake();
        if (GameManager.instance != null) Debug.LogError("only one GameManager allow to exist");
        GameManager.instance = this;
        Application.targetFrameRate = 60;
        this.Wait();
    }
    public virtual void Play()
    {
        SoundManager.Instance.PlayUInoise();
        state = GameState.Playing;
        animator.SetBool("IsPlaying", true);
        Score = 0;
        scoretext.text = Score.ToString();
        gameOver.SetActive(false);
        playbutton.SetActive(false);
        Starter.SetActive(false);
        HignScoreBoard.SetActive(false);
        Time.timeScale = 1;
        player.enabled = true;
        PipeCtr[] pipes = FindObjectsOfType<PipeCtr>();
        foreach (PipeCtr pipe in pipes)
        {
            PipeDespawn despawn = pipe.GetComponentInChildren<PipeDespawn>();
            if (despawn != null)
            {
                despawn.DespawnObject();
            }
        }
    }
    public virtual void Wait()
    {
        state = GameState.Waiting;
        ShowHighScore();
    }
    protected virtual void Pause()
    {
        state = GameState.Pause;
        Time.timeScale = 0;
        player.enabled = false;
    }
    public virtual void IncreaseScore()
    {
        SoundManager.Instance.PlayScorenoise();
        Score++;
        scoretext.text = Score.ToString();
    }
    public virtual void GameOver()
    {
        SoundManager.Instance.PlayDespawNoise();
        gameOver.SetActive(true);
        playbutton.SetActive(true);
        this.Pause();
        HighScoreManager.Instance.TryAddNewScore(Score);
        ShowHighScore();
    }

    public void ShowHighScore()
    {
        if (scoreItemPrefab == null)
        {
            Debug.LogError("Missing scoreItemPrefab reference!");
            return;
        }

        foreach (Transform child in scoreListContainer)
        {
            Destroy(child.gameObject);
        }

        List<int> scores = HighScoreManager.Instance.HighScores;
        for (int i = 0; i < scores.Count; i++)
        {
            GameObject item = Instantiate(scoreItemPrefab, scoreListContainer);
            item.SetActive(true); // đảm bảo được bật
            var text = item.GetComponentInChildren<TextMeshProUGUI>();
            item.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
            text.text = $"{i + 1}. {scores[i]}";
        }

        HignScoreBoard.SetActive(true);
    }



}
public enum GameState
{
    Playing,
    Pause,
    Waiting
}