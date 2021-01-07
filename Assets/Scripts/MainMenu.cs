using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void NewGame(){
        SceneManager.LoadScene("CharacterCreation");
    }

    public void LoadGame(){
        GameEvents.OnLoadInitiated();
        SceneManager.LoadScene("ShopHubScene");
    }
    public void QuestGiver(){
        SceneManager.LoadScene(3);
        Debug.Log("Loading Quest Sandbox...");
    }
    public void Aaron(){
        SceneManager.LoadScene(1);
        Debug.Log("Loading Shop Sandbox...");
    }
    public void Nick(){
        Debug.Log("Loading Combat Sandbox...");
        SceneManager.LoadScene(2);
    }
    public void Quit(){
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
