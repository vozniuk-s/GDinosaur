using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageSwitcher : MonoBehaviour
{
    private string currentLanguage = "en";

    private void Start()
    {
        SwitchLanguage();
    }

    public void SwitchLanguage()
    {
        currentLanguage = (currentLanguage == "en") ? "uk" : "en";
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(currentLanguage);
        Debug.Log(currentLanguage);
    }
}
