using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic; // ��� ����
    private AudioSource audioSource; // ����� �ҽ� ������Ʈ

    void Start()
    {
        // AudioSource ������Ʈ�� �������ų� ������ �߰��մϴ�.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // ��� ���� ����
        audioSource.clip = backgroundMusic;

        // �ݺ� ��� ����
        audioSource.loop = true;

        // ��� ���� ���
        PlayBackgroundMusic();
    }

    // ��� ������ ����ϴ� �Լ�
    public void PlayBackgroundMusic()
    {
        // ����� ���
        audioSource.Play();
    }

    // ��� ������ �Ͻ� �����ϴ� �Լ�
    public void PauseBackgroundMusic()
    {
        // ����� �Ͻ� ����
        audioSource.Pause();
    }

    // ��� ������ �ٽ� ����ϴ� �Լ�
    public void ResumeBackgroundMusic()
    {
        // ����� �ٽ� ���
        audioSource.Play();
    }

    // ��� ������ �����ϴ� �Լ�
    public void StopBackgroundMusic()
    {
        // ����� ����
        audioSource.Stop();
    }
}
