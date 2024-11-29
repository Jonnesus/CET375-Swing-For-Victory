using UnityEngine;


public class OpenInnerAirlockDoor : MonoBehaviour
{
    [SerializeField] private Animator animatorOuterAirlock;
    [SerializeField] private Animator animatorInnerAirlock;
    [SerializeField] private AudioSource outerDoorAudio;
    [SerializeField] private AudioSource innerDoorAudio;
    [SerializeField] private string boolNameAirlock = "airlockOpen";

    private void Start()
    {
        GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>().selectEntered.AddListener(x => ToggleAirlockOpen());
    }

    public void ToggleAirlockOpen()
    {
        bool isOpen = animatorOuterAirlock.GetBool(boolNameAirlock);
        animatorOuterAirlock.SetBool(boolNameAirlock, !isOpen);
        animatorInnerAirlock.SetBool(boolNameAirlock, isOpen);
        outerDoorAudio.Play();
        innerDoorAudio.Play();
    }
}