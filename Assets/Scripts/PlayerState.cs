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
        // Debug.Log("Enter no override");
    }

    public virtual PlayerState Update()
    {
        Debug.Log("Update no override");
        return null;
    }

    // 退出状态
    public virtual void Exit()
    {
        // Debug.Log("Exit no override");
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
        
        // 按键转向扎根状态
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("<state> OnGround - > OnRoot [press key]");
            nextStateFlag = State.OnRoot;
            return new OnRootState(playerController);
        }
        
        
        playerController.Move();
        return null;
    }

    public override void Exit()
    {
        base.Exit();
        
        playerController.rb.velocity = Vector2.zero;
        playerController.anim.SetTrigger("Idle");
    }
}

// 在空中
// 可以转向：扎根、在地面上、被攻击
// 持续扎根检测
public class OnAirState : PlayerState
{
    private RootRangeChecker rangeChecker;
    
    public OnAirState(PlayerController controller) : base(controller)
    {
        rangeChecker = playerController.rangeChecker;
    }

    public override void Enter()
    {
        playerController.rb.gravityScale = 2;
        rangeChecker.gameObject.SetActive(true);
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
        
        // 若下落中途检测到可以扎根的地方，也扎根
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!rangeChecker.isInRange) return null;
            Debug.Log("<state> OnAir - > OnRoot [press key]");
            nextStateFlag = State.OnRoot;
            return new OnRootState(playerController, rangeChecker.hitPos);
        }
        
        playerController.Move();
        
        return null;
    }

    public override void Exit()
    {
        base.Exit();
        rangeChecker.gameObject.SetActive(false);
        playerController.rb.velocity = new Vector2(playerController.rb.velocity.x, 0f);
    }
}

// 在扎根
// 可以延长根、旋转根、撤销
// 开启并持续扎根检测
public class OnRootState : PlayerState
{
    private RootGrow rootController;
    private RootRangeChecker rangeChecker;
    private Vector3 customStartPos; // 指定新根起点，若没有，则选取角色脚下
    private bool isCustomStartPos;

    public OnRootState(PlayerController controller) : base(controller)
    {
        rootController = playerController.GetComponent<RootGrow>();
        rangeChecker = playerController.rangeChecker;
    }
    
    public OnRootState(PlayerController controller, Vector3 startPos) : base(controller)
    {
        rootController = playerController.GetComponent<RootGrow>();
        rangeChecker = playerController.rangeChecker;
        customStartPos = startPos;
        isCustomStartPos = true;
    }
    

    public override void Enter()
    {
        if(!isCustomStartPos)
            rootController.StartRoot();
        else
        {
            rootController.Reset();
            rootController.ReRoot(customStartPos);
        }

        playerController.anim.SetTrigger("Idle");
        playerController.rb.gravityScale = 0;
        rangeChecker.gameObject.SetActive(true);
    }

    public override PlayerState Update()
    {
        // 动画时不响应操作
        if (rootController.isAnim) return null;
        
        var HorizontalMove = Input.GetAxisRaw("Horizontal");
        
        // 存在左右方向按键时，摇摆
        if (HorizontalMove != 0)
        {
            rootController.Swing(HorizontalMove);
        }
        // // 切换地方扎根    // 集中到f键
        // else if (Input.GetKeyDown(KeyCode.C))
        // {
        //     // todo...
        //     /*
        //      * 判断是否可以重新扎根
        //      * 撤回旧根
        //      * 根据新的扎根位置重新初始化根
        //      * 旋转player
        //      */
        //
        //     if (!rangeChecker.isInRange) return null;
        //     
        //     rootController.Reset();
        //     rootController.ReRoot(rangeChecker.hitPos);
        //     
        // }
        // 撤销
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("<state> OnRoot - > OnAir [press key]");
            rootController.Reset();
            nextStateFlag = State.OnAir;
            return new OnAirState(playerController);
        }
        // 进一步生长
        else if (Input.GetKeyDown(KeyCode.F))
        {
            if (rangeChecker.isInRange)
            {
                Debug.Log("try reroot");
                rootController.Reset();
                rootController.ReRoot(rangeChecker.hitPos);   
            }
            else
            {
                Debug.Log("try more root");
                rootController.TryGrowRoot();   
            }
        }

        return null;
    }

    public override void Exit()
    {
        base.Exit();
        rangeChecker.gameObject.SetActive(false);
    }
}