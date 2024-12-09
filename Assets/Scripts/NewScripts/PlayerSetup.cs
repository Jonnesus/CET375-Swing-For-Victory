using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerSetup : MonoBehaviour
{
    public PlayerMovement movement;
    public PlayerGrappling grappling;
    public TempKeyboardMovement keyboardMovement;
    public MouseLook mouseLook;

    public GameObject Camera;
    public GameObject localPlayerGFX;
    public GameObject localPlayerPredictionPoint;
    public GameObject localPlayerNametag;

    public string nickname;

    public TextMeshPro nicknameText;

    public void IsLocalPlayer()
    {
        movement.enabled = true;
        grappling.enabled = true;
        keyboardMovement.enabled = true;
        mouseLook.enabled = true;
        Camera.SetActive(true);
        localPlayerGFX.SetActive(false);
        localPlayerPredictionPoint.SetActive(true);
        localPlayerNametag.SetActive(false);
    }

    [PunRPC]
    public void SetNickname(string _name)
    {
        nickname = _name;
        nicknameText.text = nickname;
    }
}