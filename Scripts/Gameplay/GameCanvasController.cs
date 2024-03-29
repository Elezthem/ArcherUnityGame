﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MadsBangH.ArcheryGame
{
	public class GameCanvasController : MonoBehaviour
	{
		private static readonly int ScoreBumpedHash = Animator.StringToHash("Score Bumped");
		private static readonly int VisibleHash = Animator.StringToHash("Visible");

		[SerializeField]
		[Tooltip("Label to update text of when score is increased")]
		private TMP_Text scoreLabel = default;
		[SerializeField]
		[Tooltip("Animator to trigger \"Score Bumped\" on when score is increased")]
		private Animator scoreLabelAnimator = default;
		[SerializeField]
		private Animator startGameLabelAnimator = default;
		[SerializeField]
		private Animator gameOverLabelsAnimator = default;
		[SerializeField]
		private TMP_Text gameOverScoreLabel = default;

		private string gameOverScoreTextTemplate;

		public GameObject  btn_start; 
		public GameObject panel_loading;

		private void Start()
		{
			gameOverScoreTextTemplate = gameOverScoreLabel.text;
			btn_start.SetActive(false);
		}

		private void OnEnable()
		{
			ArcheryGame.ScoreIncreased += ArcheryGame_ScoreChanged;
			ArcheryGame.GameLost += ArcheryGame_GameLost;
			ArcheryGame.NewGameStarted += ArcheryGame_NewGameStarted;
			ArcheryGame.FirstTargetWasShot += ArcheryGame_FirstTargetWasShot;
		}

		private void OnDisable()
		{
			ArcheryGame.ScoreIncreased -= ArcheryGame_ScoreChanged;
			ArcheryGame.GameLost -= ArcheryGame_GameLost;
			ArcheryGame.NewGameStarted -= ArcheryGame_NewGameStarted;
			ArcheryGame.FirstTargetWasShot -= ArcheryGame_FirstTargetWasShot;
		}

		private void ArcheryGame_ScoreChanged()
		{
			scoreLabel.text = ArcheryGame.CurrentScore.ToString();
			scoreLabelAnimator.SetTrigger(ScoreBumpedHash);
		}

		private void ArcheryGame_GameLost()
		{
			btn_start.SetActive(true);
			gameOverScoreLabel.text = gameOverScoreTextTemplate
				.Replace("{score}", ArcheryGame.CurrentScore.ToString())
				.Replace("{highscore}", ArcheryGame.Highscore.ToString());

			gameOverLabelsAnimator.SetBool(VisibleHash, true);
			scoreLabelAnimator.SetBool(VisibleHash, false);
			
			//AdManager.instance.show_ads_ingames();
		}

		private void ArcheryGame_NewGameStarted()
		{
			gameOverLabelsAnimator.SetBool(VisibleHash, false);
			startGameLabelAnimator.SetBool(VisibleHash, true);
		}

		private void ArcheryGame_FirstTargetWasShot()
		{
			startGameLabelAnimator.SetBool(VisibleHash, false);
			scoreLabelAnimator.SetBool(VisibleHash, true);
		}
		public void click_home()
		{
			panel_loading.SetActive(true);
            SceneManager.LoadSceneAsync(0 , LoadSceneMode.Single);	
		}
		public void click_restart()
		{
            SceneManager.LoadSceneAsync(2 , LoadSceneMode.Single);
		
		}
	}
}
