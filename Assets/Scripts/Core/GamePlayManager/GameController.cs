using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : Singleton<GameController>
{
    [SerializeField] LayerMask towerLayer;
    [SerializeField] private CoinWallet coinWallet;
    [SerializeField] private TowerPlaceManager placeManager;
    [SerializeField] private PathFinding pathFinding;

    private Tower tower;
    private void Start()
    {
        EventManager.Instance.Register(EventID.OnGameStateChanged, OnGameStateChange);
        GameStateManager.Instance.SetState(GameState.Selection);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            GameStateManager.Instance.SetState(GameState.Victory);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameStateManager.Instance.SetState(GameState.Defeat);
        }
        if (Input.GetMouseButtonDown(0))
        {
            CheckTowerHit();
        }
    }
    void CheckTowerHit()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, Mathf.Infinity, towerLayer);
        if (hit.collider == null)
        {
            if (tower != null) tower.SetActiveStatus(false);
            return;
        }
        if (hit.collider.TryGetComponent<Tower>(out Tower tmpTower) && !placeManager.gameObject.activeSelf)
        {
            if (tower != null) tower.SetActiveStatus(false);
            tmpTower.SetActiveStatus(true);
            tower = tmpTower;
        }
    }
    public int GetCurretCoins()
    {
        return coinWallet.GetCurrentCoins();
    }
    public TowerPlaceManager GetTowerPlaceManager()
    {
        return placeManager;
    }
    public PathFinding GetPathFinding()
    {
        return pathFinding;
    }
    private void OnGameStateChange(object obj)
    {
        if (obj is GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Victory:
                    if (StaticLevel.currentLevelStoryMode == PlayerPrefs.GetInt(GameConstants.STORY_UNLOCK, 1)
                        && StaticLevel.mode == Mode.Story
                        )
                    {
                        PlayerPrefs.SetInt(GameConstants.STORY_UNLOCK, StaticLevel.currentLevelStoryMode + 1);
                    }
                    else if (StaticLevel.currentLevelMazeMode == PlayerPrefs.GetInt(GameConstants.MAZE_UNLOCK, 1)
                        && StaticLevel.mode == Mode.Maze)
                    {
                        PlayerPrefs.SetInt(GameConstants.STORY_UNLOCK, StaticLevel.currentLevelMazeMode + 1);
                    }
                    break;
            }
        }
    }








}
