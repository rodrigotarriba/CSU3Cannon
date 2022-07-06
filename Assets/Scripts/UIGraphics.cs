using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CannonApp;

public class UIGraphics : MonoBehaviour
{
    private static readonly int LevelEndedHash = Animator.StringToHash("LevelEnded");
    private static readonly int GameOverHash = Animator.StringToHash("GameOver");

    [SerializeField] private Animator animator;




    [SerializeField] private TMP_Text levelFinishedText;
    [SerializeField] private TMP_Text remainingTargetsText;


    public void UpdateRemainingTargets(int remainingTargets)
    {
        remainingTargetsText.text = $"Remaining Targets: {remainingTargets}!";
    }

    public void OnFinishedLevelAnimation()
    {
        //we will call the game services,
        GameServices.GetService<LevelController>().NextLevel();

    }

    public void OnRetryClicked()
    {
        GameServices.GetService<LevelController>().RetryGame();
    }

    public void EndGame()
    {
        animator.SetTrigger(GameOverHash);
    }

    public void EndLevel(int currentLevel)
    {
        levelFinishedText.text = $"Level {currentLevel} Finished!";
        animator.SetTrigger(LevelEndedHash);
    }
}
