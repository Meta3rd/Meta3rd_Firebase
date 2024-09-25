using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// 내정보 or 회원정보
[FirestoreData]
public class UserInfo
{
    [FirestoreProperty]
    public string name { get; set; }
    [FirestoreProperty]
    public int age { get; set; }
    [FirestoreProperty]
    public List<string> interests { get; set; }
}


public class SampleSceneMgr : MonoBehaviour
{
    public InputField inputEmail;
    public InputField inputPassword;

    public void OnClickSignIn()
    {
        FireAuth.instance.SignIn(inputEmail.text, inputPassword.text);
    }

    public void OnClickLogin()
    {
        FireAuth.instance.Login(inputEmail.text, inputPassword.text);
    }

    public void OnClickLogout()
    {
        FireAuth.instance.Logout();
    }

    public void OnClickSaveUserInfo()
    {
        // 가상 데이터 만들자.
        UserInfo info = new UserInfo();
        info.name = "김현진";
        info.age = 27;
        info.interests = new List<string>();
        info.interests.Add("게임");
        info.interests.Add("치킨-닭가슴살");
        info.interests.Add("기계식 키보드");

        FireStore.instance.SaveUserInfo(info);
    }

    public void OnClickLoadUserInfo()
    {
        FireStore.instance.LoadUserInfo(OnCompleteLoadUserInfo);
    }

    void OnCompleteLoadUserInfo(UserInfo info)
    {
        // 회원정보를 기반으로 UI 를 구성
        print(info.name);
        print(info.age);
        for (int i = 0; i < info.interests.Count; i++)
        {
            print(info.interests[i]);
        }
    }

    public void Uplaod()
    {
        byte[] data = File.ReadAllBytes("D:\\UnityProjects\\Meta3rd_Photon\\Assets\\Materials\\lock.png");
        string path = "Video/" + FireAuth.instance.auth.CurrentUser.UserId + ".png";

        FireStorage.instance.Upload(data, path);
    }

    public void Download()
    {
        string path = "ProfileImage/" + FireAuth.instance.auth.CurrentUser.UserId + ".png";

        FireStorage.instance.Download(path, OnCompleteDownload);
    }

    public RawImage profileImage;
    void OnCompleteDownload(byte[] data)
    {
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(data);
        profileImage.texture = texture;
    }
}
