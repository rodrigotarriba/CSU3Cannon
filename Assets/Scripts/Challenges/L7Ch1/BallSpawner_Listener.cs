using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This tests the use of the observer pattern, using UnityActions 
/// </summary>


public class BallSpawner_Listener : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_lowerLeftVecPos;
    [SerializeField]
    private Vector3 m_upperRightVecPos;

    public int m_ballScore = 0;
    public GameObject m_ballPrefab;
    public UnityAction m_ballCreated;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("CreateBall", Random.Range(1, 3));
    }

    public void CreateBall()
    {
        float m_randomTime = Random.Range(1, 3);

        //Give the new position a random pos that makes sense for your UI
        var randomPos = (new Vector3(
    Random.Range(m_lowerLeftVecPos.x, m_upperRightVecPos.x),
    Random.Range(m_lowerLeftVecPos.y, m_upperRightVecPos.y),
    Random.Range(m_lowerLeftVecPos.z, m_upperRightVecPos.z)));
        var ball = Instantiate(m_ballPrefab, randomPos, Quaternion.identity);

        //increase the local value of the ball score
        m_ballScore++;

        //this is where your UnityAction is invoked, since invoke has no seconmd argument, it is going to be automatically invoked.
        m_ballCreated.Invoke();

        //re-Invoke this function after a set amount of time
        Invoke("CreateBall", m_randomTime);
        Invoke("CreateBall", m_randomTime);



    }
}
