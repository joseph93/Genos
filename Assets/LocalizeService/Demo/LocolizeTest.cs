﻿using SunCubeStudio.Localization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// For DEMO VIEW
public class LocolizeTest : MonoBehaviour
{
    public Text CurrentText;

    private void Start()
    {
        CurrentText.text = string.Format("Current localization - {0}", LocalizationService.Instance.Localization);
    }

    public void EnglishInPanel()
    {
        LocalizationService.Instance.Localization = "English";

        CurrentText.text = string.Format("Current localization {0}",
            LocalizationService.Instance.GetTextByKeyWithLocalize("localization1", "English"));
    }

    public void FrenchInPanel()
    {
        LocalizationService.Instance.Localization = "French";

        CurrentText.text = string.Format("Current localization {0}",
            LocalizationService.Instance.GetTextByKeyWithLocalize("localization3", "English"));
    }

    public void English()
    {
        SceneManager.LoadScene("Menu");
        Vibration.Vibrate(100);
        LocalizationService.Instance.Localization = "English";

        CurrentText.text = string.Format("Current localization {0}",
            LocalizationService.Instance.GetTextByKeyWithLocalize("localization1", "English"));
        
    }
    public void Russian()
    {
        LocalizationService.Instance.Localization = "Russian";

        CurrentText.text = string.Format("Current localization {0}",
            LocalizationService.Instance.GetTextByKeyWithLocalize("localization2", "English"));
    }

    public void French()
    {
        SceneManager.LoadScene("Menu");
        Vibration.Vibrate(100);
        LocalizationService.Instance.Localization = "French";

        CurrentText.text = string.Format("Current localization {0}",
            LocalizationService.Instance.GetTextByKeyWithLocalize("localization3", "English"));
        
    }

}