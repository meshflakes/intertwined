using System.Collections.Generic;
using Character;
using UnityEngine;

namespace Prompts
{
    public class PromptManager : MonoBehaviour
    {
        private Transform _boyTransform;
        private Transform _dogTransform;
        
        private Transform _canvasTransform;

        public List<Sprite> sprites;

        private Prompt _boyCurrentPrompt;
        private float _boyPromptTimeout;
        private Prompt _dogCurrentPrompt;
        private float _dogPromptTimeout;
        
        private void Start()
        {
            _canvasTransform = GameObject.FindGameObjectWithTag("PromptsCanvas").transform;
            _boyTransform = GameObject.FindGameObjectWithTag("Boy").transform.parent;
            _dogTransform = GameObject.FindGameObjectWithTag("Dog").transform.parent;
        }
        
        private void Update()
        {
            DeleteExpiredPrompts();
            RepositionActivePrompts();
        }

        private void DeleteExpiredPrompts()
        {
            if (Time.time > _boyPromptTimeout && _boyCurrentPrompt != null)
            {
                _boyCurrentPrompt.DestroyPrompt();
                _boyCurrentPrompt = null;
            }
            if (Time.time > _dogPromptTimeout && _dogCurrentPrompt != null)
            {
                _dogCurrentPrompt.DestroyPrompt();
                _dogCurrentPrompt = null;
            } 
        }

        private void RepositionActivePrompts()
        {
            if (_boyCurrentPrompt != null)
            {
                _boyCurrentPrompt.UpdatePromptPosition();
            }
            if (_dogCurrentPrompt != null)
            {
                _dogCurrentPrompt.UpdatePromptPosition();
            }
        }

        public void RegisterNewPrompt(CharType character, float duration, PromptType prompt )
        {
            if (character == CharType.Boy)
            {
                DestroyCurrentPrompt(CharType.Boy);
                _boyPromptTimeout = Time.time + duration;
                _boyCurrentPrompt = new Prompt(sprites[(int)prompt], _boyTransform, _canvasTransform);
            }
            else if (character == CharType.Dog)
            {
                DestroyCurrentPrompt(CharType.Dog);
                _dogPromptTimeout = Time.time + duration;
                _dogCurrentPrompt = new Prompt(sprites[(int)prompt], _dogTransform, _canvasTransform);
            }
        }

        public bool HasActivePrompt(CharType character)
        {
            if (character == CharType.Boy)
            {
                return _boyCurrentPrompt != null;
            }
            else
            {
                return _dogCurrentPrompt != null;
            }
        }

        private void DestroyCurrentPrompt(CharType character)
        {
            if (character == CharType.Boy && _boyCurrentPrompt != null)
            {
                _boyCurrentPrompt.DestroyPrompt();
                _boyCurrentPrompt = null;
            }
            if (character == CharType.Dog && _dogCurrentPrompt != null)
            {
                _dogCurrentPrompt.DestroyPrompt();
                _dogCurrentPrompt = null;
            } 
        }

        // Destroy prompts that are related to anxiety if they exist
        public void DestroyAnxietyPrompts()
        {
            if (_boyCurrentPrompt != null && _boyCurrentPrompt.GetSprite() == sprites[(int) PromptType.Dog])
            {
                _boyCurrentPrompt.DestroyPrompt();
                _boyCurrentPrompt = null;
            }
            if (_dogCurrentPrompt != null && _dogCurrentPrompt.GetSprite() == sprites[(int) PromptType.Boy])
            {
                _dogCurrentPrompt.DestroyPrompt();
                _dogCurrentPrompt = null;
            }
        }
    }
}