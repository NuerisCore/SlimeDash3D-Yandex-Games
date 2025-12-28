using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using YG;

public class Interface : MonoBehaviour
{
    public static Interface instance;

    public int _points = 0;
    public int _addPointCount = 1;
    public bool _canAddPoint;

    public bool _isShieldON;
    public bool _isDoubleJump;

    public TMP_Text _pointText;
    public TMP_Text _coinsText;

    public Image _humpFillAmount;
    public Image _shieldFillAmount;

    public float humpFill = 1f;
    public float shieldFill = 1f;

    public static bool CanUse;
    public static bool CanUse2;

    public Menu men;

    private void Update()
    {
        if (CanUse)
        {
            CanUse = false;
            men.SetQuality(PlayerPrefs.GetInt("Quality_ID"));
        }

        if (_isDoubleJump && humpFill < 26f) humpFill += Time.deltaTime;
        else if (!_isDoubleJump) humpFill = 0f;
        if (_isShieldON && shieldFill < 46f) shieldFill += Time.deltaTime;
        else if (!_isShieldON) shieldFill = 0f;

        _humpFillAmount.fillAmount = ((humpFill / 25f) - 1f) * -1f;
        _shieldFillAmount.fillAmount = ((shieldFill / 45f) - 1f) * -1f;

        if (Input.GetKeyDown(KeyCode.F) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void Jum()
    {
        if (humpFill < 25f) return;

        humpFill = 0f;

        Player.player._CanJump2 = true;
    }

    public void Shi()
    {
        if (shieldFill < 45f) return;

        shieldFill = 0f;

        Player.player._isShield = true;
    }

    public void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void AddToCoins(int s)
    {
        YandexGame.savesData.money += s;
    }

    private void Start()
    {
        instance = this;
        Sicl();

        if (!CanUse2)
        {
            CanUse2 = true;
            YandexGame.LoadCloud();
        }

        if (CanUse)
        {
            CanUse = false;
            men.SetQuality(PlayerPrefs.GetInt("Quality_ID"));
        }
    }

    public GameObject[] _cans;

    public void Activate(int y)
    {
        if (y == 2)
        {
            if (_isShieldON)
            {
                _cans[y].SetActive(false);
                _isShieldON = false;
                YandexGame.savesData.money += 40;
            }
            else if (YandexGame.savesData.money > 39)
            {
                _cans[y].SetActive(true);
                _isShieldON = true;
                YandexGame.savesData.money -= 40;
            }
        }
        else if (y == 3)
        {
            if (_isDoubleJump)
            {
                _cans[y].SetActive(false);
                _isDoubleJump = false;
                YandexGame.savesData.money += 20;
            }
            else if (YandexGame.savesData.money > 19)
            {
                _cans[y].SetActive(true);
                _isDoubleJump = true;
                YandexGame.savesData.money -= 20;
            }
        }
    }

    public Image[] images;

    private bool ssss;

    public void Pouse(bool d = false)
    {
        if (ssss && d) return;
        else if (d) ssss = true;

        if (Time.timeScale == 0f)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].color = new Color(0f, 0f, 0f, 0f);
            }

            Time.timeScale = 1f;
            if (!Player.player._isStoped) _canAddPoint = true;
        }
        else
        {
            Time.timeScale = 0f;
            if (!Player.player._isStoped) _canAddPoint = false;
        }
    }

    public void LateUpdate()
    {
        _pointText.text = _points.ToString();

        int dist = (int)Vector2Int.Distance(new Vector2Int(0, (int.Parse(_coinsText.text))), new Vector2Int(0, YandexGame.savesData.money));
        int parsed = int.Parse(_coinsText.text);

        if (YandexGame.savesData.money == 0) _coinsText.text = YandexGame.savesData.money.ToString();
        else if (YandexGame.savesData.money == int.Parse(_coinsText.text))
        {
            _coinsText.text = YandexGame.savesData.money.ToString();
            _coinsText.color = Color.Lerp(_coinsText.color, Color.white, 1f);
        }
        else if (YandexGame.savesData.money > int.Parse(_coinsText.text))
        {
            _coinsText.text = (parsed + dist / 4 + 1).ToString();
            _coinsText.color = Color.Lerp(_coinsText.color, Color.green, 1f);
        }
        else if (YandexGame.savesData.money < int.Parse(_coinsText.text))
        {
            _coinsText.text = (parsed - dist / 4 - 1).ToString();
            _coinsText.color = Color.Lerp(_coinsText.color, Color.red, 1f);
        }
    }

    private void Sicl()
    {
        Invoke("Sicl", 0.5f);
        AddPoint(_addPointCount);
    }

    public void AddPoint(int point = 0)
    {
        if (_canAddPoint)
        {
            _points += point;
        }
    }

    public void StartGame()
    {
        _canAddPoint = true;
        Controller.instance.enabled = true;
        Controller.instance.SpawnFloor();

        humpFill = 25f;
        shieldFill = 45f;

        Invoke("ed", 0.2f);
    }

    private void ed()
    {
        YandexGame.SaveCloud();
    }
}
