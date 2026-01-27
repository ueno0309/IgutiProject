using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ピクトグラムの再生・停止・リセットをまとめて制御するクラス。
/// Escキーで停止、Rキーでリセット、Spaceキーで再生。
/// </summary>
public class ReplayController : MonoBehaviour
{
    // 再生対象のピクトグラム群（PictogramPlaybackBaseをアタッチしたオブジェクト）
    [SerializeField] private List<PictogramPlaybackBase> pictograms = new List<PictogramPlaybackBase>();

    // 再生中フラグ（状態トグル）
    private bool isPlaying = false;

    private void Update()
    {
        // スペースキー：再生開始
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartPlayback();
        }

        // Sキー：再生停止
        if (Input.GetKeyDown(KeyCode.S))
        {
            StopPlayback();
        }

        // Rキー：シーンリセット（停止＋初期化）
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPlayback();
        }
    }

    /// <summary>
    /// 再生開始
    /// </summary>
    private void StartPlayback()
    {
        if (isPlaying) return;
        isPlaying = true;

        foreach (var pictogram in pictograms)
        {
            if (pictogram != null)
                pictogram.OnStartPlayback();
        }

        Debug.Log("OnStartPlayback呼び出し: isPlaying = True");
    }

    /// <summary>
    /// 再生停止
    /// </summary>
    private void StopPlayback()
    {
        if (!isPlaying) return;
        isPlaying = false;

        foreach (var pictogram in pictograms)
        {
            if (pictogram != null)
                pictogram.OnStopPlayback();
        }

        Debug.Log("OnStopPlayback呼び出し: isPlaying = False");
    }

    /// <summary>
    /// リセット処理（停止＋位置リセット）
    /// </summary>
    private void ResetPlayback()
    {
        isPlaying = false;

        foreach (var pictogram in pictograms)
        {
            if (pictogram != null)
                pictogram.OnReset();
        }

        Debug.Log("OnReset呼び出し: isPlaying = False");
    }
}
