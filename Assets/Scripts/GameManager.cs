using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TMP_Text coinText;

    [Header("Coins")]
    public List<GameObject> coins = new List<GameObject>();

    [Header("Door")]
    public Door door;

    [Header("Door")]
    [SerializeField] private GameObject panel;


    private int collectedCoinCount = 0;
    private int totalCoinCount;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        totalCoinCount = coins.Count;
        UpdateCoinUI();
        CheckDoor();
    }

    public void CollectCoin(GameObject coin)
    {
        if (coins.Contains(coin))
        {
            coins.Remove(coin);
            collectedCoinCount++;
            UpdateCoinUI();
            CheckDoor();
        }
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = collectedCoinCount + " / " + totalCoinCount;
        }
    }

    void CheckDoor()
    {
        if (door == null) return;

        if (coins.Count == 0)
            door.OpenDoor();
        else
            door.CloseDoor();
    }

    public void MenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Consts.MAIN_MENU);
    }

    public void OpenPanel()
    {
        if (panel != null)
        {
            panel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public class Consts
    {
        public const string MAIN_MENU = "MainMenu";
    }
}
