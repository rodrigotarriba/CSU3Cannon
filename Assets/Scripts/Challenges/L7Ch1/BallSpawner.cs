using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;


/// <summary>
/// Spawns a ball every random interval in a specified vectors contraint.
/// </summary>

public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_lowerLeftVecPos;
    [SerializeField]
    private Vector3 m_upperRightVecPos;

    [SerializeField]
    private TextMeshProUGUI m_textPro;

    [SerializeField]
    private GameObject m_spherePrefab;

    [SerializeField]
    private float m_lowerInterval;
    [SerializeField]
    private float m_higherInterval;
    private float m_timerLength;
    private float m_currentTimer;

    [SerializeField]
    private int m_currentScore;

    private UnityAction m_myBallAction;

    //assigns actions to be held by UnityAction
    private void Start()
    {
        m_myBallAction += BallCreated;
    }

 

    //method that runs a function every random interval of time
    private void Update()
    {
        m_currentTimer -= Time.deltaTime;

        if(m_currentTimer <= 0)
        {
            m_myBallAction();

            m_timerLength = Random.Range(m_lowerInterval, m_higherInterval);

            m_currentTimer = m_timerLength;


        }
    }



    //method that instantiates a new ball and updates new value to the local score, also updates the UI
    private void BallCreated()
    {
        var randomPos = (new Vector3(
            Random.Range(m_lowerLeftVecPos.x, m_upperRightVecPos.x),
            Random.Range(m_lowerLeftVecPos.y, m_upperRightVecPos.y),
            Random.Range(m_lowerLeftVecPos.z, m_upperRightVecPos.z)));

        Instantiate(m_spherePrefab, randomPos, Quaternion.identity);

        m_currentScore++;

        m_textPro.text = $"Balls Created: {m_currentScore.ToString()}";

    }




}
