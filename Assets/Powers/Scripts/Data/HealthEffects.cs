using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Powers
{
    public class HealthEffects : MonoBehaviour
    {
        public HealthController playerHealth;
        public PostProcessProfile gamePostProcess;

        //post process effects to be edited
        private Grain postProcessGrain;
        private ColorGrading postProcessColor;
        private Vignette postProcessVignette;

        private void Start()
        {
            //assign the specific post process variables to the post process profile settings
            postProcessColor = gamePostProcess.GetSetting<ColorGrading>();
            postProcessGrain = gamePostProcess.GetSetting<Grain>();
            postProcessVignette = gamePostProcess.GetSetting<Vignette>();
        }

        // Update is called once per frame
        void Update()
        {
            postProcessColor.saturation.Override(Mathf.Clamp((playerHealth.health / playerHealth.maxHealth) * playerHealth.maxHealth * 2 - 100, -100, 0));
            postProcessGrain.intensity.Override(Mathf.Clamp((-playerHealth.health / playerHealth.maxHealth)  + 0.8f, 0.3f, 0.8f));
            postProcessVignette.smoothness.Override(Mathf.Clamp(-(playerHealth.health / playerHealth.maxHealth) + 0.5f, 0f, 0.5f));
        }
    }

}
