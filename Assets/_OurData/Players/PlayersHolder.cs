using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersHolder : MonoBehaviour
{
    public static PlayersHolder Ins;
    public List<HeroCtrl> heroCtrls;

    private void Awake()
    {
        if (PlayersHolder.Ins != null) Destroy(gameObject);
        else PlayersHolder.Ins = this;
    }

    public virtual HeroCtrl GetHero(string name)
    {

        return heroCtrls[0];
    }
}
