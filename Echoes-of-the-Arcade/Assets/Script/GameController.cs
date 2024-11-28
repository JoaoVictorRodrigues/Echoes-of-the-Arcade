using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public enum GameState { FreeRoam, MiniGame, Dialog }
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Camera worldCamera;

    GameState state;

    private void Awake()
    {
        //ConditionsDB.Init();
    }

    private void Start()
    {
        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };
        DialogManager.Instance.OnCloseDialog += () =>
        {
            if (state == GameState.Dialog)
                state = GameState.FreeRoam;
        };
    }

    /*void StartMiniGame()
    {
        state = GameState.MiniGame;
        miniGame.gameObject.setActive(true);
        worldCamera.gameObject.SetActive(false);
    }
    void EndMiniGame()
    {
        state = GameState.MiniGame;
        miniGame.gameObject.setActive(false);
        worldCamera.gameObject.SetActive(true);
    }*/

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
    }

}
