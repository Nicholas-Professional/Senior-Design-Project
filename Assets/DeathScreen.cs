using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    SceneController controller;
    Button deathbutton;
    // Start is called before the first frame update
    void Start()
    {
        controller=GameObject.Find("SceneController").GetComponent<SceneController>();
        deathbutton=transform.Find("DeathScreen/ReturnToMain").GetComponent<Button>();
        deathbutton.onClick.AddListener(delegate{ triggerReturnToMenu();});
    }
    public void triggerReturnToMenu(){
        controller.GameOver();
    }

}
