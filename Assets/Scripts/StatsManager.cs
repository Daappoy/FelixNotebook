using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StatsManager : MonoBehaviour
{
    public Scene SceneManagement;
    public int roundstat;
    public int gruntkills;
    public int Elitekills;
    public int mathcorrect;
    public int mathincorrect;
    public int mathSkipped;
    public TextMeshProUGUI roundstext;
    public TextMeshProUGUI gruntsKilled;
    public TextMeshProUGUI Elitekillstext;
    public TextMeshProUGUI mathcorrecttext;
    public TextMeshProUGUI mathincorrecttext;
    public TextMeshProUGUI mathSkippedtext;
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            // roundstext.text = "Rounds Survived: " + roundstat;
            // gruntsKilled.text = "Grunt Kills: " + gruntkills;
            // Elitekillstext.text = "Elite Kills: " + Elitekills;
            // mathcorrecttext.text = "Math Correct: " + mathcorrect;
            // mathincorrecttext.text = "Math Incorrect: " + mathincorrect;
            // mathSkippedtext.text = "Math Skipped: " + mathSkipped;
        }
        
    }
}
