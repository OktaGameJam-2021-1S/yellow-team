using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Factories
{
	/// <summary>
	/// Creates a UI View attached to a certain ViewModel.
	/// </summary>
	public class UIFactory : MonoBehaviour
	{

		public string m_sViewPath = "UIView";   // path to the resources file.;
		public Transform m_tCanvasParent;       // the canvas the factory will instantiate into.

		public List<GameObject> m_lViews;       // cache view in case we have the View already in the scene. Call ScanViews in Editor to refresh list.

		// instaces in cache of views.
		private Dictionary<string, BaseView> m_dViewInstances = new Dictionary<string, BaseView>();

		#region Overrides - Monobehaviour
		private void Awake()
		{

			BaseView pBaseView;
			foreach (var pView in m_lViews)
			{
				pBaseView = pView.GetComponent<BaseView>();
				if (pBaseView != null)
				{

					m_dViewInstances[pView.name] = pBaseView;
				}
				pView.SetActive(false);
			}
		}

		private void Reset()
		{
			m_tCanvasParent = GameObject.FindObjectOfType<Canvas>().transform;
			m_lViews = new List<GameObject>();
			ScanViews();
		}
		#endregion

		/// <summary>
		/// Create or find an existing view, 
		/// and attach to a certain ViewModel.
		/// </summary>
		public BaseView CreateView(string sPrefab, Action<BaseView> delBeforeInit)
		{

			// find existing:
			BaseView pView = FindExistingInstance(sPrefab);

			// found?
			if (pView == null)
			{
				// no, then creante
				string sPath = System.IO.Path.Combine(m_sViewPath, sPrefab);
				BaseView pViewPrefab = Resources.Load<BaseView>(sPath);

				pView = GameObject.Instantiate<BaseView>(pViewPrefab, m_tCanvasParent);
				m_dViewInstances[sPrefab] = pView;
			}

			if (pView != null)
			{
				if (delBeforeInit != null)
				{
					delBeforeInit(pView);
				}
				pView.Init();
			}

			return pView;
		}

		private BaseView FindExistingInstance(string sName)
		{

			BaseView pView;

			m_dViewInstances.TryGetValue(sName, out pView);

			return pView;
		}


		[ContextMenu("Scan Views")]
		private void ScanViews()
		{

			Transform tRoot = m_tCanvasParent;
			int iChild = tRoot.childCount;
			BaseView pView;
			Transform tChild;

			for (int i = 0; i < iChild; i++)
			{
				tChild = tRoot.GetChild(i);
				pView = tChild.GetComponent<BaseView>();
				if (pView != null)
				{

					if (!m_lViews.Contains(tChild.gameObject))
					{

						m_lViews.Add(tChild.gameObject);
					}
				}
			}

			for (int i = m_lViews.Count - 1; i >= 0; i--)
			{
				if (m_lViews[i] == null)
				{
					m_lViews.RemoveAt(i);
				}
			}
		}

	}
}
