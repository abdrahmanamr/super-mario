using UnityEngine;
using UnityEngine.SceneManagement;
using System; 

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int world { get; private set; } = 1;
    public int stage { get; private set; } = 1;
    public int lives { get; private set; } = 3;
    public int coins { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        
    }

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
        string sceneName = scene.name;
        string[] parts = sceneName.Split('-');

        if (parts.Length == 2 && int.TryParse(parts[0], out int parsedWorld) && int.TryParse(parts[1], out int parsedStage))
        {
            world = parsedWorld;
            stage = parsedStage;
            Debug.Log($"GameManager: Loaded Level: World {world}, Stage {stage}");
        }
        
    }


    public void NewGame()
    {
        lives = 3;
        coins = 0;
         Debug.Log("GameManager: New Game state initialized.");
    }

    public void GameOver()
    {
        Debug.Log("GameManager: Game Over. Loading Main Menu."); 
        SceneManager.LoadScene(0); 
    }

    public void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;
        Debug.Log($"GameManager: Loading Level: World {world}, Stage {stage}"); 
        SceneManager.LoadScene($"{world}-{stage}");
    }

    public void ResetLevel(float delay)
    {
        CancelInvoke(nameof(ResetLevel));
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        lives--;
        Debug.Log($"GameManager: Player died. Lives remaining: {lives}"); 

        if (lives > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            GameOver();
        }
    }

    public void AddCoin()
    {
        coins++;
        Debug.Log($"GameManager: Coin collected. Total coins: {coins}");

        if (coins == 100)
        {
            coins = 0;
            AddLife();
        }
    }

    public void AddLife()
    {
        lives++;
        Debug.Log($"GameManager: Extra life gained. Total lives: {lives}");
    }

}
