using System;
using System.Collections.Generic; 
using UnityEngine;

namespace GiantRushClone.Utils
{
	
	[ExecuteAlways]
	public class ObjectSpawner : MonoBehaviour, ISerializationCallbackReceiver
	{
		public static List<string> tempList;
		
		[ListToPopup(typeof(ObjectSpawner), "tempList")]
		public string Type;

		private List<string> popupList;
		private SpawnableObjects _objects;
		
		private void Awake()
		{
			if(!Application.IsPlaying(gameObject)) return;
			
			UpdateList();
			
			popupList = _objects.GetAllObjectListTypes();
			tempList = popupList;
			
			Instantiate(_objects.GetRandomGameObjectFromType(Type), transform.position, Quaternion.identity, transform);
		}

		private void Update()
		{
			UpdateList();
		}

		private void UpdateList()
		{
			if (_objects == null)
			{
				_objects = Resources.Load("Objects") as SpawnableObjects;

				if (_objects == null)
				{
					Debug.LogError("No such SpawnableObject named \"Objects\" in Resources folder");
				}
			}
			else
			{
				popupList = _objects.GetAllObjectListTypes();
				tempList = popupList;
			}
		}

		public void OnBeforeSerialize()
		{
			tempList = popupList;
		}

		public void OnAfterDeserialize()
		{
		}
	}
}