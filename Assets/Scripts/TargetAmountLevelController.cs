using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CannonApp;
/// <summary>
/// Not entirely sure what this function is used for. 
/// It seems to be for placing a "goal" number of targets to be destroyed.
/// 
/// 
/// 
/// </summary>
public class TargetAmountLevelController : LevelController
{
    [SerializeField] private int targetDestructionCount = 30;

    public override void RegisterTarget()
    {
        
    }

    protected override void Awake()
    {
        base.Awake();

        remainingTargets = targetDestructionCount;
        uiGraphics.UpdateRemainingTargets(remainingTargets);
    }



}
