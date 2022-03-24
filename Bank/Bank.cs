using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class Bank : MonoBehaviour
{
    public Tower towerPrefab;
    [SerializeField] int startBallance = 150;
    [SerializeField] int currentBallance;
    [SerializeField] TextMeshProUGUI dispalyBalance;
    public Camera mainCamera;
    public ControlPanel controlPanel;
    private RaycastHit hit;
    private GameObject selected;
    bool isEnemySelected = false;
    EnemyManager eManager;

    public int CurrentBallance { get => currentBallance;}
    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
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
            DeSelect();
        }
        if(isEnemySelected)
        {
            SelectDetail(selected);
        }
    }
    private void Awake()
    {
        currentBallance = startBallance;
        UpadateDispaly();
    }
    private void Start()
    {
        eManager = FindObjectOfType<EnemyManager>();
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
                SelectDetail(selected);
            }
        }
    }
    private void SelectDetail(GameObject selected)
    {
        if (selected != null)
        {
            string tag = selected.tag;
            switch (tag)
            {
                case "Enemy":
                    Enemy enemy = selected.GetComponent<Enemy>();
                    controlPanel.Select(enemy.GetInfo(), enemy.icon);
                    controlPanel.NoNTowerControl();
                    isEnemySelected = true;
                    break;
                case "Tower":
                    Tower tower = selected.GetComponent<Tower>();
                    controlPanel.Select(tower.GetInfo(), tower.icon);
                    controlPanel.TowerControl(tower);
                    isEnemySelected = false;
                    break;
                default:
                    Waypoint waypoint = selected.GetComponent<Waypoint>();
                    controlPanel.Select(waypoint.GetInfo(), waypoint.icon);
                    controlPanel.NoNTowerControl();
                    isEnemySelected = false;
                    break;
            }
            controlPanel.EnableDetailPanel(true);
        }
    }
    private void DeSelect()
    {
        selected = null;
        controlPanel.EnableDetailPanel(false);
        controlPanel.NoNTowerControl();
    }
}
