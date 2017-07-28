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
    public GameObject Options;
    public GameObject blackBackground;

    public Button setGamespeedA; // Game speed 1x
    public Button setGamespeedB; // Game speed 1.5x
    public Button setGamespeedC; // Game speed 2x
    bool UI_IsShown;
    GameObject CurrentLevelPanel;
    float gameSpeed=1;
	// Use this for initialization
	void Start () {
        Fastforwarded = false;
        DOTween.Init(false, false, LogBehaviour.Default);
        UI_IsShown = false;

        if (gameSpeed == 1)
        {
            SetTransparency255(setGamespeedA.GetComponent<Image>()); 
            SetTransparency0(setGamespeedB.GetComponent<Image>());
            SetTransparency0(setGamespeedC.GetComponent<Image>());
        }
        else if (gameSpeed == 1.5f)
        {
            SetTransparency0(setGamespeedA.GetComponent<Image>());
            SetTransparency255(setGamespeedB.GetComponent<Image>());
            SetTransparency0(setGamespeedC.GetComponent<Image>());
        }
        else if (gameSpeed == 2)
        {
            SetTransparency0(setGamespeedA.GetComponent<Image>());
            SetTransparency0(setGamespeedB.GetComponent<Image>());
            SetTransparency255(setGamespeedC.GetComponent<Image>());
        }
    }

    //void FixedUpdate()
    //{
    //    if (Fastforwarded)
    //        Time.timeScale = 3;
    //    else
    //        Time.timeScale = gameSpeed;
    //}

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
        Time.timeScale = gameSpeed;
        Panel.gameObject.SetActive(false);
        BGMSlider.gameObject.SetActive(false);
        SFXSlider.gameObject.SetActive(false);
        ResumeButton.gameObject.SetActive(false);
        B2Menu.gameObject.SetActive(false);
    }
    public void Back2Menu()
    {
        Time.timeScale = gameSpeed;
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
            Time.timeScale = gameSpeed;
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
            Time.timeScale = gameSpeed;
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
            Time.timeScale = gameSpeed;
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
            Time.timeScale = gameSpeed;
            LoadingScreenManager.LoadScene("MainMenu");
        }
    }

    public void TestScene()
    {
        Time.timeScale = gameSpeed;
        LoadingScreenManager.LoadScene("TestScene");
    }

    public void MeatFab()
    {
        Time.timeScale = gameSpeed;
        LoadingScreenManager.LoadScene("Virt_MeatFabrication");
    }

    public void storageTest()
    {
        Time.timeScale = gameSpeed;
        LoadingScreenManager.LoadScene("StorageTest");
    }

    public void SetAugmentedMode()
    {
        if (SceneManager.GetActiveScene().name == "AR_Main")
        {
            Time.timeScale = gameSpeed;
            SceneManager.LoadScene("Virt_Restaurant");
        }
        else
        {
            Time.timeScale = gameSpeed;
            LoadingScreenManager.LoadScene("AR_Main");
        }
    }
    public void moveUI(float show)
    {
        if (!UI_IsShown)
        {
           // UI_Buttons.transform.DOMoveY(show, 0.4f);
            UI_IsShown = true;
            UI_Buttons.transform.DOLocalMoveY(show, 0.4f);
        }
        else
        {
            UI_Buttons.transform.DOLocalMoveY(1500, 0.4f);
            UI_IsShown = false;
        }
    }

    public void showOptions()
    {
        Options.SetActive(true);
        blackBackground.SetActive(true);
        Time.timeScale = 0;
    }

    public void closeOptions()
    {
        Options.SetActive(false);
        blackBackground.SetActive(false);
        Time.timeScale = gameSpeed;
    }
    public void changeGameSpeed(float newGameSpeed)
    {
        gameSpeed = newGameSpeed;

        if (newGameSpeed == 1)
        {
            SetTransparency255(setGamespeedA.GetComponent<Image>());
            SetTransparency0(setGamespeedB.GetComponent<Image>());
            SetTransparency0(setGamespeedC.GetComponent<Image>());
        }
        else if (newGameSpeed == 1.5f)
        {
            SetTransparency0(setGamespeedA.GetComponent<Image>());
            SetTransparency255(setGamespeedB.GetComponent<Image>());
            SetTransparency0(setGamespeedC.GetComponent<Image>());
        }
        else if (newGameSpeed == 2)
        {
            SetTransparency0(setGamespeedA.GetComponent<Image>());
            SetTransparency0(setGamespeedB.GetComponent<Image>());
            SetTransparency255(setGamespeedC.GetComponent<Image>());
        }
    }

    void SetTransparency255(Image p_image)
    {
        if (p_image != null)
        {
            Color __alpha = p_image.color;
            __alpha.a = 255;
            p_image.color = __alpha;
        }
    }

    void SetTransparency0(Image p_image)
    {
        if (p_image != null)
        {
            Color __alpha = p_image.color;
            __alpha.a = 0;
            p_image.color = __alpha;
        }
    }
}
