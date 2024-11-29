using UnityEngine;


public class EndGame : MonoBehaviour
{
    [SerializeField] private SceneTransitionManager sceneTransitionManager;
    [SerializeField] private CountdownTimer countdownTimer;

    private void Start()
    {
        GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>().selectEntered.AddListener(x => FinishGame());
    }

    private void FinishGame()
    {
        countdownTimer.gameEnded = true;
        sceneTransitionManager.GoToSceneAsync(0);
    }
}