using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : KitchenObjectHolder
{
    public static Player Instance { get; private set; }//单例模式
    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private float rotateSpeed = 10;
    [SerializeField] private GameInput gameInput;
    private bool isWalking=false;
    [SerializeField] private LayerMask counterLayerMask ; 
    public bool IsWalking {  get { return isWalking; } }

    private BaseCounter selectCount;//记录当前选中的柜子

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        HandleInteraction();
    }

    void Start()
    {
        gameInput.OnInteraction += GameInput_OnInteraction;
        gameInput.OnOperateInteraction += GameInput_OnOperateInteraction;
    }

    private void GameInput_OnOperateInteraction(object sender, System.EventArgs e)
    {
        selectCount?.InteractOperate(this);
    }

    /// <summary>
    /// 按键处理当前柜台的交互
    /// </summary>
    private void GameInput_OnInteraction(object sender, System.EventArgs e)
    {
        selectCount?.Interact(this);
    }

   

    //一般存放跟物理相关的代码
    private void FixedUpdate()
    {
        HandleMovement();
    }

    /// <summary>
    /// 控制主角移动和旋转
    /// </summary>
    private void HandleMovement()
    {
        Vector3 direction = gameInput.GetMovementDirectionNormalized();
        isWalking = direction != Vector3.zero;
        transform.position += direction * Time.deltaTime * moveSpeed;
        if (direction != Vector3.zero)
        {//进行插值运算，让旋转更加自然
            transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * rotateSpeed);
        }
    }

    /// <summary>
    /// 处理交互，判断可交互的柜台
    /// </summary>
    private void HandleInteraction()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitinfo, 2f,counterLayerMask))
        {
            if (hitinfo.transform.TryGetComponent(out BaseCounter counter))
            {
                SetSelectCounter(counter);
            }
            else SetSelectCounter(null);
        }else SetSelectCounter(null);
    }

    /// <summary>
    /// 判断柜台是否改变，处理柜台的选择与取消选择
    /// </summary>
    public void SetSelectCounter(BaseCounter counter)
    {
        if (counter!=selectCount)
        {
            selectCount?.ConcalSelect();
            counter?.SelectCounter();
            this.selectCount = counter;
        }
    }
}
