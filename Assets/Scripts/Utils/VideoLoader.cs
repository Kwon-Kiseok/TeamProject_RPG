using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class VideoLoader : MonoBehaviour
{

    public string path;
    public VideoPlayer player;
    private VideoClip videoAsset;

    // Start is called before the first frame update
    void Awake()
    {
        LoadClip(path);
    }

    private void LoadClip(string path)
    {
        var clipHandle = Addressables.LoadAssetAsync<VideoClip>(path);
        clipHandle.WaitForCompletion();

        player.clip = ClipLoad_Complete(clipHandle);
        
        if(player.clip != null)
            Debug.Log(clipHandle.Result.name);
        player.Play();

        Addressables.Release(clipHandle);
    }

    private VideoClip ClipLoad_Complete(AsyncOperationHandle<VideoClip> handle)
    {
        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            videoAsset = handle.Result;
            Debug.Log(videoAsset);
            return videoAsset;
        }

        return null;
    }
}
