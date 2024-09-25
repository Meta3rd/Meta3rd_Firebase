using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class FireAuth : MonoBehaviour
{
    public static FireAuth instance;

    public FirebaseAuth auth;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;

        // �α��� ���� üũ �̺�Ʈ ���
        auth.StateChanged += OnChangedAuthState;
    }

    void Update()
    {
        
    }

    void OnChangedAuthState(object sender, EventArgs e)
    {
        // ���࿡ ���������� �ִٸ�
        if(auth.CurrentUser != null)
        {
            print(auth.CurrentUser.Email + ", " + auth.CurrentUser.UserId);
            // �α��� �Ǿ�����
            print("�α��� ����");
        }
        // �׷��� ������
        else
        {
            // �α׾ƿ� 
            print("�α׾ƿ� ����");
        }
    }

    public void SignIn(string email, string password)
    {
        StartCoroutine(CoSignIn(email, password));
    }

    IEnumerator CoSignIn(string email, string password)
    {
        // ȸ������ �õ�
        Task<AuthResult> task = auth.CreateUserWithEmailAndPasswordAsync(email, password);
        // ����� �Ϸ�ɶ����� ��ٸ���.
        yield return new WaitUntil( () => task.IsCompleted );
        // ���࿡ ���ܰ� ���ٸ�
        if(task.Exception == null)
        {
            print("ȸ������ ����");
        }
        else
        {
            print("ȸ������ ���� : " + task.Exception);
        }
    }

    public void Login(string email, string password)
    {
        StartCoroutine(CoLogin(email, password));
    }

    IEnumerator CoLogin(string email, string password)
    {
        // �α��� �õ�
        Task<AuthResult> task = auth.SignInWithEmailAndPasswordAsync(email, password);
        // ����� �Ϸ�ɶ����� ��ٸ���.
        yield return new WaitUntil(() => task.IsCompleted);
        // ���࿡ ���ܰ� ���ٸ�
        if (task.Exception == null)
        {
            print("�α��� ����");
        }
        else
        {
            print("�α��� ���� : " + task.Exception);
        }
    }

    public void Logout()
    {
        auth.SignOut();
    }
}
