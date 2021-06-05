using System.Collections.Generic;
using UI;

namespace Managers
{

	/// <summary>
	/// Base class to controll the flow of screens and popups.
	/// There can only be 1 "active" screen at a time.
	/// Popups are not dismissed when a new appears.
	/// </summary>
	public class UIStackViewManager
	{

		protected Stack<BaseView> StackScreen = new Stack<BaseView>();
		protected List<BaseView> StackPopup = new List<BaseView>();
		protected BaseView CurrentFocus;

		public int GetTotalScreen()
		{
			return StackScreen.Count;
		}
		public int GetTotalPopup()
		{
			return StackPopup.Count;
		}

		/// <summary>
		/// called whenever push/pop a view model, to trigger focus gain and lost to them
		/// </summary>
		protected void UpdateFocus()
		{
			BaseView viewModel;
			if (StackPopup.Count > 0)
			{
				viewModel = GetCurrentPopup();
			}
			else
			{
				viewModel = GetCurrentScreen();
			}

			SetFocus(viewModel);
		}
		protected void SetFocus(BaseView viewModel)
		{

			if (CurrentFocus == viewModel)
			{

			}
			else
			{

				if (CurrentFocus != null)
				{
					//UnityEngine.Debug.Log(string.Format("{0} Lost Focus", m_pCurrentFocus.Name					CurrentFocus.FocusChange(false);
				}

				CurrentFocus = viewModel;

				if (CurrentFocus != null)
				{
					//UnityEngine.Debug.Log(string.Format("{0} Gain Focus", m_pCurrentFocus.Name					CurrentFocus.FocusChange(true);
				}
			}
		}


		public void PopAll()
		{

			for (int i = StackPopup.Count - 1; i >= 0; i--)
			{
				PopPopup(i);
			}

			while (StackScreen.Count > 0)
			{
				PopScreen(StackScreen.Peek());
			}
		}

		public BaseView GetCurrentScreen()
		{
			BaseView screen;

			if (StackScreen.Count > 0)
			{
				screen = StackScreen.Peek();
			}
			else
			{
				screen = null;
			}

			return screen;
		}
		public BaseView GetCurrentPopup()
		{
			BaseView popup;

			if (StackPopup.Count > 0)
			{
				popup = StackPopup[StackPopup.Count - 1];
			}
			else
			{
				popup = null;
			}

			return popup;
		}

		public void PushScreen(BaseView viewModel)
		{
			// is the pushing vm already in?
			if (StackScreen.Contains(viewModel))
			{
				// yes, then pop all other, and go back?

			}
			else
			{

				// has a current screen?
				BaseView current = GetCurrentScreen();
				if (current != null)
				{
					// yes, then hide it for now.
					current.LostScreenFocus();
				}

				// no, then push it
				StackScreen.Push(viewModel);
				viewModel.Open();                  // trigger to open it

				// Update Focus:
				UpdateFocus();
			}

		}

		public bool PopScreen(string sScreenName)
		{
			BaseView viewModel = null;
			foreach (var item in StackScreen.ToArray())
			{
				if (item.Name == sScreenName)
				{
					viewModel = item;
					break;
				}

			}
			return PopScreen(viewModel);
		}

		public bool PopScreen(BaseView viewModel)
		{
			bool success = false;

			// is the poped vm the current?
			if (viewModel != null && GetCurrentScreen() == viewModel)
			{
				// yes, then it is valid.

				// pop, and hide it.
				StackScreen.Pop();
				viewModel.Close();      // all callbaks are cleared here. so call to animate before
				success = true;

				// has next screen?
				BaseView pNext = GetCurrentScreen();
				if (pNext != null)
				{
					// yes, then show it.
					pNext.RefocusScreen();
				}

				UpdateFocus();

			}

			return success;
		}

		public void PushPopup(BaseView viewModel)
		{

			if (!StackPopup.Contains(viewModel))
			{
				StackPopup.Add(viewModel);
				viewModel.Open();                      // trigger to open it
				UpdateFocus();
			}
		}

		public bool PopPopup(string name)
		{

			int iIndex = StackPopup.FindIndex(delegate (BaseView viewModel) {
				return viewModel.Name == name;
			});

			return PopPopup(iIndex);
		}

		public bool PopPopup(BaseView viewModel)
		{
			int index = StackPopup.IndexOf(viewModel);
			return PopPopup(index);
		}

		private bool PopPopup(int index)
		{

			if (index >= 0)
			{
				BaseView viewModel = StackPopup[index];
				StackPopup.RemoveAt(index);
				viewModel.Close();                     // trigger to close it
				UpdateFocus();
				return true;
			}
			else
			{
				return false;
			}
		}


		public BaseView GetFocused()
		{

			return CurrentFocus;

		}

		/// <summary>
		/// Returns true if the given ViewModel is the currently focused VM screen.
		/// If a popup is in front, it will return false.
		/// </summary>
		public bool IsFocusedScreen(BaseView viewModel)
		{

			if (HasPopup())
			{
				return false;
			}
			else
			{

				return GetCurrentScreen() == viewModel;
			}
		}

		/// <summary>
		/// Returns true if the given ViewModel is the currently focused VM popup.
		/// </summary>
		public bool IsFocusedPopup(BaseView viewModel)
		{
			return GetCurrentPopup() == viewModel;
		}

		/// <summary>
		/// Returns true if has a popup open.
		/// </summary>
		public bool HasPopup()
		{
			return StackPopup.Count > 0;
		}

		public string[] GetScreenStack()
		{
			BaseView[] viewModels = StackScreen.ToArray();

			string[] screens = new string[viewModels.Length];
			for (int i = 0; i < screens.Length; i++)
			{
				screens[i] = viewModels[i].Name;
			}

			return screens;
		}
	}
}
