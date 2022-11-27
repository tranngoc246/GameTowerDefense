using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : AutoLoadComponent
{
    public static PlayerManager Ins;
    public PlayerMovement playerMovement;
    public PlayerAttacking playerAttacking;
    public HeroCtrl currentHero;
    public PlayerInput playerInput;
    public string firstClass = "Shooter";

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
        this.playerInput = gameObject.GetComponentInChildren<PlayerInput>();

        Debug.Log(transform.name + ": LoadComponent");
    }

    protected virtual void LoadPlayer()
    {
        HeroCtrl heroCtrl;
        Vector3 vec3 = transform.position;
        foreach(HeroesManager obj in HeroTypeManagers.Ins.heroesManagers)
        {
            string className = obj.heroProfile.HeroClass();
            if (className != this.firstClass) continue;

            heroCtrl = obj.GetHero();
            heroCtrl.transform.position = vec3;
            heroCtrl.transform.parent = PlayersHolder.Ins.transform;

            PlayersHolder.Ins.heroCtrls.Add(heroCtrl);

            SetPlayerCtrl(heroCtrl.gameObject);
        }
    }

    public virtual bool ChoosePlayer(string heroClass)
    {
        foreach(HeroCtrl hero in PlayersHolder.Ins.heroCtrls)
        {
            if(hero.heroProfile.HeroClass() == heroClass)
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
