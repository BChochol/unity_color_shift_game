using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    [SerializeField] private List<Button> _buttons = new();

    public void RunLastLevel()
    {
        DeactivateAllButtons();
        SceneChanger.Instance.ChangeScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void DeactivateAllButtons()
    {
        foreach (Button button in _buttons)
        {
            button.interactable = false;
        }
    }
    
    public void Exit()
    {
        Application.Quit();
    }
}
