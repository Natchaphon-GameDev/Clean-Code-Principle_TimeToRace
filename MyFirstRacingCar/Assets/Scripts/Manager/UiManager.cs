using System;
using System.Collections;
using System.Collections.Generic;
using LapSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{
    public class UiManager : MonoBehaviour
    {
        //Lap Panel
        [SerializeField] private RectTransform lapPanel;
        [SerializeField] private TextMeshProUGUI round;
        
        //Menu Panel
        [SerializeField] private RectTransform menuPanel;
        [SerializeField] private Button playButton;
        [SerializeField] private Button howToPlayButton;
        [SerializeField] private Button creditButton;
        [SerializeField] private Button quitButton;
        
        //HowtoPlay
        [SerializeField] private RectTransform howToPlayPanel;
        [SerializeField] private Image[] manuals;
        [SerializeField] private Button goBack;
        [SerializeField] private Button nextManual;
        [SerializeField] private Button nextManual2;

        private int nextManualPage = 0; //Control Page
        
        //Credit Panel
        [SerializeField] private RectTransform creditPanel;
        [SerializeField] private Image[] developers;
        [SerializeField] private Button backButton;
        [SerializeField] private Button nextDev;
        
        private int nextDevPage; //Control Page
        
        //In-Game Menu Panel
        [SerializeField] private RectTransform inGameMenuPanel;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button inGameQuitButton;
        [SerializeField] private Button mainMenuButton;
        
        //Each Lab time
        [SerializeField] private TextMeshProUGUI eachLapTime;
        
        //Final Score Panel
        [SerializeField] private RectTransform finalScorePanel;
        [SerializeField] private Button backToMenu;
        [SerializeField] private TextMeshProUGUI score;
        [SerializeField] private TextMeshProUGUI bestScore;
        
        //Select Level 
        [SerializeField] private RectTransform selectLevelPanel;
        [SerializeField] private TextMeshProUGUI selectMapText;
        [SerializeField] private TextMeshProUGUI selectModeText;
        [SerializeField] private Button selectMap;
        [SerializeField] private Button selectMode1;
        [SerializeField] private Button selectMode2;
        
        public bool IsGameStart { get; private set; }

        public static UiManager Instance { get; private set; }

        public event Action OnStarted;
        public event Action OnRestarted;
        public event Action OnMainManu;
        public event Action OnButtonClicked;

        private void Awake()
        {
            Debug.Assert(lapPanel != null, "lapPanel can't be null");
            Debug.Assert(round != null, "round can't be null");
            
            Debug.Assert(menuPanel != null, "menuPanel can't be null");
            Debug.Assert(playButton != null, "playButton can't be null");
            Debug.Assert(howToPlayButton != null, "howToPlayButton can't be null");
            Debug.Assert(creditButton != null, "creditButton can't be null");
            Debug.Assert(quitButton != null, "quitButton can't be null");
            
            Debug.Assert(howToPlayPanel != null, "howToPlayPanel can't be null");
            Debug.Assert(manuals != null && developers.Length != 0, "rule clips need to setup");
            Debug.Assert(goBack != null, "goBack can't be null");
            Debug.Assert(nextManual != null, "nextManual can't be null");
            Debug.Assert(nextManual2 != null, "nextManual2 can't be null");
            
            Debug.Assert(creditPanel != null, "creditPanel can't be null");
            Debug.Assert(developers != null && developers.Length != 0, "developers clips need to setup");
            Debug.Assert(backButton != null, "backButton can't be null");
            Debug.Assert(nextDev != null, "nextDev can't be null");
            
            Debug.Assert(inGameMenuPanel != null, "inGameMenuPanel can't be null");
            Debug.Assert(resumeButton != null, "resumeButton can't be null");
            Debug.Assert(restartButton != null, "restartButton can't be null");
            Debug.Assert(inGameQuitButton != null, "inGameQuitButton can't be null");
            Debug.Assert(mainMenuButton != null, "mainMenuButton can't be null");
            
            Debug.Assert(eachLapTime != null, "eachLapTime can't be null");
            
            Debug.Assert(finalScorePanel != null, "finalScorePanel can't be null");
            Debug.Assert(backToMenu != null, "backToMenu can't be null");
            Debug.Assert(score != null, "score can't be null");
            Debug.Assert(bestScore != null, "bestScore can't be null");
            
            Debug.Assert(selectLevelPanel != null, "selectLevelPanel can't be null");
            Debug.Assert(selectMapText != null, "selectMapText can't be null");
            Debug.Assert(selectModeText != null, "selectModeText can't be null");
            Debug.Assert(selectMap != null, "selectMap can't be null");
            Debug.Assert(selectMode1 != null, "selectMode1 can't be null");
            Debug.Assert(selectMode2 != null, "selectMode2 can't be null");

            //Set Menu Game
            IsGameStart = false;
            
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            
            //Hide Unused Panel
            inGameMenuPanel.gameObject.SetActive(false);
            creditPanel.gameObject.SetActive(false);
            howToPlayPanel.gameObject.SetActive(false);
            lapPanel.gameObject.SetActive(false);
            finalScorePanel.gameObject.SetActive(false);
            selectLevelPanel.gameObject.SetActive(false);
            selectMode1.gameObject.SetActive(false);
            selectMode2.gameObject.SetActive(false);
            selectModeText.gameObject.SetActive(false);
            eachLapTime.gameObject.SetActive(false);
            //Hide all dev except First dev
            foreach (var developer in developers)
            {
                if (developer == developers[0])
                {
                    continue;
                }
                developer.gameObject.SetActive(!true);
            }
            //Hide all manual except First manual
            foreach (var manual in manuals)
            {
                if (manual == manuals[0])
                {
                    continue;
                }
                manual.gameObject.SetActive(!true);
            }

            //Menu Set Button 
            playButton.onClick.AddListener(OnPlayButtonClicked);
            howToPlayButton.onClick.AddListener(OnHowToPlayClicked);
            creditButton.onClick.AddListener(OnCreditClicked);
            quitButton.onClick.AddListener(OnQuitClicked);
            
            //HowToPlay Set Button
            goBack.onClick.AddListener(OnGoBackClicked);
            nextManual.onClick.AddListener(OnNextManualClicked);
            nextManual2.onClick.AddListener(OnNextManualClicked);
            
            //Credit Set Button
            backButton.onClick.AddListener(OnBackClicked);
            nextDev.onClick.AddListener(OnNextDevClicked);
            
            //In-Game Menu Set Button
            resumeButton.onClick.AddListener(OnResumeClicked);
            restartButton.onClick.AddListener(OnRestartClicked);
            inGameQuitButton.onClick.AddListener(OnQuitClicked);
            mainMenuButton.onClick.AddListener(OnMainMenuClicked);
            
            //Final Score Set Button
            backToMenu.onClick.AddListener(OnBackToMenuClicked);
            
            //Select Level Set Button
            selectMap.onClick.AddListener(OnSelectMapClicked);
            selectMode1.onClick.AddListener(OnSelectMode1Clicked);
            selectMode2.onClick.AddListener(OnSelectMode2Clicked);
        }

        private void Start()
        {
            FinishLapCheckPoint.Instance.OnGameEnded += GameEnded;
        }

        //Lap
        private void GameEnded()
        {
            lapPanel.gameObject.SetActive(!true);
            finalScorePanel.gameObject.SetActive(true);
            ShowScore();
            ShowScore();
        }
        //Lap
        
        //Menu
        private void OnPlayButtonClicked()
        {
            OnButtonClicked?.Invoke();
            selectLevelPanel.gameObject.SetActive(true);
            selectMapText.gameObject.SetActive(true);
            selectMap.gameObject.SetActive(true);
            menuPanel.gameObject.SetActive(false);
        }

        private void GameStarted()
        {
            lapPanel.gameObject.SetActive(true);
            IsGameStart = true; //Set Camera Update
            OnStarted?.Invoke();
            RoundCount(); //Update Round UI
        }

        private void OnHowToPlayClicked()
        {
            OnButtonClicked?.Invoke();
            nextManualPage = 6; //Reset to page first manual
            OnNextManualClicked(); //Update page
            howToPlayPanel.gameObject.SetActive(true);
        }

        private void OnCreditClicked()
        {
            OnButtonClicked?.Invoke();
            nextDevPage = 3; //Reset to page first dev
            OnNextDevClicked(); //Update page
            creditPanel.gameObject.SetActive(!false);
            
        }

        private void OnQuitClicked()
        {
            OnButtonClicked?.Invoke();
            Application.Quit();
        }
        //Menu
        
        //HowToPlay
        private void OnGoBackClicked()
        {
            OnButtonClicked?.Invoke();
            howToPlayPanel.gameObject.SetActive(false);
        }

        private void OnNextManualClicked()
        {
            OnButtonClicked?.Invoke();
            Debug.Log(nextManualPage);
            //Easy switch case to implement next page 
            switch (nextManualPage)
            {
                case 0 :
                    manuals[0].gameObject.SetActive(!true);
                    manuals[1].gameObject.SetActive(true);
                    nextManualPage++;
                    break;
                case 1 :
                    manuals[1].gameObject.SetActive(!true);
                    manuals[2].gameObject.SetActive(true);
                    nextManualPage++;
                    break;
                case 2:
                    manuals[2].gameObject.SetActive(!true);
                    manuals[3].gameObject.SetActive(true);
                    nextManualPage++;
                    nextManual2.gameObject.SetActive(true); //change position of button
                    nextManual.gameObject.SetActive(!true); //change position of button
                    break;
                case 3:
                    manuals[3].gameObject.SetActive(!true);
                    manuals[4].gameObject.SetActive(true);
                    nextManualPage++;
                    break;
                case 4:
                    manuals[4].gameObject.SetActive(!true);
                    manuals[5].gameObject.SetActive(true);
                    nextManualPage++;
                    break;
                case 5:
                    manuals[5].gameObject.SetActive(!true);
                    manuals[6].gameObject.SetActive(true);
                    nextManualPage++;
                    break;
                case 6:
                    manuals[6].gameObject.SetActive(!true);
                    manuals[0].gameObject.SetActive(true);
                    nextManual.gameObject.SetActive(true);
                    nextManual2.gameObject.SetActive(false);
                    nextManualPage = 0;
                    break;
            }
        }
        //HowToPlay

        //Credit
        private void OnBackClicked()
        {
            OnButtonClicked?.Invoke();
            creditPanel.gameObject.SetActive(false);
        }

        private void OnNextDevClicked()
        {
            OnButtonClicked?.Invoke();
            //Easy switch case to implement next page 
            switch (nextDevPage)
            {
                case 0 :
                    developers[0].gameObject.SetActive(!true);
                    developers[1].gameObject.SetActive(true);
                    nextDevPage++;
                    break;
                case 1 :
                    developers[1].gameObject.SetActive(!true);
                    developers[2].gameObject.SetActive(true);
                    nextDevPage++;
                    break;
                case 2:
                    developers[2].gameObject.SetActive(!true);
                    developers[3].gameObject.SetActive(true);
                    nextDevPage++;
                    break;
                case 3:
                    developers[3].gameObject.SetActive(!true);
                    developers[0].gameObject.SetActive(true);
                    nextDevPage = 0;
                    break;
            }
        }
        //Credit
        
        //In-Game Menu 
        private void OnRestartClicked()
        {
            OnButtonClicked?.Invoke();
            inGameMenuPanel.gameObject.SetActive(false);
            OnRestarted?.Invoke();
            RoundCount(); //Update Round UI

        }
        
        private void OnResumeClicked()
        {
            OnButtonClicked?.Invoke();
            inGameMenuPanel.gameObject.SetActive(false);
            TimeManager.Instance.UnPauseGame(); //Unpause 

        }
        
        private void OnMainMenuClicked()
        {
            OnButtonClicked?.Invoke();
            TimeManager.Instance.UnPauseGame();
            IsGameStart = false;
            GameManager.Instance.PlayerDestroyed(); //Destory player car
            OnBackToMenuClicked();
        }

        private void OnBackToMenuClicked()
        {
            OnMainManu?.Invoke();
            OnButtonClicked?.Invoke();
            HideInGameMenu(); //hide in-game menu panel
            menuPanel.gameObject.SetActive(true);
            lapPanel.gameObject.SetActive(false);
            finalScorePanel.gameObject.SetActive(false);
        }

        public void ShowInGameMenu()
        {
            inGameMenuPanel.gameObject.SetActive(true);
        }
        
        public void HideInGameMenu()
        {
            inGameMenuPanel.gameObject.SetActive(!true);
        }
        //In-Game Menu 
        
        //Round
        public void RoundCount()
        {
            round.text = $"Round {LapCheck.Instance.Round} / {FinishLapCheckPoint.Instance.NumberOfFinishRounds}";
        }

        private void ShowScore()
        {
            score.text = $"Your Time Score : {LapTime.Instance.MIN}:{LapTime.Instance.Sec}:{LapTime.Instance.MilliSec:F0}";
            
            //Show Score Each Mode
            if (FinishLapCheckPoint.Instance.NumberOfFinishRounds == 1)
            {
                bestScore.text = ScoreManager.Instance.BestScoreOfMode1();
            }
            else if (FinishLapCheckPoint.Instance.NumberOfFinishRounds == 3)
            {
                bestScore.text = ScoreManager.Instance.BestScoreOfMode2();
            }
        }
        //Round
        
        //SelectLevel
        private void OnSelectMapClicked()
        {
            OnButtonClicked?.Invoke();
            selectMap.gameObject.SetActive(!true);
            selectMapText.gameObject.SetActive(!true);
            selectModeText.gameObject.SetActive(true);
            selectMode1.gameObject.SetActive(true);
            selectMode2.gameObject.SetActive(true);
        }
        
        private void OnSelectMode1Clicked()
        {
            OnButtonClicked?.Invoke();
            FinishLapCheckPoint.Instance.SetLap(1); //Set finish laps
            selectLevelPanel.gameObject.SetActive(false);
            selectModeText.gameObject.SetActive(false);
            selectMode1.gameObject.SetActive(false);
            selectMode2.gameObject.SetActive(false);
            GameStarted();
        }
        
        private void OnSelectMode2Clicked()
        {
            OnButtonClicked?.Invoke();
            FinishLapCheckPoint.Instance.SetLap(3);
            selectLevelPanel.gameObject.SetActive(false);
            selectModeText.gameObject.SetActive(false);
            selectMode1.gameObject.SetActive(false);
            selectMode2.gameObject.SetActive(false);
            GameStarted();
        }
        //SelectLevel
        
        //EachLapTime
        public void ShowEachLap()
        {
            eachLapTime.gameObject.SetActive(true);
            eachLapTime.text = ""; //Reset text
            eachLapTime.text += $"Lap {LapCheck.Instance.CheckpointLapIndex + 1} Time : {LapTime.Instance.MIN}:{LapTime.Instance.Sec}:{LapTime.Instance.MilliSec:F0}";
            Invoke("HideEachLapTime",4); //show lap time for 4 second
        }

        private void HideEachLapTime()
        {
            eachLapTime.gameObject.SetActive(false);
        }
        //EachLapTime
    }
}
//Please Give A to 1620701795 senPai :)

