using Assets.HeroEditor.Common.CharacterScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : AutoLoadComponent
{
    [Header("Components")]
    public Character character;
    public CharacterController charCtrl;

    [Header("LayerMask")]
    [SerializeField] protected Transform myGround;

    [Header("Movement")]
    [SerializeField] protected float walkingSpeed = 7;
    [SerializeField] protected float jumpSpeed = 10;
    [SerializeField] protected float fallingSpeed = 25;
    [SerializeField] protected int jumpMax = 2;
    [SerializeField] protected int jumpCount = 0;
    [SerializeField] protected bool isGrounded = true;
    [SerializeField] protected bool canJump = false;
    [SerializeField] protected bool jumping = false;

    [Header("Input")]
    [SerializeField] protected float inputHorizontalRaw = 0;
    [SerializeField] protected float inputVerticalRaw = 0;
    [SerializeField] protected float inputJumpRaw = 0;
    [SerializeField] protected bool pressJump = false;

    [Header("Vectors")]
    [SerializeField] protected Vector3 speed = Vector3.zero;
    [SerializeField] protected Vector2 direction;
    [SerializeField] protected Vector3 mouseToChar = Vector3.zero;

    private void Awake()
    {
        this.jumpCount = this.jumpMax;
    }
    private void Start()
    {
        Physics.IgnoreLayerCollision(MyLayerManager.Ins.layerHero, MyLayerManager.Ins.layerCeiling, true);
    }

    private void Update()
    {
        this.GroundFinding();
        this.IsGoingDown();
        this.IsGrounded();
        this.InputToDirection();
        this.CharecterStateUpdate();
        this.Move();
        this.Turning();
    }

    protected virtual void GroundFinding()
    {
        Vector3 direction = Vector3.down;
        Vector3 position = this.character.transform.position;
        Physics.Raycast(position, direction, out RaycastHit hit);
        this.DebugRaycast(position, hit, direction);
        if (!hit.transform) return;

        Ground ground = hit.transform.GetComponent<Ground>();
        if (ground == null) return;
        if (this.myGround == hit.transform) return;

        ground.ChangeLayer(MyLayerManager.Ins.layerGround);
        this.myGround = hit.transform;
    }

    protected virtual bool IsGoingDown()
    {
        bool isGoingDown = this.direction.y < 0;
        if (isGoingDown) this.ResetMyGround();
        return isGoingDown;
    }

    public virtual void ResetMyGround()
    {
        if (!this.myGround) return;
        Ground ground = this.myGround.GetComponent<Ground>();
        if (ground.name == "Ground0") return;
        ground.ChangeLayer(MyLayerManager.Ins.layerCeiling);
        this.myGround = null;
    }

    protected virtual bool IsGrounded()
    {
        this.isGrounded = this.charCtrl.isGrounded;

        if (this.isGrounded)
        {
            this.jumpCount = this.jumpMax;
            this.canJump = true;
            this.jumping = false;
        }
        return this.isGrounded;
    }
    
    protected virtual Vector2 InputToDirection()
    {
        Vector2 direction = Vector2.zero;

        this.inputHorizontalRaw = Input.GetAxisRaw("Horizontal");
        this.inputVerticalRaw = Input.GetAxisRaw("Vertical");
        this.inputJumpRaw = Input.GetAxisRaw("Jump");

        direction.x = this.inputHorizontalRaw;
        direction.y = this.inputVerticalRaw;
        if (this.inputVerticalRaw == 0) direction.y = this.inputJumpRaw;

        if (direction.y > 0) this.pressJump = true;
        else this.pressJump = false;

        this.direction = direction;
        return this.direction;
    }
    
    protected virtual void CharecterStateUpdate()
    {
        character.Animator.SetBool("Run", this.IsGrounded() && Math.Abs(this.direction.x) > 0.01f);
        character.Animator.SetBool("Jump", !this.IsGrounded());
    }
    
    protected virtual void Move()
    {
        this.Walking();
        this.Jumping();

        this.speed.y -= this.fallingSpeed * Time.deltaTime;
        this.charCtrl.Move(this.speed * Time.deltaTime);
    }

    protected virtual void Walking()
    {
        if (!IsGrounded()) return;
        this.speed.x = this.walkingSpeed * this.direction.x; 
    }

    protected virtual void Jumping()
    {
        if(this.jumping && !this.pressJump)
        {
            this.canJump = true;
            this.jumpCount--;
            this.jumping = false;
        }

        if (!this.pressJump) return;
        if (!this.canJump) return;
        if (this.jumpCount < 1) return;

        this.canJump = false;
        this.jumping = true;

        this.speed.y = this.jumpSpeed * this.direction.y;
    }
    
    protected virtual void Turning()
    {
        var scale = character.transform.localScale;

        scale.x = Mathf.Abs(scale.x);

        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < character.transform.position.x) scale.x *= -1;

        character.transform.localScale = scale;
    }


    /*public Character character;
    public KeyCode LeftButton = KeyCode.LeftArrow;
    public KeyCode RightButton = KeyCode.RightArrow;
    public KeyCode JumpButton = KeyCode.UpArrow;
    public CharacterController controller;

    Vector3 _speed = Vector3.zero;

    public void Start()
    {
        if (!character) return;
        
        character.Animator.SetBool("Ready", true);
        controller = character.GetComponent<CharacterController>();
        if (!controller) controller = character.gameObject.AddComponent<CharacterController>();
        
    }

    public void Update()
    {
        if (!character) return;

        Move(Input.GetKey(LeftButton), Input.GetKey(RightButton), Input.GetKey(JumpButton));
        FollowMouse();
    }

    public void Move(bool left, bool right, bool jump)
    {
        if (controller.isGrounded)
        {
            _speed = Vector3.zero;

            if (left) _speed.x = -5;
            if (right) _speed.x = 5;
            if (jump) _speed.y = 10;
        }

        character.Animator.SetBool("Run", controller.isGrounded && Math.Abs(_speed.x) > 0.01f); 
        character.Animator.SetBool("Jump", !controller.isGrounded);

        _speed.y -= 25 * Time.deltaTime; 
        controller.Move(_speed * Time.deltaTime);
    }

    public void FollowMouse()
    {
        var scale = character.transform.localScale;

        scale.x = Mathf.Abs(scale.x);

        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < character.transform.position.x) scale.x *= -1;

        character.transform.localScale = scale;
    }*/

}
