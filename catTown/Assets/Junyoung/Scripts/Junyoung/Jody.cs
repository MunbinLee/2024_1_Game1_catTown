using System;
using System.Collections;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.AI;

public class Jody : MonoBehaviour
{
    public LayerMask whatIsTarget; // 적 대상 레이어
    private NavMeshAgent navMeshAgent; // 경로 계산 AI 에이전트
    private Animator amyAnimator; // 애니메이터 컴포넌트
    private Player targetEntity; // 추적 대상
    private int currentPointIndex = 0;
    private bool surprised = true;
    [SerializeField] int noiseLevel = 0;

    // 노이즈 레벨이 일정 수치에 도달하면 Jody가 잠에서 깸.
    // 잠에서 깨었을때 거실을 둘러 보는데 거실에 적이 있으면 추적해서 공격.
    // 적이 없을 경우 노이즈 레벨을 초기화하고 다시 잠.



    // 추적할 대상이 존재하는지 알려주는 프로퍼티
    private bool hasTarget
    {
        get
        {
            // 추적할 대상이 존재하고, 대상이 사망하지 않았다면 true
            if (targetEntity != null && !targetEntity.dead)
            {
                return true;
            }

            // 그렇지 않다면 false
            return false;
        }
    }


    private void Awake()
    {
        // 초기화
        navMeshAgent = GetComponent<NavMeshAgent>();
        amyAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        // 게임 오브젝트 활성화와 동시에 AI의 추적 루틴 시작
        StartCoroutine(UpdatePath());
    }

    void Update()
    {
        // 추적 대상의 존재 여부에 따라 다른 애니메이션 재생
        amyAnimator.SetBool("HasTarget", hasTarget);

    }

    // 주기적으로 추적할 대상의 위치를 찾아 경로 갱신
    private IEnumerator UpdatePath()
    {
        // 살아 있는 동안 무한 루프
        while (true)
        {
            // 적을 발견하지 않은 경우
            if (!hasTarget)
            {
                // 주변에 적이 있는지 확인
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
            else // 적을 발견한 경우
            {
                // 멈춰서서 발견 애니메이션 재생
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
                // 적과의 거리를 확인하여 일정 범위 내에 있으면 추적 시작
                if (Vector3.Distance(transform.position, targetEntity.transform.position) <= 10f)
                {
                    // 추적 중인 애니메이션 재생
                    amyAnimator.SetBool("isRunning", true);
                    navMeshAgent.SetDestination(targetEntity.transform.position);
                    // 추적 중인 경우
                    if (Vector3.Distance(transform.position, targetEntity.transform.position) <= 2f)
                    {
                        // 플레이어가 일정 범위 내에 있으면 공격
                        amyAnimator.SetTrigger("Attack");
                    }
                }
            }

            // 0.25초 주기로 처리 반복
            yield return new WaitForSeconds(0.25f);
        }
    }
}
