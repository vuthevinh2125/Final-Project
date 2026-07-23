using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Bắt buộc phải có để Load lại màn chơi

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Progression")]
    public int currentDay = 1;
    public float dayDuration = 10f; // Mỗi ngày dài 10 giây
    private float currentTime;      // Thời gian đếm ngược hiện tại

    [Header("Tài chính")]
    public int currentMoney = 10000;
    public int dailyCost = 1500;

    [Header("Giao diện UI")]
    public TextMeshProUGUI dayTextUI;
    public TextMeshProUGUI moneyTextUI;
    public TextMeshProUGUI timeTextUI; // Dòng chữ báo thời gian
    public GameObject gameOverPanel;   // Màn hình thua (Panel)

    private bool isGameOver = false;

    private void Awake()
    {
        // Tạm thời xóa lệnh DontDestroyOnLoad ở các bài trước để lúc bấm "Chơi lại", 
        // toàn bộ thông số sẽ được Reset mới hoàn toàn.
        Instance = this;
    }

    private void Start()
    {
        currentTime = dayDuration; // Đổ đầy thời gian lúc bắt đầu
        if (gameOverPanel != null) gameOverPanel.SetActive(false); // Đảm bảo màn hình thua bị ẩn
        UpdateUI();
    }

    private void Update()
    {
        if (isGameOver) return; // Nếu đã thua thì không đếm thời gian nữa

        // Đếm ngược thời gian thực
        currentTime -= Time.deltaTime;

        // Nếu hết thời gian hoặc bấm phím T (dùng để test nhanh)
        if (currentTime <= 0 || Input.GetKeyDown(KeyCode.T))
        {
            NextDay();
        }

        UpdateUI(); // Cập nhật thời gian trên màn hình liên tục
    }

    private void NextDay()
    {
        currentDay++;
        currentMoney -= dailyCost;
        currentTime = dayDuration; 

        if (currentMoney < 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f; 
        if (gameOverPanel != null) gameOverPanel.SetActive(true); 
    }

    
    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    public void UpdateUI()
    {
        if (dayTextUI != null) dayTextUI.text = "Day: " + currentDay + ".";
        if (moneyTextUI != null) moneyTextUI.text = "Money: " + currentMoney + "VND";

        if (timeTextUI != null)
        {
           
            timeTextUI.text = "Time: " + Mathf.Ceil(currentTime).ToString() + "s";
        }
    }
}