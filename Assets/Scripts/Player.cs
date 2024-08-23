using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : KitchenObjectHolder
{
    public static Player Instance { get; private set; }//����ģʽ
    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private float rotateSpeed = 10;
    [SerializeField] private GameInput gameInput;
    private bool isWalking=false;
    [SerializeField] private LayerMask counterLayerMask ; 
    public bool IsWalking {  get { return isWalking; } }

    private BaseCounter selectCount;//��¼��ǰѡ�еĹ���

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
    /// ��������ǰ��̨�Ľ���
    /// </summary>
    private void GameInput_OnInteraction(object sender, System.EventArgs e)
    {
        selectCount?.Interact(this);
    }

   

    //һ���Ÿ�������صĴ���
    private void FixedUpdate()
    {
        HandleMovement();
    }

    /// <summary>
    /// ���������ƶ�����ת
    /// </summary>
    private void HandleMovement()
    {
        Vector3 direction = gameInput.GetMovementDirectionNormalized();
        isWalking = direction != Vector3.zero;
        transform.position += direction * Time.deltaTime * moveSpeed;
        if (direction != Vector3.zero)
        {//���в�ֵ���㣬����ת������Ȼ
            transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * rotateSpeed);
        }
    }

    /// <summary>
    /// ���������жϿɽ����Ĺ�̨
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
    /// �жϹ�̨�Ƿ�ı䣬�����̨��ѡ����ȡ��ѡ��
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
