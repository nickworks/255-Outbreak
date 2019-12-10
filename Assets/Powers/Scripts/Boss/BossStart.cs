using System.Collections;
using UnityEngine;

namespace Powers
{
    public class BossStart : MonoBehaviour
    {
        private Animation fenceClose;
        public AnimationClip fenceCloseClip;
        public GameObject bossObject;
        public CanvasGroup bossHealth;

        public AudioSource audioSource;
        public AudioClip music;

        private float fadeCurrentVelocity;

        private float audioCurrentVelocity;
        private float volume = 0;

        private void Start()
        {
            //prepare level for gameplay
            fenceClose = gameObject.GetComponent<Animation>();
            bossHealth.alpha = 0;
            bossObject.SetActive(false);
            audioSource.volume = volume;
        }

        private void OnTriggerEnter(Collider collider)
        {
            //if the player collides with the trigger, disable it and start the boss fight.
            if (collider.tag == "Player")
            {
                BoxCollider box = GetComponent<BoxCollider>();
                box.enabled = false;

                StartCoroutine(BossIntro());
            }
        }

        IEnumerator BossIntro()
        {
            if (!Game.isPaused)
            {
                //play the animation to close the fence
                fenceClose.clip = fenceCloseClip;
                fenceClose.Play();

                //wait as long as the fence close clip is
                yield return new WaitForSeconds(fenceCloseClip.length);

                //enable the boss
                bossObject.SetActive(true);

                //prepare the audio source to be played
                audioSource.loop = true;
                audioSource.clip = music;
                audioSource.Play();

                //fade in the boss health script
                while (bossHealth.alpha != 1 || audioSource.volume != 0.5f)
                {
                    if(bossHealth.alpha != 1)
                    {
                        bossHealth.alpha = Mathf.SmoothDamp(bossHealth.alpha, 1, ref fadeCurrentVelocity, 2, 999, Time.deltaTime);
                        Mathf.Clamp(bossHealth.alpha, 0, 1);
                        if (bossHealth.alpha >= 0.99f) bossHealth.alpha = 1;
                        yield return null;
                    }
                    if(audioSource.volume != 0.5f)
                    {
                        volume = Mathf.SmoothDamp(volume, 0.5f, ref audioCurrentVelocity, 6, 999, Time.deltaTime);
                        Mathf.Clamp(volume, 0, 0.5f);
                        if (volume >= 0.49f) volume = 0.5f;
                        audioSource.volume = volume;
                        yield return null;
                    }
                }


                yield break;
            }
            
        }
    }

}
