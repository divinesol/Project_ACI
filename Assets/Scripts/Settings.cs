using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Settings : MonoBehaviour {

    public GameObject Panel, BGMSlider, SFXSlider, ResumeButton, B2Menu;
    public Slider BGMSliderVal, SFXSliderVal;
    private bool Fastforwarded;
    public GameObject UI_Buttons;

    bool UI_IsShown;
    GameObject CurrentLevelPanel;

	// Use this for initialization
	void Start () {
        Fastforwarded = false;
        DOTween.Init(false, false, LogBehaviour.Default);
        UI_IsShown = false;
	}

    void FixedUpdate()
    {
        if (Fastforwarded)
            Time.timeScale = 3;
        else
            Time.timeScale = 1;
    }

    public void SettingsButt()
    {
        Time.timeScale = 0;
        Panel.gameObject.SetActive(true);
        BGMSlider.gameObject.SetActive(true);
        SFXSlider.gameObject.SetActive(true);
        ResumeButton.gameObject.SetActive(true);
        B2Menu.gameObject.SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        Panel.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        Panel.gameObject.SetActive(false);
        BGMSlider.gameObject.SetActive(false);
        SFXSlider.gameObject.SetActive(false);
        ResumeButton.gameObject.SetActive(false);
        B2Menu.gameObject.SetActive(false);
    }
    public void Back2Menu()
    {
        Time.timeScale = 1;
        Panel.gameObject.SetActive(false);
        BGMSlider.gameObject.SetActive(false);
        SFXSlider.gameObject.SetActive(false);
        ResumeButton.gameObject.SetActive(false);
        B2Menu.gameObject.SetActive(false);
    }

    public void Fastforward()
    {
        Fastforwarded = !Fastforwarded;
    }

    public float GetBGMValue()
    {
        return BGMSliderVal.value;
    }
    public void SetBGMValue(float BGMVal)
    {
        BGMSliderVal.value = BGMVal;
    }
    
    public void GetSFXValue(float SFXVal)
    {
        SFXVal = SFXSliderVal.value;
    }

    public void CurrentLevelNotif()
    {
        CurrentLevelPanel.SetActive(true);
    }
    public void GoSupplierScene()
    {
        if(SceneManager.GetActiveScene().name == "something")
        {
            CurrentLevelNotif();
        }
        else
        {
            Time.timeScale = 1;
            LoadingScreenManager.LoadScene("Virt_Suppliers");
        }
    }

    public void GoStorageScene()
    {
        if (SceneManager.GetActiveScene().name == "something")
        {
            CurrentLevelNotif();
        }
        else
        {
            Time.timeScale = 1;
            LoadingScreenManager.LoadScene("Virt_Storage");
        }
    }

    public void GoRestaurantScene()
    {
        if (SceneManager.GetActiveScene().name == "something")
        {
            CurrentLevelNotif();
        }
        else
        {
            Time.timeScale = 1;
            LoadingScreenManager.LoadScene("Virt_Restuarant");
        }
    }
    public void GoMainMenu()
    {
        if (SceneManager.GetActiveScene().name == "something")
        {
            CurrentLevelNotif();
        }
        else
        {
            Time.timeScale = 1;
            LoadingScreenManager.LoadScene("MainMenu");
        }
    }

    public void TestScene()
    {
        Time.timeScale = 1;
        LoadingScreenManager.LoadScene("TestScene");
    }

    public void MeatFab()
    {
        Time.timeScale = 1;
        LoadingScreenManager.LoadScene("Virt_MeatFabrication");
    }

    public void storageTest()
    {
        Time.timeScale = 1;
        LoadingScreenManager.LoadScene("StorageTest");
    }

    public void SetAugmentedMode()
    {
        if (SceneManager.GetActiveScene().name == "AR_Main")
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Virt_Restaurant");
        }
        else
        {
            Time.timeScale = 1;
            LoadingScreenManager.LoadScene("AR_Main");
        }
    }
    public void moveUI()
    {
        if (!UI_IsShown)
        {
            UI_Buttons.transform.DOMoveY(280, 0.4f);
            UI_IsShown = true;
        }
        else
        {
            UI_Buttons.transform.DOMoveY(850, 0.4f);
            UI_IsShown = false;
        }
    }
}
