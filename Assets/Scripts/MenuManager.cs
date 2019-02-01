using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Overlay;
    [SerializeField]
    private GameObject HighScore;

    public static MenuManager Instance;
    private static bool _alreadyInstantiated;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != Scenes.MainMenu.ToString())
            ShowMenu(false);
    }

    private void Awake()
    {
        if (_alreadyInstantiated)
        {
            Destroy(gameObject);
            return;
        }

        _alreadyInstantiated = true;
        Instance = this;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == Scenes.MainMenu.ToString()) return;

        if (!Input.GetKeyDown(KeyCode.Escape)) return;

        
        Overlay.SetActive(Overlay.activeSelf == false);
        Time.timeScale = Overlay.activeSelf ? 0 : 1;
    }


    public void StartGame()
    {
        SceneManager.LoadScene(Scenes.GameScene.ToString());
    }

    public void Quit()
    {
        if (SceneManager.GetActiveScene().name == Scenes.MainMenu.ToString())
            Application.Quit();
        else
            SceneManager.LoadScene(Scenes.MainMenu.ToString());
    }

    public void ShowHighScore()
    {
        print("Display High Score");

    }

    public void ShowSettings()
    {
        // todo talk to marilola and ask about game settings. might not be necessary
        print("show settings");
    }

    public void ShowMenu(bool show)
    {
        Overlay.SetActive(show);
    }
}