using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallTracker_Observer : MonoBehaviour
{
    //This is your reference to the spawner from this script, this is the script being "observed".
    public BallSpawner_Listener m_ballSpawner;


    public TextMeshProUGUI m_text;

    public void Start()
    {
        //serializing the spawner is not enough, you need to GIVE the spawner unityaction the function from this script that you want it to run whenver something else happens.
        //In UnityActions, you do not send the (), you just give it the function name.
        m_ballSpawner.m_ballCreated += UpdateScore;
    }


    //Updates the text, only gets called from the ball spawner whenever a new ball is created.
    public void UpdateScore()
    {
        var m_score = m_ballSpawner.m_ballScore.ToString();

        m_text.text = $"Balls Created: {m_score}";
    }


    public void OnDestroy()
    {
        m_ballSpawner.m_ballCreated -= UpdateScore;
    }
}
