using UnityEngine;

public class PlayerState
{
    protected PlayerController playerController;
    protected State nextStateFlag;

    public PlayerState(PlayerController controller)
    {
        playerController = controller;
    }
    
    // 进入状态
    public virtual void Enter()
    {
        Debug.Log("Enter no override");
    }

    public virtual PlayerState Update()
    {
        Debug.Log("Update no override");
        return null;
    }

    // 退出状态
    public virtual void Exit()
    {
        Debug.Log("Exit no override");
        playerController.stateFlag = nextStateFlag;
    }
}

// 在地面上
// 可以转向：扎根、被攻击、掉落
public class OnGroundState : PlayerState
{

    public OnGroundState(PlayerController controller) : base(controller)
    {
    }
    
    public override PlayerState Update()
    {
        // 若发现没有位于下方的碰撞体，进入掉落状态
        if (playerController.colliderUnder == null)
        {
            Debug.Log("<state> OnGround - > OnAir [check no collider]");
            nextStateFlag = State.OnAir;
            return new OnAirState(playerController);
        }
        
        
        playerController.Move();
        return null;
    }
}

// 在空中
// 可以转向：扎根、在地面上、被攻击
public class OnAirState : PlayerState
{

    public OnAirState(PlayerController controller) : base(controller)
    {
    }
    
    public override PlayerState Update()
    {
        // 若发现有位于下方的碰撞体，进入在地面状态
        if (playerController.colliderUnder != null)
        {
            Debug.Log("<state> OnAir - > OnGround [found collider under]");
            nextStateFlag = State.OnGround;
            return new OnGroundState(playerController);
        }
        
        return null;
    }
}