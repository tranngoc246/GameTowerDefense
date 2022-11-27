using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : AutoLoadComponent
{
    [Header("Player Input")]
    public PlayerInteractable interactable;

    private void Update()
    {
        this.Interacting();
        this.Moving();
    }

    protected virtual void Interacting()
    {
        if (this.interactable == null) return;

        if (Input.GetKeyDown("f")) this.interactable.Interact();
    }

    protected virtual void Moving()
    {
        PlayerManager playerManager = PlayerManager.Ins;
        playerManager.playerMovement.inputHorizontalRaw = Input.GetAxisRaw("Horizontal");
        playerManager.playerMovement.inputVerticalRaw = Input.GetAxisRaw("Vertical");
        playerManager.playerMovement.inputJumpRaw = Input.GetAxisRaw("Jump");
    }
}