using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CannonApp;

/// <summary>
/// This is a service - 
/// </summary>

public class UIGraphics : MonoBehaviour
{
    private static readonly int LevelEndedHash = Animator.StringToHash("LevelEnded");
    private static readonly int GameOverHash = Animator.StringToHash("GameOver");


    [SerializeField] private Animator animator; //From the UI prefab itself
    [SerializeField] private TMP_Text remainingTargetsText; //From the UI nested prefabs
    [SerializeField] private TMP_Text levelFinishedText; // from the UI nested prefabs


    //Update the UI with the remaining targets, 
    public void UpdateRemainingTargets(int remainingTargets)
    { 
        remainingTargetsText.text = $"Remaining Targets: {remainingTargets}!";
    }


    public void EndGame()
    {
        animator.SetTrigger(GameOverHash);
    }


    public void OnFinishedEndLevelAnimation()
    {
        //When the animation has ended, this instructs level controller to start the next level
        GameServices.GetService<LevelController>().NextLevel();
    }


    public void OnRetryClicked()
    {
        //when retry is clicked, this tells level controller to retry the level
        GameServices.GetService<LevelController>().RetryGame();
    }


    public void EndLevel(int currentLevel)
    {
        levelFinishedText.text = $"Level {currentLevel} Finished!";
        animator.SetTrigger(LevelEndedHash);
    }

}
