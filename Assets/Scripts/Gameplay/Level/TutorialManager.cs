using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{

    public enum TutorialState
    {
        JoyStick,
        CameraMovement,
        ComboAttack,
        AttackButton,
        HealthBar,
        DefendButton,
        PowerAttack,
        Radar,
        End}

    ;

    public TutorialState _currentTutorialState;
    public GameObject[] Button;
    public Text TutorialTxt;
    public int Number = 0;
    public GameObject[] TutorialPanels;

    void Start()
    {
        UserPrefs.Load();
        if (UserPrefs.isTutorialFinished || GameManager.Instance.currentGameMode == GameManager.GameMode.SurvivalMode)
            Destroy(gameObject);
        else if (!UserPrefs.isTutorialFinished)
        {
            if (Constants.selectedLevel == 0 || Constants.selectedLevel == 1)
                hideButtons();
            else
            {
                Destroy(gameObject);
            }

            if (Constants.selectedLevel == 0)
            {
                _currentTutorialState = TutorialState.JoyStick;
                showText();
            }
            else if (Constants.selectedLevel == 1)
            {
                _currentTutorialState = TutorialState.DefendButton;
                showText();
            }
        }


        Debug.Log("tutorial status : " + UserPrefs.isTutorialFinished);
    }

    void hideButtons()
    {
        int starting_num = 0;

        if (Constants.selectedLevel == 0)
        {
            starting_num = 0;
        }
        else if (Constants.selectedLevel == 1)
        {
            starting_num = 2;

        }
        else if (Constants.selectedLevel > 1)
        {
            UserPrefs.isTutorialFinished = true;
            UserPrefs.Save(); 
            Destroy(gameObject);
        }


        for (int i = starting_num; i < Button.Length; i++)
        {
            Button[i].GetComponent<CanvasGroup>().alpha = 0;
            Button[i].GetComponent<CanvasGroup>().interactable = false;
            Button[i].GetComponent<CanvasGroup>().blocksRaycasts = false;

            if (Button[i].GetComponent<Animator>())
                Button[i].GetComponent<Animator>().enabled = false;
        }

        Number = starting_num;
    }

    public void HideTutorialPanel()
    {
        if (Number == 1)
        {
            TutorialPanels[0].SetActive(false);

        }
    }

    void showText()
    {
        foreach (GameObject g in TutorialPanels)
            g.SetActive(false);

        TutorialPanels[Number].SetActive(true);
        TutorialTxt = TutorialPanels[Number].GetComponentInChildren<Text>();

        if (_currentTutorialState.Equals(TutorialState.HealthBar))
        {
            Number++;
        }

        GAManager.Instance.LogDesignEvent("Tutorial:Step:" + Number);

        Button[Number].GetComponent<CanvasGroup>().alpha = 1;
        Button[Number].GetComponent<CanvasGroup>().interactable = true;
        Button[Number].GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (Button[Number].GetComponent<Animator>())
            Button[Number].GetComponent<Animator>().enabled = true;

        for (int i = 0; i < Button.Length; i++)
        {
            if (i == Number)
            {
                if (Button[i].GetComponent<Animator>())
                    Button[i].GetComponent<Animator>().enabled = true;
            }
            else
            {
                if (Button[i].GetComponent<Animator>())
                    Button[i].GetComponent<Animator>().enabled = false;
            }
        }


        if (_currentTutorialState.Equals(TutorialState.ComboAttack))
        {
            Button[Number + 1].GetComponent<CanvasGroup>().alpha = 1;
            Button[Number + 1].GetComponent<CanvasGroup>().interactable = true;
            Button[Number + 1].GetComponent<CanvasGroup>().blocksRaycasts = true;



            for (int i = 0; i < Button.Length; i++)
            {
                if (i == (Number + 1))
                {
                    if (Button[i].GetComponent<Animator>())
                        Button[i].GetComponent<Animator>().enabled = true;
                }

            }

        }

        Number++;

        switch (_currentTutorialState)
        {
            case TutorialState.JoyStick:
                TutorialTxt.text = Constants.TutorialText[0];
                break;
            case TutorialState.CameraMovement:
                TutorialTxt.text = Constants.TutorialText[1];
                break;
            case TutorialState.ComboAttack:
                TutorialTxt.text = Constants.TutorialText[2];
                Invoke("CompleteCombo", 5);
                break;
            case TutorialState.AttackButton:
                TutorialTxt.text = Constants.TutorialText[3];
                break;
            case TutorialState.DefendButton:
                TutorialTxt.text = Constants.TutorialText[4];
                break;
            case TutorialState.HealthBar:
                TutorialTxt.text = Constants.TutorialText[6];
                Invoke("showEnd", 4);
                break;
        }


    }

    void CompleteCombo()
    {
        NextTutorial(TutorialManager.TutorialState.ComboAttack);
    }

    void showPauseMenu()
    {
        //_currentTutorialState = TutorialState.PauseMenu;
        showText();
        Number++;
    }

    void showEnd()
    {
        Number++;
        _currentTutorialState = TutorialState.End;
        NextTutorial(_currentTutorialState);

        for (int i = 0; i < Button.Length; i++)
        {
            if (Button[i].GetComponent<Animator>())
            {
                Button[i].GetComponent<Animator>().enabled = false;
            }

            Button[i].transform.localScale = Vector3.one;

            if (Button[i].GetComponent<CanvasGroup>())
                Button[i].GetComponent<CanvasGroup>().alpha = 1;
        }
			
    }

    public void DestroyTutorial()
    {
        for (int i = 0; i < Button.Length; i++)
        {
            if (Button[i].GetComponent<Animator>())
            {
                Button[i].GetComponent<Animator>().enabled = false;
            }

            Button[i].transform.localScale = Vector3.one;

        }
        Destroy(gameObject);
    }

    public void NextTutorial(TutorialState _current)
    {

        switch (_current)
        {
            case TutorialState.JoyStick: 
                if (Number == 1)
                {
                    _currentTutorialState = TutorialState.AttackButton;
                    showText();
                }
                break;
          
            case TutorialState.AttackButton: 
                if (Number == 2)
                {
                    _currentTutorialState = TutorialState.DefendButton;
                    showText();
                }
                break;
            case TutorialState.DefendButton: 
                if (Number == 3)
                {
                    _currentTutorialState = TutorialState.ComboAttack;
                    showText();
                }
                break;
            case TutorialState.ComboAttack: 
                if (Number == 4)
                {
                    _currentTutorialState = TutorialState.HealthBar;
                    showText();
                }
                break;
            case TutorialState.End: 
                if (Number == 7)
                {
                    UserPrefs.isTutorialFinished = true;
                    UserPrefs.Save(); 
                    Destroy(gameObject);
                }
                break;
        }
    }
}
