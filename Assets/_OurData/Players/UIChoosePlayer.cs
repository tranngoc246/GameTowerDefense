using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChoosePlayer : MonoBehaviour
{
    public virtual void ChoosePlayer()
    {
        string heroClass = gameObject.name.Replace("BtnChoose", "");
        PlayerManager.Ins.ChoosePlayer(heroClass);
    }
}
