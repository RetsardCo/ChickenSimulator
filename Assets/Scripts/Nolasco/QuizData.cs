using UnityEngine;

[CreateAssetMenu(fileName = "QuizData", menuName = "Quiz/Create New Quiz")]
public class QuizData : ScriptableObject
{
    [System.Serializable]
    public class Question
    {
        public string questionText;     // The actual question
        public string[] choices;        // Array of choices
        public int correctAnswerIndex;  // Index of the correct answer
    }

    public Question[] questions;  // Array to store multiple questions
}
