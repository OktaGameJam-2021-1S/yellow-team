using UnityEngine;
using System.Collections;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using Factories;
using System;

namespace UI
{

	public class BaseView : MonoBehaviour
	{

		[Header("Animations")]
		[SerializeField] protected PlayableDirector ShowAnimation;      //
		[SerializeField] protected PlayableDirector HideAnimation;      //
		[SerializeField] protected PlayableDirector RefocusAnimation;   //
		[SerializeField] protected PlayableDirector LostFocusAnimation; //
						
		// UI Binds:
		private List<Button> BoundButtonsList = new List<Button>();
		private List<InputField> BoundInputFieldList = new List<InputField>();
		private List<Toggle> BoundToggleList = new List<Toggle>();

		#region Overrides - Monobehaviour

		private void Update()
		{
			
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				ActionDismiss();
			}
			
		}

		#endregion

		public string Name { set; get; }

		private UISpriteFactory SpriteFactory;
		private object Payload;

		/// <summary>
		/// 
		/// </summary>
		protected IEnumerator EnableContent(bool enable)
		{			
			gameObject.SetActive(enable);
			yield break;
		}

		/// <summary>
		/// Wait delay in queue
		/// </summary>
		/// <param name="delay"></param>
		/// <returns></returns>
		protected IEnumerator WaitDelay(float delay)
		{
			yield return new WaitForSeconds(delay);
		}

		/// <summary>
		/// Plays a certain PlayableDirector.
		/// Used for show and hide.
		/// </summary>
		protected IEnumerator PlayTransition(PlayableDirector playable, bool isInverse = false, Action callback = null)
		{

			double currentTime = 0f;
			double duration = playable.duration;

			if (isInverse)
			{
				currentTime = duration;
			}
			if (isInverse)
			{

				while (currentTime > 0f)
				{

					currentTime -= Time.deltaTime;
					playable.time = currentTime;
					playable.Evaluate();
					yield return null;
				}
				if (callback != null) callback();
			}
			else
			{

				while (currentTime < duration)
				{
					currentTime += Time.deltaTime;
					playable.time = currentTime;
					playable.Evaluate();
					yield return null;
				}
				if(callback != null) callback();
			}
		}


		/// <summary>
		/// Clears all the UI elements listeners from buttons, texts, etc.
		/// </summary>
		protected virtual void ClearBinds()
		{
			foreach (var button in BoundButtonsList)
			{
				CancelBind(button);
			}
			foreach (var input in BoundInputFieldList)
			{
				input.onEndEdit.RemoveAllListeners();
			}
			foreach (var toggle in BoundToggleList)
			{
				toggle.onValueChanged.RemoveAllListeners();
			}
		}

		protected void CancelBind(Button button)
		{
			button.onClick.RemoveAllListeners();
		}

		/// <summary>
		/// Bind a button with a OnClick call.
		/// Helps clear all bound buttons later.
		/// </summary>
		protected void Bind(Button button, UnityAction actionOnClick)
		{
			button.onClick.AddListener(actionOnClick);
			BoundButtonsList.Add(button);
		}

		/// <summary>
		/// Binds an unique event to the given button.
		/// The given button will remove all listeners if have been bound before.
		/// Used when re-bind when new dto comes, but no changes to instances are done.
		/// </summary>
		protected void BindUnique(Button button, UnityAction delOnClick)
		{

			if (BoundButtonsList.Contains(button))
			{
				CancelBind(button);
			}
			else
			{
				BoundButtonsList.Add(button);
			}

			button.onClick.AddListener(delOnClick);
		}

		/// <summary>
		/// Bind a input field with a On Edit End call.
		/// Helps clear all bound input fields later.
		/// </summary>
		protected void Bind(InputField inputField, UnityAction<string> actionOnEnd)
		{
			inputField.onEndEdit.AddListener(actionOnEnd);
			BoundInputFieldList.Add(inputField);
		}

		protected void Bind(Toggle toggle, UnityAction<bool> actionOnValueChanged)
		{
			toggle.onValueChanged.AddListener(actionOnValueChanged);
			BoundToggleList.Add(toggle);
		}

		public void SetSpriteFactory(UISpriteFactory spriteFactory)
		{
			SpriteFactory = spriteFactory;
		}


		protected UISpriteFactory GetSpriteFactory()
		{
#if UNITY_EDITOR
			if (SpriteFactory == null)
			{
				SpriteFactory = GameObject.FindObjectOfType<UISpriteFactory>();
				if (SpriteFactory != null)
				{
					// mock for tests, to check only the view without vm.
					Debug.LogWarning("We are using a wrong ref of UISpriteFactory! Should inject it.");
				}
				else
				{
					Debug.LogError("Not found UISpriteFactory!");
				}
			}
#endif
			return SpriteFactory;
		}

		public virtual void SetPayload(object payload)
        {
			Payload = payload;
        }

		protected virtual void ActionDismiss()
        {

        }

		public virtual void Init()
        {

        }

		public virtual void Open()
		{
			gameObject.SetActive(true);
			if (ShowAnimation != null)
			{
				StartCoroutine(PlayTransition(ShowAnimation));
			}
		}

		public virtual void Close()
		{
			if (HideAnimation != null)
			{
				StartCoroutine(PlayTransition(HideAnimation, HideAnimation==ShowAnimation, delegate()
				{
					gameObject.SetActive(false);
				}));
            }
            else
            {
				gameObject.SetActive(false);
			}
		}

		public virtual void RefocusScreen()
        {
			gameObject.SetActive(true);
		}

		public virtual void LostScreenFocus()
        {
			gameObject.SetActive(false);
        }
	}
}