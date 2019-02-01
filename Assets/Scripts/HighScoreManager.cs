using System;
using System.IO;
using Structs;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    private HighScore _highScore;
    private readonly string _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                                    "/QuizGame/highScore.json";
    private readonly string _directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                                         "/QuizGame";

    [Header("Single HighScore Prefabs")] 
    public Transform HighScoreParent;
    public GameObject HighScorePrefab;

    [Header("Add new high score")]
    public GameObject HighScorePanel;
    public GameObject AddHighScorePanel;

    public InputField NewHighScoreName;
    public Text NewHighScoreValue;

    public static HighScoreManager Instance;
    private static bool _alreadyInstantiated;

    private void Awake()
    {
        if (_alreadyInstantiated)
        {
            Destroy(gameObject);
            return;
        }

        _alreadyInstantiated = true;
        Instance = this;
    }

    private void Start()
    {
        LoadHighScore();
    }


    public void LoadHighScore()
    {
        _highScore = (File.Exists(_path)  ? JsonUtility.FromJson<HighScore>(File.ReadAllText(_path)) : new HighScore()) ??
                     new HighScore();
    }

    public void SaveHighScore()
    {
        var json = JsonUtility.ToJson(_highScore);
        if(File.Exists(_path))
            File.Delete(_path);

        if (Directory.Exists(_directory) == false)
            Directory.CreateDirectory(_directory);

        File.Create(_path).Close();
        File.AppendAllText(_path, json);
    }

    public void ShowAddHighScore(int score)
    {
        AddHighScorePanel.SetActive(true);
        NewHighScoreValue.text = score.ToString();
    }

    /// <summary>
    /// called from gui
    /// </summary>
    public void AddHighScore()
    {
        _highScore.AddScore(NewHighScoreName.text, NewHighScoreValue.text);
        SaveHighScore();

        DisplayHighScore();
        AddHighScorePanel.SetActive(false);
    }

    /// <summary>
    /// display top n scores. default is 100
    /// </summary>
    /// <param name="scores"></param>
    public void DisplayHighScore(int n = 100)
    {
        LoadHighScore();
        HighScorePanel.SetActive(true);
        var top = _highScore.GetTopScores(n);

        HighScoreParent.DestroyAllChildren();
        foreach (var score in top)
        {
            var newHighScore = Instantiate(HighScorePrefab, HighScoreParent, true);
            newHighScore.transform.GetChild(0).GetComponent<Text>().text = score.Name;
            newHighScore.transform.GetChild(1).GetComponent<Text>().text = score.Value;
        }
    }

    /// <summary>
    /// called from gui
    /// </summary>
    public void ShowHighScore()
    {
        DisplayHighScore();
    }
}