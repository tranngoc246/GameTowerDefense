using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : AutoLoadComponent
{
    public static PlayerManager Ins;
    public PlayerMovement playerMovement;
    public PlayerAttacking playerAttacking;
    public HeroCtrl currentHero;

    private void Awake()
    {
        if (PlayerManager.Ins != null) Destroy(gameObject);
        PlayerManager.Ins = this;
    }

    private void Start()
    {
        this.LoadPlayer();
    }

    protected override void LoadComponents() {
        if (this.playerAttacking != null) return;

        this.playerMovement = gameObject.GetComponentInChildren<PlayerMovement>();
        this.playerAttacking = gameObject.GetComponentInChildren<PlayerAttacking>();
    }

    protected virtual void LoadPlayer()
    {
        Vector3 vec3 = transform.position;
        vec3.x = -8f;
        vec3.y = -2.895f;
        foreach(HeroesManager obj in HeroTypeManagers.Ins.heroesManagers)
        {
            vec3.x += 3;
            GameObject hero = obj.GetHero();
            hero.transform.position = vec3;
            hero.transform.parent = PlayersHolder.Ins.transform;

            HeroCtrl heroCtrl = hero.GetComponent<HeroCtrl>();
            PlayersHolder.Ins.heroCtrls.Add(heroCtrl);

            SetPlayerCtrl(hero);
        }
    }

    public virtual bool ChoosePlayer(string heroClass)
    {
        foreach(HeroCtrl hero in PlayersHolder.Ins.heroCtrls)
        {
            if(hero.heroProfile.HeroClass == heroClass)
            {
                SetPlayerCtrl(hero.gameObject);
                return true;
            }
        }
        return false;
    }

    public virtual void SetPlayerCtrl(GameObject obj)
    {
        this.currentHero = obj.GetComponent<HeroCtrl>();

        this.playerMovement.character = this.currentHero.character;
        this.playerMovement.charCtrl = this.currentHero.GetComponent<CharacterController>();
        this.playerMovement.ResetMyGround();
        this.playerAttacking.character = this.currentHero.character;
        this.playerAttacking.armL = this.currentHero.armL;
        this.playerAttacking.armR = this.currentHero.armR;
    }
}
