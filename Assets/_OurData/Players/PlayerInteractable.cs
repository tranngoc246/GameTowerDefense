using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractable : AutoLoadComponent
{
    public virtual void Interact()
    {

    }

    public virtual void LinkToInput(bool link)
    {
        PlayerInput playerInput = PlayerManager.Ins.playerInput;
        if (link) playerInput.interactable = this;
        else playerInput.interactable = null;
    }
}

