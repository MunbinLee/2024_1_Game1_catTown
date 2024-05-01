using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPlayer : MonoBehaviour
{
    // �÷��̾� �̵� �ӵ�
    private float walkSpeed = 3f;

    private float runSpeed = 6f;
    public bool isRunnig = false;

    public float applySpeed;

    //ĳ���� ��Ʈ�ѷ� ����
    CharacterController cc;

    //�÷��̾� ü�� ����

    int hp = 200; //hp = health point

    public int maxHp = 200;
    private int minHp = 0;

    public Slider hpSlider;

    //�÷��̾� ���¹̳� ����

    int st = 1000; //st = stemina

    public int maxSt = 1000;
    private int minSt = -5;

    private int std = 10; // stemina damega
    private int sth = 500; // stemina heal

    public Slider stSlider;

    //������ �ڷ�ƾ
    IEnumerator DelayDamst()
    {

        float seconds = 10.0f;
        yield return new WaitForSecondsRealtime(seconds);

    }
    IEnumerator DelayHilst()
    {

        float secondsH = 3.0f;
        yield return new WaitForSecondsRealtime(secondsH);
        st += sth;

    }

    //public Animator anim; // �������� �ְ� �ƴ� ���� �ִ� �ִϸ��̼� ���

    //�߷�, ���� �ӵ� ����
    float gravity = -20f;

    float yVelocity = 0;

    // ���� ����
    public float jumpPower = 5f;
    public bool isJumping = false;

    // ��ũ��Ʈ ���� ȣ��
    private void Start()
    {
        //ĳ���� ��Ʈ�ѷ� ������Ʈ �޾ƿ���
        cc = GetComponent<CharacterController>();

        //�ӵ� �ʱ�ȭ
        applySpeed = walkSpeed;

    }

    void Update()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //�÷��̾� �̵� ����
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        transform.position += dir * applySpeed * Time.deltaTime;

        //���� ī�޶� ���� ���� �̵�
        dir = Camera.main.transform.TransformDirection(dir);

        //���� �ӵ� * �߷� 
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        cc.Move(dir * applySpeed * Time.deltaTime);


        //�����̽��� �Է¿� ���� ���� ���� ���ǹ�

        if (isJumping && cc.collisionFlags == CollisionFlags.Below)
        {

            isJumping = false;
            yVelocity = 0;

        }

        if (Input.GetButtonDown("Jump") && !isJumping)
        {

            yVelocity = jumpPower;
            isJumping = true;

        }

        //Shift Ű �Է¿� ���� �޸��� ���� ���ǹ� �� ���¹̳� ���� ����

        if (Input.GetKey(KeyCode.RightShift) && st > 0)
        {

            isRunnig = true;
            applySpeed = runSpeed;
            st -= std;

        }
        else
        {

            isRunnig = false;
            applySpeed = walkSpeed;

            StartCoroutine("DelayDamst");
            StartCoroutine("DelayHilst");

        }

        //���� �÷��̾� ü�� �ۼ��������� ü�¹��� Value�� �ݿ�

        hpSlider.value = (float)hp / (float)maxHp;

        //���� �÷��̾� ���¹̳� �ۼ������ ü�¹��� Value�� �ݿ�

        stSlider.value = (float)st / (float)maxSt;

    }



}
