using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCtrl : AutoLoadComponent
{
    [Header("Hero")]
    public HeroesManager heroesManager;
    public Character character;
    public CharacterController characterCtrl;
    public Firearm firearm;
    public Transform armL;
    public Transform armR;
    public HeroProfile heroProfile;
    public HeroLevel heroLevel;

    protected override void LoadComponents()
    {
        this.LoadCharacter();
        this.LoadCharacterCtrl();
        this.LoadCharBodyParts();
        this.LoadHeroProfile();
    }


    protected virtual void LoadHeroProfile()
    {
        if (this.heroProfile != null) return;
        this.heroProfile = GetComponent<HeroProfile>();
        this.heroLevel = GetComponent<HeroLevel>();
    }

    protected virtual void LoadCharacter()
    {
        if (this.character != null) return;
        this.character = transform.GetComponentInChildren<Character>();
    }

    protected virtual void LoadCharacterCtrl()
    {
        if (this.characterCtrl != null) return;

        this.characterCtrl = GetComponent<CharacterController>();
        if(this.characterCtrl == null) this.characterCtrl = gameObject.AddComponent<CharacterController>();

        this.characterCtrl.center = new Vector3(0, 1.125f);
        this.characterCtrl.height = 2.5f;
        this.characterCtrl.radius = 0.75f;
        this.characterCtrl.minMoveDistance = 0;
    }

    protected virtual void LoadCharBodyParts()
    {
        if (this.firearm != null) return;
                
        Transform animation = transform.GetChild(0).Find("Animation");
        Transform body = animation.Find("Body");
        Transform upper = body.Find("Upper");
        Transform armR1 = upper.Find("ArmR[1]");
        Transform forearmR = armR1.Find("ForearmR");
        Transform handR = forearmR.Find("HandR");
        Transform tranFirearm = handR.Find("Firearm");

        this.firearm = tranFirearm.GetComponent<Firearm>();
        this.armL = upper.Find("ArmL");
        this.armR = armR1;
    }
}
