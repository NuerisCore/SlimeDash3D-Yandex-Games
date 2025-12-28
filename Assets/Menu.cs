using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.PostProcessing;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public PostProcessingProfile profile;
    public PostProcessingBehaviour volume;
    public Slider dropdown;
    public Slider dropdown1;
    public Slider dropdown2;
    public TMP_Text qualityText;
    public TMP_Text recordText;
    public TMP_Text coinText;

    public AudioMixer sound;

    public GameObject MenuCAnvas;

    public void LateUpdate()
    {
        recordText.text = YG.YandexGame.savesData.record.ToString();

        if (MenuCAnvas.activeSelf)
        {
            int dist = (int)Vector2.Distance(new Vector2(0f, (int.Parse(coinText.text))), new Vector2(0f, YG.YandexGame.savesData.money));
            int parsed = int.Parse(coinText.text);

            if (YG.YandexGame.savesData.money == 0) coinText.text = YG.YandexGame.savesData.money.ToString();
            else if (YG.YandexGame.savesData.money == int.Parse(coinText.text))
            {
                coinText.text = YG.YandexGame.savesData.money.ToString();
                coinText.color = Color.Lerp(coinText.color, Color.white, 1f);
            }
            else if (YG.YandexGame.savesData.money > int.Parse(coinText.text))
            {
                coinText.text = (parsed + dist / 4 + 1).ToString();
                coinText.color = Color.Lerp(coinText.color, Color.green, 1f);
            }
            else if (YG.YandexGame.savesData.money < int.Parse(coinText.text))
            {
                coinText.text = (parsed - dist / 4 - 1).ToString();
                coinText.color = Color.Lerp(coinText.color, Color.red, 1f);
            }
        }
    }



    private void Start()
    {
        if (PlayerPrefs.HasKey("Quality_ID"))
        {
            SetQuality(PlayerPrefs.GetInt("Quality_ID"));
            dropdown.value = PlayerPrefs.GetInt("Quality_ID");
            RotaterTR.GraphicsID = PlayerPrefs.GetInt("Quality_ID");
            ChangeMusic(PlayerPrefs.GetFloat("Music_ID"));
            ChangeSounds(PlayerPrefs.GetFloat("Sounds_ID"));
            dropdown1.value = PlayerPrefs.GetFloat("Music_ID");
            dropdown2.value = PlayerPrefs.GetFloat("Sounds_ID");
        }
        else
        {
            PlayerPrefs.SetInt("Quality_ID", 3);
            SetQuality(PlayerPrefs.GetInt("Quality_ID"));
            dropdown.value = PlayerPrefs.GetInt("Quality_ID");
            RotaterTR.GraphicsID = PlayerPrefs.GetInt("Quality_ID");

            PlayerPrefs.SetFloat("Music_ID", 0f);
            PlayerPrefs.SetFloat("Sounds_ID", 0f);
        }
    }

    public void ChangeMusic(float a)
    {
        sound.SetFloat("Music", a);
        PlayerPrefs.SetFloat("Music_ID", a);
    }

    public void ChangeSounds(float a)
    {
        sound.SetFloat("Sounds", a);
        PlayerPrefs.SetFloat("Sounds_ID", a);
    }


    public TMP_Text[] ss;

    public void SetQuality(float ID)
    {
        if (ID == 0)
        {
            qualityText.text = ss[0].text;
            QualitySettings.SetQualityLevel(0);
            profile.bloom.enabled = false;
            profile.colorGrading.enabled = false;
            profile.ambientOcclusion.enabled = false;
            profile.motionBlur.enabled = false;
            volume.enabled = false;
        }
        else if (ID == 1)
        {
            qualityText.text = ss[1].text;
            QualitySettings.SetQualityLevel(0);
            profile.bloom.enabled = false;
            profile.colorGrading.enabled = true;
            profile.ambientOcclusion.enabled = false;
            profile.motionBlur.enabled = false;
            volume.enabled = true;
        }
        else if (ID == 2)
        {
            qualityText.text = ss[2].text;
            QualitySettings.SetQualityLevel(1);
            profile.bloom.enabled = true;
            profile.colorGrading.enabled = true;
            profile.ambientOcclusion.enabled = false;
            profile.motionBlur.enabled = false;
            volume.enabled = true;
        }
        else if (ID == 3)
        {
            qualityText.text = ss[3].text;
            QualitySettings.SetQualityLevel(2);
            profile.bloom.enabled = true;
            profile.colorGrading.enabled = true;
            profile.ambientOcclusion.enabled = true;
            profile.motionBlur.enabled = false;
            volume.enabled = true;
        }
        else if (ID == 4)
        {
            qualityText.text = ss[4].text;
            QualitySettings.SetQualityLevel(3);
            profile.bloom.enabled = true;
            profile.colorGrading.enabled = true;
            profile.ambientOcclusion.enabled = true;
            profile.motionBlur.enabled = true;
            volume.enabled = true;
        }

        PlayerPrefs.SetInt("Quality_ID", (int)ID);
        RotaterTR.GraphicsID = (int)ID;

        Invoke("dfg", 1f);
    }

    void dfg()
    {
        SetQuality(PlayerPrefs.GetInt("Quality_ID"));
    }
}
