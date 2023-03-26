using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// �I�[�f�B�I�N���b�v���C���|�[�g�������ɐݒ��ύX����
/// https://amagamina.jp/blog/audio-clip/
/// https://qiita.com/ptkyoku/items/84b62cfbb4282a7cd7e6
/// https://kan-kikuchi.hatenablog.com/entry/AudioSettings
/// </summary>
public class AudioPostprocessor : AssetPostprocessor
{
    #region Constants

    private const float BGM_LENGTH = 10;

    #endregion

    #region Unity Method

    private void OnPostprocessAudio(AudioClip clip)
    {
        var importer = assetImporter as AudioImporter;

        //����C���|�[�g�݂̂ɐ���
        if (!importer.importSettingsMissing) return;

        SetAudioImporter(clip, importer);
    }

    #endregion

    #region Private Method

    /// <summary>
    /// ���y����ʉ���K�����ݒ�ɕύX����֐�
    /// </summary>
    private void SetAudioImporter(AudioClip clip, AudioImporter importer)
    {
        //Default��AudioImporterSampleSettings�擾
        var settings = importer.defaultSampleSettings;

        var isBGM = clip.length >= BGM_LENGTH;
        if (isBGM)//���y
        {
            //���[�h���Ȃ���Đ����s���̂ŁA���������ق�̏��������g��Ȃ�
            settings.loadType = AudioClipLoadType.Streaming;
        }
        else//���ʉ�
        {
            //�t�@�C���T�C�Y�����悻�����ɂȂ�
            importer.forceToMono = true;
            //�W�J���x�������ACPU�̕��ׂ�}������
            settings.compressionFormat = AudioCompressionFormat.ADPCM;
        }
        //�ύX�𔽉f
        importer.defaultSampleSettings = settings;
    }

    #endregion
}
