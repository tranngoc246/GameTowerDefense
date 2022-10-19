using Assets.HeroEditor.Common.CharacterScripts;
using HeroEditor.Common.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
    public Character character;
    public Transform armL;
    public Transform armR;
    public KeyCode fireButton = KeyCode.Mouse1;
    public KeyCode reloadButton = KeyCode.R;
    public bool fixHorizontal;

    private bool _locked;

    public void Update()
    {
        _locked = !character.Animator.GetBool("Ready") || character.Animator.GetInteger("Dead") > 0;

        if (_locked) return;

        switch (character.WeaponType)
        {
            case WeaponType.Melee1H:
            case WeaponType.Melee2H:
            case WeaponType.MeleePaired:
                if (Input.GetKeyDown(fireButton))
                {
                    character.Animator.SetTrigger(Time.frameCount % 2 == 0 ? "Slash" : "Jab"); // Play animation randomly
                }
                break;
            case WeaponType.Bow:
                character.BowShooting.ChargeButtonDown = Input.GetKeyDown(fireButton);
                character.BowShooting.ChargeButtonUp = Input.GetKeyUp(fireButton);
                break;
            case WeaponType.Firearms1H:
            case WeaponType.Firearms2H:
                character.Firearm.Fire.FireButtonDown = Input.GetKeyDown(fireButton);
                character.Firearm.Fire.FireButtonPressed = Input.GetKey(fireButton);
                character.Firearm.Fire.FireButtonUp = Input.GetKeyUp(fireButton);
                character.Firearm.Reload.ReloadButtonDown = Input.GetKeyDown(reloadButton);
                break;
            case WeaponType.Supplies:
                if (Input.GetKeyDown(fireButton))
                {
                    character.Animator.Play(Time.frameCount % 2 == 0 ? "UseSupply" : "ThrowSupply", 0); // Play animation randomly
                }
                break;
        }
    }

    /// <summary>
    /// Called each frame update, weapon to mouse rotation example.
    /// </summary>
    public void LateUpdate()
    {
        if (_locked) return;

        Transform arm;
        Transform weapon;

        switch (character.WeaponType)
        {
            case WeaponType.Bow:
                arm = armL;
                weapon = character.BowRenderers[3].transform;
                break;
            case WeaponType.Firearms1H:
            case WeaponType.Firearms2H:
                arm = armR;
                weapon = character.Firearm.FireTransform;
                break;
            default:
                return;
        }

        RotateArm(arm, weapon, fixHorizontal ? arm.position + 1000 * Vector3.right : Camera.main.ScreenToWorldPoint(Input.mousePosition), -40, 40);
    }

    /// <summary>
    /// Selected arm to position (world space) rotation, with limits.
    /// </summary>
    public void RotateArm(Transform arm, Transform weapon, Vector2 target, float angleMin, float angleMax) // TODO: Very hard to understand logic
    {
        target = arm.transform.InverseTransformPoint(target);

        var angleToTarget = Vector2.SignedAngle(Vector2.right, target);
        var angleToFirearm = Vector2.SignedAngle(weapon.right, arm.transform.right) * System.Math.Sign(weapon.lossyScale.x);
        var angleFix = Mathf.Asin(weapon.InverseTransformPoint(arm.transform.position).y / target.magnitude) * Mathf.Rad2Deg;
        var angle = angleToTarget + angleToFirearm + angleFix;

        angleMin += angleToFirearm;
        angleMax += angleToFirearm;

        var z = arm.transform.localEulerAngles.z;

        if (z > 180) z -= 360;

        if (z + angle > angleMax)
        {
            angle = angleMax;
        }
        else if (z + angle < angleMin)
        {
            angle = angleMin;
        }
        else
        {
            angle += z;
        }

        arm.transform.localEulerAngles = new Vector3(0, 0, angle);
    }
}
