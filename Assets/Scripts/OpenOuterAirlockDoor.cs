using UnityEngine;


public class OpenOuterAirlockDoor: MonoBehaviour
{
    [SerializeField] private Animator animatorOuterAirlock;
    [SerializeField] private AudioSource doorAudio;
    [SerializeField] private string boolNameOuterAirlock = "airlockOpen";

    private void Start()
    {
        GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>().selectEntered.AddListener(x => ToggleAirlockOpen());
    }

    public void ToggleAirlockOpen()
    {
        bool isOpen = animatorOuterAirlock.GetBool(boolNameOuterAirlock);
        animatorOuterAirlock.SetBool(boolNameOuterAirlock, !isOpen);
        doorAudio.Play();
    }
}