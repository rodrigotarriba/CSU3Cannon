using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using TMPro;

/// <summary>
/// Attached to UI prefab - Keeps track of targets and score, scenes, updates UI
/// </summary>




namespace CannonApp
{
    public class LevelController : MonoBehaviour
    {
        private static readonly int LevelEndedHash = Animator.StringToHash("LevelEnded");
        private static readonly int GameOverHash = Animator.StringToHash("GameOver");

        //[SerializeField] private CannonController cannonController; //from scene, imported directly into the script, but it has been deleted.

        [SerializeField] private Animator animator; //From the UI prefab itself
        [SerializeField] private TMP_Text remainingTargetsText; //From the UI nested prefabs
        [SerializeField] private TMP_Text levelFinishedText; // from the UI nested prefabs

        private static int levelCount;//Where does this one come from?

        protected int remainingTargets; //changing to protected variable so it can be modified from the targets script
        private int currentLevel;

        public Action levelEnded; //fires off once the level has ended


        protected virtual void Awake()
        {
            //service locator pattern
            //we will add the level controller as part of our service locator.
            GameServices.RegisterService(this);
            
            InitializeLevelCount();
            SetCurrentLevel();
            //InitializeTargets();
        }


        // sets the total number of levels in the game
        private void InitializeLevelCount()
        {
            
            // guard clause in case you already have a level count
            if (levelCount != 0)
                return;


            //loop to get the number of levels in the game - goes through every scene and identifies levels if the scenes have 2 digits
            int sceneCount = SceneManager.sceneCountInBuildSettings;

            int maxLevelFound = 0;

            for (int i = 0; i < sceneCount; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                string sceneName = Path.GetFileNameWithoutExtension(scenePath);


                // we need to check if we have reached the max amount of levels
                if (GetLevelIndex(sceneName, out var levelIndex))
                {
                    maxLevelFound = Mathf.Max(maxLevelFound, levelIndex);
                    levelCount++;
                }
            }

            //if the total number of levels found differs from total of max scene numbers, it will print this warning
            Debug.Assert(maxLevelFound == levelCount, "Max Scene Level differs from the total levels found");
        }


        //retrieves the 2 or more digits of a string, returnes the digits parsed as 32 signed int
        private bool GetLevelIndex(string sceneName, out int levelIndex)
        {
           //it looks for one or more digits in the scene name
            Match find = Regex.Match(sceneName, "\\d+");

            if (find != Match.Empty)
            {
                levelIndex = Int32.Parse(find.Value);
                return true;
            }

            levelIndex = -1;
            return false;
        }


        // we are comparing the actual current scene, with the variable we have in the getcurrentscenelevel function.
        private void SetCurrentLevel()
        {
         
            if (!GetLevelIndex(SceneManager.GetActiveScene().name, out currentLevel))
            {
                Debug.LogError("Level Controller on a non-level scene!");
            }
        }

        
        
        
        //increases our remaining targets - added after using the service locator pattern
        public virtual void RegisterTarget()
        {
            remainingTargets++;
            Debug.Log($"added one more target for a total of {remainingTargets}");
            UpdateRemainingTargets();
        }


        //We are not going to use this anymore.

        //private void InitializeTargets()
        //{
        //    Target[] targets = FindObjectsOfType<Target>();

        //    foreach(var target in targets)
        //    {
        //        target.SetUp(this);
        //    }

        //    remainingTargets = targets.Length;
        //    UpdateRemainingTargets();
        //}


        // When a target is destroyed, decreases remainingTargets variable, then calls a function that updates the remaining targets number in the UI, if 
        public void TargetDestroyed()
        {
            remainingTargets--;

            // if all targets are hit, start endlevel process
            if (remainingTargets <= 0)
                EndLevel();

            UpdateRemainingTargets();
        }



        //When the level ends, disables fire, triggers animation, we send a "level ended" text.  
        private void EndLevel()
        {
            levelEnded?.Invoke();



            if (currentLevel == levelCount)
            {
                animator.SetTrigger(GameOverHash);
                return;
            }

            levelFinishedText.text = $"Level {currentLevel} Finished!";
            animator.SetTrigger(LevelEndedHash);
        }


        public void OnFinishedEndLevelAnimation()  //good reference of how to word functions to make sure they are easy to understand.
        {
             GoToLevel(currentLevel + 1);
        }



        public void OnRetryClicked()
        {
            GoToLevel(1);
        }

        //scene management, loads a scene depending on the scene integer you want - now we need to construct some UI thjat tells us the remaining targets and level they are.
        private void GoToLevel(int levelIndex)
        {
            SceneManager.LoadScene($"Level{levelIndex}");
        }


        //Update the UI with the remaining targets, 
        protected void UpdateRemainingTargets()
        {
            remainingTargetsText.text = $"Remaining Targets: {remainingTargets}!";
        }
    }
}