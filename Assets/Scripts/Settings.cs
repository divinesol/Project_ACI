using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour {

    public GameObject Panel, BGMSlider, SFXSlider, ResumeButton, B2Menu;
    public Slider BGMSliderVal, SFXSliderVal;

    GameObject CurrentLevelPanel;

	// Use this for initialization
	void Start () {
        
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


}
