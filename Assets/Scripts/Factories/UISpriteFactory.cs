using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Factories
{
	public class UISpriteFactory : MonoBehaviour
	{

		[System.AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
		sealed class AssetPathAttribute : Attribute
		{

			public AssetPathAttribute(string path)
			{
				Path = path;
			}

			public string Path
			{
				get;
			}

		}

		[Header("Resources")]
		[AssetPath("currency/hard")] public Sprite HardIcon;
		[AssetPath("currency/soft")] public Sprite SoftIcon;

		private Dictionary<string, Sprite> SpritesDic;

		#region Overrides - Monobehaviour

		private void Awake()
		{
			InitSpritesRefs();
		}

		#endregion


		private void InitSpritesRefs()
		{
			var fields = GetType().GetFields();


			SpritesDic = new Dictionary<string, Sprite>();
			foreach (var field in fields)
			{
				object[] attributes = field.GetCustomAttributes(typeof(AssetPathAttribute), false);

				if (attributes.Length > 0)
				{
					AssetPathAttribute pAttribute = attributes[0] as AssetPathAttribute;
					SpritesDic[pAttribute.Path] = field.GetValue(this) as Sprite;
				}

			}

		}

		public Sprite GetIcon(string path)
		{

			Sprite sprite;
			if (path != null)
			{
				SpritesDic.TryGetValue(path, out sprite);
			}
			else
			{
				sprite = null;
			}
			return sprite;

		}

	}
}
