using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChickenQuiz : MonoBehaviour
{
    public QuizData quizData;               // Reference to QuizData ScriptableObject
    public TextMeshProUGUI questionText;    // TextMeshPro for the question
    public TextMeshProUGUI[] choiceTexts;   // Array for choice texts (A, B, C)
    public TextMeshProUGUI feedbackText;    // TextMeshPro for feedback
    public Button[] choiceButtons;          // Array for choice buttons

    private int currentQuestionIndex = 0;
    private bool questionDisplayed = false; // Ensure only one question per day

    public bool testingMode = false;        // Enable this for testing mode

    private enum QuizState { Question, Feedback, Finished }
    private QuizState currentState = QuizState.Finished;

    // Correct and wrong answer feedback phrases
    private string[] correctPhrases =
    {
        "That's right! {0} is indeed {1}.",
        "Correct! {0} is definitely {1}.",
        "You got it! The answer to {0} is {1}.",
        "Well done! {0} is {1}.",
        "Absolutely! The right answer to {0} is {1}."
    };

    private string[] wrongPhrases =
    {
        "Oops, that's wrong. The correct answer to {0} is {1}.",
        "Not quite. {0} is actually {1}.",
        "Nope, the right answer to {0} is {1}.",
        "Unfortunately, that's wrong. The correct answer is {1}.",
        "Close, but {0} is {1}."
    };

    void Start()
    {
        InitializeButtons();
        HideFeedback();
        HideQuizUI();  // Initially hide the quiz until it's time to display
    }

    void Update()
    {
        // Testing mode: Press 'T' to display question for debugging purposes
        if (testingMode && Input.GetKeyDown(KeyCode.T))
        {
            ShowQuestionAfterDayEnd();
        }
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            int index = i;  // Capture variable for listener
            choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(index));
        }
    }

    private void HideFeedback()
    {
        if (feedbackText == null)
        {
            Debug.LogError("Feedback Text (TextMeshProUGUI) is not assigned! Please assign it in the Inspector.");
            return;
        }
        feedbackText.gameObject.SetActive(false); // Hide feedback at the start
    }

    public void ShowQuestionAfterDayEnd()
    {
        if (!questionDisplayed && currentQuestionIndex < quizData.questions.Length)
        {
            questionDisplayed = true;  // Ensure the question is only shown once per day
            LoadQuestion();
        }
    }

    private void LoadQuestion()
    {
        if (currentQuestionIndex >= quizData.questions.Length)
        {
            Debug.Log("All questions completed. No more questions to show.");
            EndQuiz();  // Handle the end of the quiz here
            return;
        }

        var question = quizData.questions[currentQuestionIndex];
        questionText.text = question.questionText;

        for (int i = 0; i < choiceTexts.Length; i++)
        {
            choiceTexts[i].text = question.choices[i];
        }

        SetButtonsInteractable(true);      // Enable buttons when loading a new question
        SetQuizUIState(true, QuizState.Question);
    }

    private void OnChoiceSelected(int selectedIndex)
    {
        var question = quizData.questions[currentQuestionIndex];
        var correctAnswerIndex = question.correctAnswerIndex;

        // Disable buttons to prevent multiple clicks
        SetButtonsInteractable(false);

        // Hide the question and choices
        HideQuizUI();

        // Process the feedback
        DisplayFeedback(GetFeedbackMessage(selectedIndex, correctAnswerIndex, question.questionText, question.choices[correctAnswerIndex]));

        // Move to the next question or end the quiz
        if (currentQuestionIndex < quizData.questions.Length - 1)
        {
            currentQuestionIndex++;
            Invoke(nameof(PrepareForNextDay), 3f);  // Delay for feedback before resetting for next day
        }
        else
        {
            Invoke(nameof(EndQuiz), 3f);  // Delay to show feedback before ending the quiz
        }
    }

    private void DisplayFeedback(string message)
    {
        feedbackText.text = message;
        feedbackText.gameObject.SetActive(true);
        currentState = QuizState.Feedback;
    }

    private string GetFeedbackMessage(int selectedIndex, int correctAnswerIndex, string questionText, string correctAnswer)
    {
        if (selectedIndex == correctAnswerIndex)
        {
            // Select a random correct feedback phrase
            int randomIndex = Random.Range(0, correctPhrases.Length);
            return string.Format(correctPhrases[randomIndex], questionText, correctAnswer);
        }
        else
        {
            // Select a random wrong feedback phrase
            int randomIndex = Random.Range(0, wrongPhrases.Length);
            return string.Format(wrongPhrases[randomIndex], questionText, correctAnswer);
        }
    }

    private void PrepareForNextDay()
    {
        currentState = QuizState.Finished; // Reset the quiz state
        questionDisplayed = false;           // Reset for the next day

        // Re-enable buttons and hide quiz UI
        SetButtonsInteractable(true);        // Enable buttons for the new question
        HideQuizUI();                       // Hide the quiz until the next question is ready
    }

    private void SetQuizUIState(bool isActive, QuizState state)
    {
        questionText.gameObject.SetActive(isActive);
        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(isActive);
        }
        feedbackText.gameObject.SetActive(state == QuizState.Feedback);
    }

    private void HideQuizUI()
    {
        questionText.gameObject.SetActive(false);
        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }
        feedbackText.gameObject.SetActive(false); // Make sure feedback is hidden initially
    }

    private void SetButtonsInteractable(bool interactable)
    {
        foreach (var button in choiceButtons)
        {
            button.interactable = interactable;
        }
    }

    private void EndQuiz()
    {
        Debug.Log("Quiz has ended!");
        DisplayFeedback("Game Finished!");
        SetQuizUIState(false, QuizState.Finished);
    }

    private void RewardPlayer()
    {
        Debug.Log("Player rewarded!");
        // Add reward logic here
    }

    private void PenalizePlayer()
    {
        Debug.Log("Player penalized!");
        // Add penalty logic here
    }
}
