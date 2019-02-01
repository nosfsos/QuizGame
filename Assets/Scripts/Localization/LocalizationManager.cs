using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Localization
{
    public class LocalizationManager : MonoBehaviour
    {
        public static LocalizationManager Instance;

        private Dictionary<Languages, Dictionary<string, string>> _localization; // <Uk, <"newGameButton","New Game">>
        private Dictionary<Languages, Dictionary<long, string>> _localizedQuestions; // <UK, <25238592375872,"questionText">>
        private Dictionary<Languages, Dictionary<long, List<string>>> _localizedAnswers; // <Uk, <25238592375872, ["answer1", "answer2", ...]>>

        private static bool _alreadyInstantiated;
        private Languages _currentLanguage;

        private void Awake()
        {
            if (_alreadyInstantiated)
            {
                Destroy(gameObject);
                return;
            }

            _localization = new Dictionary<Languages, Dictionary<string, string>>();
            _localizedQuestions = new Dictionary<Languages, Dictionary<long, string>>();
            _localizedAnswers = new Dictionary<Languages, Dictionary<long, List<string>>>();

            _currentLanguage =
                EnumUtils.StringToEnum<Languages>(PlayerPrefs.GetString("Language", Languages.English.ToString()));

            _alreadyInstantiated = true;
            Instance = this;

            LoadXml();
        }

        private void LoadXml()
        {
            // TODO load all languages at once.
            // TODO load all the questions and answers for all languages at once
        }

        public void ChangeLanguage(Languages selectedLanguage)
        {
            _currentLanguage = selectedLanguage;
            PlayerPrefs.SetString("Language", selectedLanguage.ToString());
        }

        /// <summary>
        /// if there is no key on this language, return default error message to be displayed.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetText(string key)
        {
            return "";
        }
    }
}