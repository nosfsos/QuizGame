using System;
using System.Collections;
using System.Collections.Generic;
using Structs;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public List<QuestionViewModel> Questions;
    public int Score;

    public Timer Timer;

    public Text CurrentScoreText;
    public Text QuestionText;
    public Transform InstantiateParent;

    public Button ChooseAnswer;

    public int CurrentNumberOfQuestions;
    public int TotalNumberOfQuestions;

    private void Awake()
    {
        _defaultQuestionColor = QuestionText.color;
    }

    private void Start()
    {
        StartCoroutine(ManagerQuestions());
    }

    private Color _defaultQuestionColor;


    private IEnumerator ManagerQuestions()
    {
        while (CurrentNumberOfQuestions < TotalNumberOfQuestions)
        {
            QuestionText.color = _defaultQuestionColor;
            var currentQuestion = Questions[Random.Range(0, Questions.Count)].Question;

            // prepare question

            var models = Instantiate(currentQuestion.AnswerModels, InstantiateParent);
            QuestionText.text = currentQuestion.Text;
            Timer.ResetTimer();
            ChooseAnswer.interactable = true;

            // wait one frame to allow the objects to render
            yield return null;

            // gather the initial positions for the reset position functionality
            ResetPositions.Instance.GatherInitialPositions();

            // wait for answer
            while (Timer.Label.text != "0")
                yield return null;

            // get component gets executed just once per answer
            var selectedAnswer = SelectionManager.LastSelected.GetComponent<Answer>().Text;
            
            //evaluate answer
            var evaluation =
                currentQuestion.CorrectAnswer.Equals(selectedAnswer, StringComparison.InvariantCultureIgnoreCase);

            //Questions.RemoveAt(0);
            CurrentNumberOfQuestions++;

            Score += evaluation ? 10 : 0;
            CurrentScoreText.text = Score.ToString();

            yield return null;

            // give feedback according to answer

            QuestionText.text = evaluation ? "CORRECT" : "WRONG";
            QuestionText.color = evaluation ? Color.green : Color.red;
            yield return new WaitForSecondsRealtime(3);
            Destroy(models);
            SelectionManager.LastSelected = null;
        }

        QuestionText.color = _defaultQuestionColor;
        QuestionText.text = "Game Over. Score: " + Score;

        HighScoreManager.Instance.ShowAddHighScore(Score);
        MenuManager.Instance.ShowMenu(true);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(Scenes.MainMenu.ToString());
    }
}