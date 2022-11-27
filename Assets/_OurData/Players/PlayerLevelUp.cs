using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelUp : PlayerInteractable
{
    [Header("PlayerLevelUp")]
    [SerializeField] protected float distance = Mathf.Infinity;
    [SerializeField] protected float distanceLimit = 1.5f;
    [SerializeField] protected bool opened = false;
    [SerializeField] protected Animator animator;
    [SerializeField] protected int cost = 100;
    [SerializeField] protected Vector3 spawnPos;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimatior();
    }

    private void FixedUpdate()
    {
        this.CheckDistance();
    }

    protected virtual void LoadAnimatior()
    {
        if (this.animator) return;
        this.animator = GetComponent<Animator>();
        Debug.Log(transform.name + ": LoadAnimator");
    }

    protected virtual void CheckDistance()
    {
        HeroCtrl hero = PlayerManager.Ins.currentHero;
        this.distance = Vector3.Distance(transform.position, hero.transform.position);

        if (this.distance < this.distanceLimit)
        {
            this.animator.SetBool("close", false);
            this.animator.SetBool("closing", false);
            this.animator.SetBool("open", true);
            this.opened = true;
        }
        else
        {
            this.animator.SetBool("close", true);
            this.animator.SetBool("closing", true);
            this.animator.SetBool("open", false);
            this.opened = false;
        }

        this.LinkToInput(this.opened);
    }

    public override void Interact()
    {
        base.Interact();
        HeroCtrl currentHero = PlayerManager.Ins.currentHero;
        HeroesManager heroesManager = currentHero.heroesManager;

        int currentLevel = currentHero.heroLevel.GetLevel();
        if (!heroesManager.TryGetNextHero(currentLevel))
        {
            Debug.LogWarning("Cant level up Hero");
            return;
        }

        int levelCost = this.cost * currentLevel;
        if (!ScoreManager.Ins.GoldDeduct(levelCost)) return;

        HeroCtrl heroCtrl = heroesManager.GetNextHero(currentLevel);

        heroCtrl.transform.parent = PlayersHolder.Ins.transform;
        currentHero.gameObject.SetActive(false);

        PlayerManager.Ins.playerMovement.inputHorizontalRaw = 1;
        this.spawnPos = currentHero.transform.position;
        this.spawnPos.y += 0.5f;

        heroCtrl.characterCtrl.enabled = false;
        heroCtrl.transform.position = this.spawnPos;
        heroCtrl.characterCtrl.enabled = true;

        PlayerManager.Ins.SetPlayerCtrl(heroCtrl.gameObject);
    }
}