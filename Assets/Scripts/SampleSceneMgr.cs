using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// ������ or ȸ������
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
        // ���� ������ ������.
        UserInfo info = new UserInfo();
        info.name = "������";
        info.age = 27;
        info.interests = new List<string>();
        info.interests.Add("����");
        info.interests.Add("ġŲ-�߰�����");
        info.interests.Add("���� Ű����");

        FireStore.instance.SaveUserInfo(info);
    }

    public void OnClickLoadUserInfo()
    {
        FireStore.instance.LoadUserInfo(OnCompleteLoadUserInfo);
    }

    void OnCompleteLoadUserInfo(UserInfo info)
    {
        // ȸ�������� ������� UI �� ����
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
