﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
    [Space(20)]
	public Text _txtFragments;
	public Text _txtTime;
    [Space(20)]
    public int Stars;
	public int Fragments;
	public int LevelSecond;
	public int levelIndex;
    [Space(20)]
    public GhostManager ghost;
    public GameObject completeLevelUI;
    public GameObject UIActive;
	

	int scoreAtualDoNivel;
	int scoreMaximoSalvo;
	float secondsCount;

	private void Awake()
	{
		if (!Instance)
		{
			Instance = this;
		}
		Stars = PlayerPrefs.GetInt("Stars");
		Fragments = PlayerPrefs.GetInt("Fragments");
	}

	private void Start()
	{
		//ghost.recording = true;		
		_txtFragments.text = Fragments.ToString();
		levelIndex = SceneManager.GetActiveScene().buildIndex;
        if (SceneManager.sceneCount == 1)
		{
			secondsCount = 0;
		}
	}

    void Update()
    {
        UpdateTimerUI();
        ChangeCursorLock();
		scoreMaximoSalvo = PlayerPrefs.GetInt(levelIndex + "Stars");
		_txtFragments.text = Fragments.ToString();
    }

    string FormatTime(float time)
	{
		int intTime = (int)time;
		int minutes = intTime / 60;
		int seconds = intTime % 60;
		float fraction = time * 1000;
		fraction = (fraction % 1000);
		string timeText = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
		return timeText;
	}

    public void ChangeCursorLock()
    {
        if(completeLevelUI.activeSelf || UIActive.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
    }

    void UpdateTimerUI()
	{
		secondsCount += Time.deltaTime;
		_txtTime.text = FormatTime(secondsCount);
	}

	public void AddStars()
	{
		if (secondsCount <= LevelSecond)
		{
			scoreAtualDoNivel = 3;
			print("3 estrelas");
		}
		else if (secondsCount <= LevelSecond + 10)
		{
			scoreAtualDoNivel = 2;
			print("2 estrelas");
		}
		else
		{
			scoreAtualDoNivel = 1;
			print("1 estrelas");
		}

		if (PlayerPrefs.HasKey(levelIndex + "Stars"))
        {
			switch (scoreAtualDoNivel)
			{
				case 2:
					if (scoreMaximoSalvo == 1)
					{
						Stars += 1;
						scoreMaximoSalvo = 2;
						PlayerPrefs.SetInt("Stars", Stars);
						PlayerPrefs.SetInt(levelIndex + "Stars", scoreMaximoSalvo);
					}
					break;

				case 3:
					if (scoreMaximoSalvo == 2)
					{
						Stars += 1;
						scoreMaximoSalvo = 3;

						PlayerPrefs.SetInt(levelIndex + "Stars", scoreMaximoSalvo);
						PlayerPrefs.SetInt("Stars", Stars);
					}
					if (scoreMaximoSalvo == 1)
					{
						scoreMaximoSalvo = 3;
						Stars += 2;
						PlayerPrefs.SetInt(levelIndex + "Stars", scoreMaximoSalvo);
						PlayerPrefs.SetInt("Stars", Stars);
					}
					break;
			}
			scoreMaximoSalvo = scoreAtualDoNivel;
		}
        else
        {
			scoreMaximoSalvo = scoreAtualDoNivel;
			Stars += scoreMaximoSalvo;

			print("existe");
			PlayerPrefs.SetInt(levelIndex + "Stars", scoreMaximoSalvo);
			PlayerPrefs.SetInt("Stars", Stars);
		}

	}

	public void AddFragments(int value)
	{
		Fragments += value;
		_txtFragments.text = Fragments.ToString();
		PlayerPrefs.SetInt("Fragments", Fragments);
	}
}
