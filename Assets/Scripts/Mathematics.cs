using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic; // Required for TextMeshPro

public class Mathematics : MonoBehaviour
{
    public StatsManager stats;
    public PostRoundBuff BuffScript;
    public DebuffManager debuffscript;
    private Coroutine mathCoroutine;
    public GameObject mathPanel;
    public TextMeshProUGUI questionText;  // Use TextMeshProUGUI for question display
    public TMP_InputField answerInput;   // Use TMP_InputField for player input
    public TextMeshProUGUI feedbackText; // Use TextMeshProUGUI for feedback text
    private bool postround;
    public bool isMathActive = false;

    private int currentQuestionIndex; // Index of the current question

    // List of math questions and their answers
    private List<MathQuestion> mathQuestions = new List<MathQuestion>();

    void Start()
    {
        mathPanel.SetActive(false);
        // Initialize questions
        mathQuestions.Add(new MathQuestion("5 + 3", "8"));
        mathQuestions.Add(new MathQuestion("10 - 7", "3"));
        mathQuestions.Add(new MathQuestion("4 * 2", "8"));
        mathQuestions.Add(new MathQuestion("12 / 4", "3"));
        mathQuestions.Add(new MathQuestion("12 * 4", "48"));
        mathQuestions.Add(new MathQuestion("7 * 2", "14"));
        mathQuestions.Add(new MathQuestion("4 * 6","24"));
        mathQuestions.Add(new MathQuestion("6 * 4","24"));
        mathQuestions.Add(new MathQuestion("9 * 10","90"));
        mathQuestions.Add(new MathQuestion("7 * 10","70"));
        mathQuestions.Add(new MathQuestion("9 * 8","72"));
        mathQuestions.Add(new MathQuestion("18 + 12","30"));
        mathQuestions.Add(new MathQuestion("77 + 33","110"));

        // Ensure the input field is selected by default
        answerInput.ActivateInputField();
    }

    void Update()
    {
        // Check if the Enter key is pressed
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SubmitAnswer(); // Call the SubmitAnswer method
        }
    }

    public void SubmitAnswer()
    {
        string playerAnswer = answerInput.text.Trim();

        // Debugging logs
        Debug.Log($"Player Answer: {playerAnswer}");
        Debug.Log($"Correct Answer: {mathQuestions[currentQuestionIndex].answer}");

        if (playerAnswer == mathQuestions[currentQuestionIndex].answer)
        {
            feedbackText.text = "Correct!";
            feedbackText.color = Color.green;
            stats.mathcorrect++;
            BuffScript.upgrade();
            postround = false; // Close the math panel
        }
        else
        {
            stats.mathincorrect++;
            isMathActive = false;
            StopCoroutine(mathCoroutine);
            mathCoroutine = null;
            postround = false; // Reset the state
            mathPanel.SetActive(false);
            debuffscript.ShowDebuffPanel();
        }

        // Clear the input field for the next attempt
        answerInput.text = "";

        // Focus the input field for further input
        answerInput.ActivateInputField();
    }
    public void StartMathQuestion()
    {
        if (mathCoroutine == null)
        {
            mathCoroutine = StartCoroutine(MathQuestion());
            isMathActive = true;
        }
    }

    public IEnumerator MathQuestion()
    {
        
        postround = true;
        Time.timeScale = 0f;
        mathPanel.SetActive(true);
        feedbackText.text = ""; // Clear feedback

        // Pick a random question from the list
        currentQuestionIndex = Random.Range(0, mathQuestions.Count);
        questionText.text = mathQuestions[currentQuestionIndex].question;

        // Focus the input field
        answerInput.ActivateInputField();

        // Wait until the answer is submitted or skipped
        while (postround)
        {
            yield return null; // Wait for the next frame
        }

        // Reset things after completion
        mathPanel.SetActive(false);
        // Time.timeScale = 1f;
        mathCoroutine = null;
        isMathActive = false;
    }


    public void Skip()
    {
        if (mathCoroutine != null)
        {
            stats.mathSkipped++;
            isMathActive = false;
            StopCoroutine(mathCoroutine);
            mathCoroutine = null;
            postround = false; // Reset the state
            mathPanel.SetActive(false);
            debuffscript.ShowDebuffPanel();
        }
    }
}

[System.Serializable]
public class MathQuestion
{
    public string question; // The math question
    public string answer;   // The correct answer

    public MathQuestion(string question, string answer)
    {
        this.question = question;
        this.answer = answer;
    }
}
