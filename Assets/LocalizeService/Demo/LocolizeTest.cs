using SunCubeStudio.Localization;
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
        //Added
        PlayerPrefs.SetString("language", "EN");
        LocalizationService.Instance.Localization = "English";   
        CurrentText.text = string.Format("Current localization {0}",
        LocalizationService.Instance.GetTextByKeyWithLocalize("localization1", "English"));
    }

    public void FrenchInPanel()
    {
        //Added
        PlayerPrefs.SetString("language", "FR");
        LocalizationService.Instance.Localization = "French";
        CurrentText.text = string.Format("Current localization {0}",
        LocalizationService.Instance.GetTextByKeyWithLocalize("localization3", "English"));
    }

    public void English()
    {
        PlayerPrefs.SetString("language", "EN");
        SceneManager.LoadScene("Menu");  //changer a Menu, this is for testing purposes
        Vibration.Vibrate(100);
        LocalizationService.Instance.Localization = "English";
        CurrentText.text = string.Format("Current localization {0}",
        LocalizationService.Instance.GetTextByKeyWithLocalize("localization1", "English"));
        
    }

    public void French()
    {
        PlayerPrefs.SetString("language", "FR");
        SceneManager.LoadScene("Menu");  //changer a Menu, this is for testing purposes
        Vibration.Vibrate(100);
        LocalizationService.Instance.Localization = "French";
        CurrentText.text = string.Format("Current localization {0}",
        LocalizationService.Instance.GetTextByKeyWithLocalize("localization3", "English"));
        
    }

}