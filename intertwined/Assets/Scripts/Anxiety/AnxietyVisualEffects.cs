using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Anxiety
{
    public class AnxietyVisualEffects : MonoBehaviour
    {
        private PostProcessVolume _volume;
        private Vignette _vignette;

        private AnxietyCalc _anxiety;

        private void Start()
        {
            
            _anxiety = GetComponent<AnxietyCalc>();
            
            _vignette = ScriptableObject.CreateInstance<Vignette>();
            _vignette.enabled.Override(true);
            _vignette.intensity.Override(1f);

            _volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, _vignette);
            
        }

        private void Update()
        {
           _vignette.intensity.value = _anxiety.anxiety / 100f;
        }
        
        private void OnDestroy()
        {
            RuntimeUtilities.DestroyVolume(_volume, true, true);
        }
    }
}