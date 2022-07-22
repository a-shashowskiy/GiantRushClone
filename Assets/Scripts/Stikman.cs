using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GiantRushClone
{
    public enum SickmenColor
    {
        Red,
        Green,
        Blue,
        Yellow
    }
    public class Stikman : MonoBehaviour
    {
        public bool randomeColor;
        public SickmenColor colorToSet;
        [SerializeField] private SkinnedMeshRenderer m_Renderer;
        private Material _materialColor;
        
        void Start()
        {
            _materialColor = m_Renderer.material;
            if (randomeColor)
            {
                int i = Random.Range(0, 3);
                switch (i)
                {
                    case 0: _materialColor.color = Color.red; colorToSet = SickmenColor.Red;  break;
                    case 1: _materialColor.color = Color.green; colorToSet = SickmenColor.Green; break;
                    case 2: _materialColor.color = Color.blue; colorToSet = SickmenColor.Blue; break;
                    case 3: _materialColor.color = Color.yellow; colorToSet = SickmenColor.Yellow; break;
                }
            }else
            {
                switch (colorToSet)
                {
                    case SickmenColor.Red: _materialColor.color = Color.red; break;
                    case SickmenColor.Green: _materialColor.color = Color.green; break;
                    case SickmenColor.Blue: _materialColor.color = Color.blue; break;
                    case SickmenColor.Yellow: _materialColor.color = Color.yellow; break;
                }
            }
        }
         
    } }
