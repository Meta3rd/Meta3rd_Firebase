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

        // 로그인 상태 체크 이벤트 등록
        auth.StateChanged += OnChangedAuthState;
    }

    void Update()
    {
        
    }

    void OnChangedAuthState(object sender, EventArgs e)
    {
        // 만약에 유저정보가 있다면
        if(auth.CurrentUser != null)
        {
            print(auth.CurrentUser.Email + ", " + auth.CurrentUser.UserId);
            // 로그인 되어있음
            print("로그인 상태");
        }
        // 그렇지 않으면
        else
        {
            // 로그아웃 
            print("로그아웃 상태");
        }
    }

    public void SignIn(string email, string password)
    {
        StartCoroutine(CoSignIn(email, password));
    }

    IEnumerator CoSignIn(string email, string password)
    {
        // 회원가입 시도
        Task<AuthResult> task = auth.CreateUserWithEmailAndPasswordAsync(email, password);
        // 통신이 완료될때까지 기다린다.
        yield return new WaitUntil( () => task.IsCompleted );
        // 만약에 예외가 없다면
        if(task.Exception == null)
        {
            print("회원가입 성공");
        }
        else
        {
            print("회원가입 실패 : " + task.Exception);
        }
    }

    public void Login(string email, string password)
    {
        StartCoroutine(CoLogin(email, password));
    }

    IEnumerator CoLogin(string email, string password)
    {
        // 로그인 시도
        Task<AuthResult> task = auth.SignInWithEmailAndPasswordAsync(email, password);
        // 통신이 완료될때까지 기다린다.
        yield return new WaitUntil(() => task.IsCompleted);
        // 만약에 예외가 없다면
        if (task.Exception == null)
        {
            print("로그인 성공");
        }
        else
        {
            print("로그인 실패 : " + task.Exception);
        }
    }

    public void Logout()
    {
        auth.SignOut();
    }
}
