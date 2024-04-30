using System;
using System.Collections;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.AI;

public class Jody : MonoBehaviour
{
    public LayerMask whatIsTarget; // �� ��� ���̾�
    private NavMeshAgent navMeshAgent; // ��� ��� AI ������Ʈ
    private Animator amyAnimator; // �ִϸ����� ������Ʈ
    private Player targetEntity; // ���� ���
    private int currentPointIndex = 0;
    private bool surprised = true;
    [SerializeField] int noiseLevel = 0;

    // ������ ������ ���� ��ġ�� �����ϸ� Jody�� �ῡ�� ��.
    // �ῡ�� �������� �Ž��� �ѷ� ���µ� �Žǿ� ���� ������ �����ؼ� ����.
    // ���� ���� ��� ������ ������ �ʱ�ȭ�ϰ� �ٽ� ��.



    // ������ ����� �����ϴ��� �˷��ִ� ������Ƽ
    private bool hasTarget
    {
        get
        {
            // ������ ����� �����ϰ�, ����� ������� �ʾҴٸ� true
            if (targetEntity != null && !targetEntity.dead)
            {
                return true;
            }

            // �׷��� �ʴٸ� false
            return false;
        }
    }


    private void Awake()
    {
        // �ʱ�ȭ
        navMeshAgent = GetComponent<NavMeshAgent>();
        amyAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        // ���� ������Ʈ Ȱ��ȭ�� ���ÿ� AI�� ���� ��ƾ ����
        StartCoroutine(UpdatePath());
    }

    void Update()
    {
        // ���� ����� ���� ���ο� ���� �ٸ� �ִϸ��̼� ���
        amyAnimator.SetBool("HasTarget", hasTarget);

    }

    // �ֱ������� ������ ����� ��ġ�� ã�� ��� ����
    private IEnumerator UpdatePath()
    {
        // ��� �ִ� ���� ���� ����
        while (true)
        {
            // ���� �߰����� ���� ���
            if (!hasTarget)
            {
                // �ֺ��� ���� �ִ��� Ȯ��
                Collider[] colliders = Physics.OverlapSphere(transform.position, 10f, whatIsTarget);
                for (int i = 0; i < colliders.Length; i++)
                {
                    Player player = colliders[i].GetComponent<Player>();
                    if (player != null && !player.dead)
                    {
                        targetEntity = player;
                        break;
                    }
                }
            }
            else // ���� �߰��� ���
            {
                // ���缭�� �߰� �ִϸ��̼� ���
                amyAnimator.SetBool("HasTarget", true);
                if (surprised)
                {
                    Debug.Log("surprised");
                    float navmMeshSpeed = navMeshAgent.speed;
                    navMeshAgent.speed = 0;
                    yield return new WaitForSeconds(2.5f);
                    navMeshAgent.speed = navmMeshSpeed;
                    surprised = false;
                }
                // ������ �Ÿ��� Ȯ���Ͽ� ���� ���� ���� ������ ���� ����
                if (Vector3.Distance(transform.position, targetEntity.transform.position) <= 10f)
                {
                    // ���� ���� �ִϸ��̼� ���
                    amyAnimator.SetBool("isRunning", true);
                    navMeshAgent.SetDestination(targetEntity.transform.position);
                    // ���� ���� ���
                    if (Vector3.Distance(transform.position, targetEntity.transform.position) <= 2f)
                    {
                        // �÷��̾ ���� ���� ���� ������ ����
                        amyAnimator.SetTrigger("Attack");
                    }
                }
            }

            // 0.25�� �ֱ�� ó�� �ݺ�
            yield return new WaitForSeconds(0.25f);
        }
    }
}