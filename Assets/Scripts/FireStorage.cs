using Firebase.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FireStorage : MonoBehaviour
{
    public static FireStorage instance;

    FirebaseStorage storage;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        storage = FirebaseStorage.DefaultInstance;
    }

    public void Upload(byte[] data, string path)
    {
        StartCoroutine(CoUpload(data, path));
    }
    IEnumerator CoUpload(byte[] data, string path)
    {
        // ��� ���� �� �����̸� ����
        StorageReference storageReference = storage.GetReference(path);
        // ���� ���ε� ��û
        Task<StorageMetadata> task = storageReference.PutBytesAsync(data);
        // ����� �Ϸ�ɶ����� ��ٸ���.
        yield return new WaitUntil(() => task.IsCompleted);
        // ���࿡ ���ܰ� ���ٸ�
        if (task.Exception == null)
        {
            print("���� ���ε� ����");
        }
        else
        {
            print("���� ���ε� ���� : " + task.Exception);
        }
    }

    public void Download(string path, Action<byte[]> onComplete)
    {
        StartCoroutine(CoDownload(path, onComplete));
    }

    IEnumerator CoDownload(string path, Action<byte[]> onComplete)
    {
        // ��� ���� �� �����̸� ����
        StorageReference storageReference = storage.GetReference(path);
        // ���� �ٿ�ε� ��û
        Task<byte[]> task = storageReference.GetBytesAsync(long.MaxValue);
        // ����� �Ϸ�ɶ����� ��ٸ���.
        yield return new WaitUntil(() => task.IsCompleted);
        // ���࿡ ���ܰ� ���ٸ�
        if (task.Exception == null)
        {
            print("���� �ٿ�ε� ����");
            if(onComplete != null)
            {
                onComplete(task.Result);
            }
        }
        else
        {
            print("���� �ٿ�ε� ���� : " + task.Exception);
        }
    }
}
