using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class Bank : MonoBehaviour
{
    [SerializeField] int startBallance = 150;
    [SerializeField] int currentBallance;
    [SerializeField] TextMeshProUGUI dispalyBalance;
    public Camera mainCamera;
    public ControlPanel controlPanel;
    public GameObject UIMenu;
    private RaycastHit hit;
    private GameObject selected;
    public Tower towerPrefab;
    public Tower holoTower;
    HolographicTower hTower;
    [SerializeField] public EnemyManager eManager;
    private bool menuControl = false;

    public void SetHTower(HolographicTower hTower)
    {
        this.hTower = hTower;
    }
    public int CurrentBallance { get => currentBallance;}
    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (menuControl == false)
            {
                ControlMenu();
                Time.timeScale = 0;
            }
            else
            {
                ControlMenu();
                Time.timeScale = 1;
            }
            menuControl = !menuControl;
        }
        if (Input.GetMouseButtonDown(0) && towerPrefab == null)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            DetectObject();
        }
        if (Input.GetMouseButtonDown(1))
        {
            towerPrefab = null;
            if (hTower != null) {
                Destroy(hTower.gameObject); }
            controlPanel.Deselect();
        }
    }
    private void ControlMenu()
    {
        controlPanel.gameObject.SetActive(menuControl);
        UIMenu.SetActive(!menuControl);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
    private void Awake()
    {
        currentBallance = startBallance;
        UpadateDispaly();
        Time.timeScale = 1;
        //eManager = FindObjectOfType<EnemyManager>();
    }
    private void Start()
    {
        eManager.EnemyBankStart(this);
    }
    public EnemyManager GetEnemyManager()
    {
        return eManager;
    }
    //Currency added
    public void Deposit(int z)
    {
        currentBallance += Mathf.Abs(z);
        UpadateDispaly();
    }
    //Currency used
    public void Withdraw(int z)
    {
        currentBallance -= Mathf.Abs(z);
        UpadateDispaly();

        if (currentBallance < 0)
        {
            ReloadScene();
        }
    }
    //Updates the dispalyed money
    void UpadateDispaly()
    {
        dispalyBalance.text = "Gold: " + currentBallance;
    }
    // Restard the level when objective is failed
    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
    private void DetectObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                selected = hit.collider.gameObject;
                controlPanel.SelectControl(selected.tag, selected);
            }
        }
    }

}
