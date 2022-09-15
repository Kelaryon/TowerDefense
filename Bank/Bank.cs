using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class Bank : MonoBehaviour
{
    #region Singleton - Not Used
    //Singleton
    //public static Bank BankInstance { get; private set; }
    //Singleton Variant
    //private void SingletonInstantiation()
    //{
    //    if (BankInstance != null && BankInstance != this)
    //    {
    //        Destroy(this);
    //    }
    //    else
    //    {
    //        BankInstance = this;
    //    }
    //}

    //UI
    #endregion

    enum SelectStatus
    {
        Build,
        SelectBuilding,
        SelectGround,
        Idle,
        Menu
    }

    #region References and Variables
    private int currentBallance;
    [SerializeField] TextMeshProUGUI dispalyBalance;
    public Camera mainCamera;
    public ControlPanel controlPanel;
    public GameObject UIMenu;

    //Select & Build Variable
    public bool canBuild;
    public Building buildingPrefab;
    HolographicTower hTower;

    //public EnemyManager eManager;
    private List<Enemy> enemyList;
    private SelectStatus selectStatus;
    public UpdateSystem updateSystem;

    //Map Control
    [SerializeField] MapDataScript mapData;
    public GridMapScript<Waypoint> gridMap;
    [SerializeField] MapReferenceScript mapRef;
    public delegate void CellToGridDelegate(Vector3 wayPos);
    public event Action GameStart;

    #endregion 

    // MonoBehaviour 

    private void Awake()
    {
        mapRef.SetBank(this);
        currentBallance = mapData.GetStartBallance();
        UpadateDispalyGold();
        Time.timeScale = 1;
        mapData.ReturnMapData(out int x, out int y, out float cellSize);
        gridMap = new GridMapScript<Waypoint>(x, y, cellSize);
    }
    private void Start()
    {
        enemyList = new List<Enemy>();
        selectStatus = SelectStatus.Idle;
        updateSystem = new UpdateSystem(controlPanel);
        StartCoroutine(GameStartDelay());
    }
    //State machine implementation for better code
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (selectStatus)
            {
                case SelectStatus.Build:
                    CancelBuild();
                    break;
                case SelectStatus.Menu:
                    MenuClose();
                    break;
                default:
                    MenuOpen();
                    break;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            switch (selectStatus)
            {
                case SelectStatus.Build:
                    TryToBuild();
                    break;
                default:
                    Select();
                    break;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            CancelBuild();
        }
    }

    #region Enemy Management Link, Can be changed for Enemies to get their manager firectly from SO RefObject 
    #endregion

    #region  Build and UI
    void TryToBuild()
    {
        CellToWaypoint(BuildDelegate);
    }
    void Select()
    {
        CellToWaypoint(SelectDelegate);
    }
    public void DeselectHolo()
    {
        if (hTower != null)
        {
            Destroy(hTower.gameObject);
        }
    }
    public void SetHoloTower(HolographicTower hTower)
    {
        if (hTower != null)
        {
            this.hTower = Instantiate(hTower, Utils.GetMouseWorldPoint(), Quaternion.identity);
            this.hTower.Setup(this);
        }
    }
    private void MenuOpen()
    {
        controlPanel.gameObject.SetActive(false);
        UIMenu.SetActive(true);
        selectStatus = SelectStatus.Menu;
        Time.timeScale = 0;
    }
    private void MenuClose()
    {
        controlPanel.gameObject.SetActive(true);
        UIMenu.SetActive(false);
        selectStatus = SelectStatus.Idle;
        Time.timeScale = 1;
    }
    private void CancelBuild()
    {
        //towerPrefab.ActivateRangeCircle();
        buildingPrefab = null;
        if (hTower != null)
        {
            Destroy(hTower.gameObject);
        }
        controlPanel.Deselect();
        selectStatus = SelectStatus.Idle;
    }
    public void ChangeToBuildPhase(Building buildingPrefab, HolographicTower holoPrefab)
    {
        selectStatus = SelectStatus.Build;
        DeselectHolo();
        this.buildingPrefab = buildingPrefab;
        SetHoloTower(holoPrefab);
    }

    //Old Method
    //public void SelectDelegate(Vector3 wayPos)
    //{
    //    gridMap.GetCell(wayPos, out int x, out int z);
    //    var way = gridMap.GetCellValue(x, z);
    //    if (way.ReturnStructure() != null)
    //    {
    //        controlPanel.ObjectSelect(way.ReturnStructure());
    //    }
    //    else
    //    {
    //        controlPanel.ObjectSelect(way);
    //    }
    //}
    public void SelectDelegate(Vector3 waypos)
    {
        gridMap.GetCell(waypos, out int x, out int z);
        var way = gridMap.GetCellValue(x,z);
        controlPanel.ObjectSelect(way.GetSelected());
    
    }

    public void BuildDelegate(Vector3 wayPos)
    {
        //gridMap.CellGridPosition(wayPos, new Vector2Int(towerPrefab.length, towerPrefab.width));
        if (canBuild == false)
        {
            Debug.Log("Can't Build Here");
            return;
        }
        List<Waypoint> wayList = gridMap.GetCellList(gridMap.GetCellList(wayPos, buildingPrefab.GetLength(), buildingPrefab.GetWidth()));
        buildingPrefab.CreateBuilding(buildingPrefab,wayList[0].transform.position,this,wayList);
    }
    //!!!! Need a better name
    public void CellToWaypoint(CellToGridDelegate @delegate)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        @delegate(Utils.GetMouseWorldPoint());
    }
    #endregion

    #region Scene Control
    //Exit Application
    public void ExitGame()
    {
        Application.Quit();
    }
    //Change to Main Menu
    public void MainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
    // Restard the level when objective is failed
    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
    #endregion

    #region Gold System - To Change
    //Updates the dispalyed money, temporary mechanic
    void UpadateDispalyGold()
    {
        dispalyBalance.text = "Gold: " + currentBallance;
    }
    public int CurrentBallance { get => currentBallance; }
    // Currency Control

    //Currency added
    //public void Deposit(int z)
    //{
    //    currentBallance += Mathf.Abs(z);
    //    UpadateDispalyGold();
    //}
    //Currency used
    public void Withdraw(int z)
    {
        currentBallance -= Mathf.Abs(z);
        UpadateDispalyGold();

        if (currentBallance < 0)
        {
            ReloadScene();
        }
    }
    public void AddEnemyToList(Enemy e)
    {
        enemyList.Add(e);
    }
    public void RemoveEnemyFromList(Enemy e)
    {
        enemyList.Remove(e);
    }
    public List<Enemy> GetEnemyList()
    {
        return enemyList;
    }
    #endregion
    IEnumerator GameStartDelay()
    {
        yield return new WaitForSeconds(2);
        GameStart?.Invoke();
    }
}

public class UpdateSystem
{
    private ControlPanel controlPanel;
    //private
    public MethodsInfo[] building1;
    public UpdateSystem(ControlPanel controlPanel)
    {
        building1 = new MethodsInfo[] {null, new MethodsInfo(Update1, "First Update", "First Update",true), null };
        this.controlPanel = controlPanel;
    }
    //public MethodsInfo building1Mehod1 = new MethodsInfo(update1);
    public void Update1()
    {
        Debug.Log("First Update");
        building1[1] = new MethodsInfo(Update2, "Second Upgrade", "Second Upgrade",false);
        controlPanel.ReloadInterface();
    }
    public void Update2()
    {
        Debug.Log("Secodn Update");
        building1[1] = null;
        controlPanel.ReloadInterface();
        TooltipSystem.Hide();
    }


}
