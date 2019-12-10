using UnityEngine;


namespace Powers
{
    public class UI_Data : MonoBehaviour
    {
        public GameManager gameManager;

        [Space(10)]

        public GameObject playerHealth;
        private RectTransform playerBar;
        private UnityEngine.UI.Image playerColor;

        public RectTransform healthHolder;

        [Space(10)]

        public GameObject playerAmmo;
        private RectTransform ammoBar;
        private UnityEngine.UI.Image ammoColor;

        [Space(10)]

        public GameObject bossHealth;
        private RectTransform bossBar;
        private UnityEngine.UI.Image bossColor;

        public RectTransform bossHolder;

        private float playerHealthBarSize;
        private float playerAmmoBarSize;
        private float bossHealthBarSize;

        private Vector3 bossHealthPosition;
        private float bossHealthHitReaction = 0;

        private Vector3 playerHealthPosition;
        private float playerHealthHitReaction = 0;

        private void Start()
        {
            playerBar = playerHealth.GetComponent<RectTransform>();
            ammoBar = playerAmmo.GetComponent<RectTransform>();
            bossBar = bossHealth.GetComponent<RectTransform>();

            playerColor = playerHealth.GetComponent<UnityEngine.UI.Image>();
            ammoColor = playerAmmo.GetComponent<UnityEngine.UI.Image>();
            bossColor = bossHealth.GetComponent<UnityEngine.UI.Image>();

            playerHealthBarSize = playerBar.sizeDelta.x;
            playerAmmoBarSize = ammoBar.sizeDelta.x;
            bossHealthBarSize = bossBar.sizeDelta.x;

            bossHealthPosition = bossHolder.anchoredPosition;
            playerHealthPosition = healthHolder.anchoredPosition;
        }

        // Update is called once per frame
        void Update()
        {
            #region Player Health

            //set the size of the player health box
            playerBar.sizeDelta = new Vector2((gameManager.player.health / gameManager.player.maxHealth) * playerHealthBarSize, playerBar.sizeDelta.y);

            //set the color of the player health box depending on if the player got hit
            playerColor.color = Color.Lerp(playerColor.color, new Color(1,0, 0, 0.85f), 0.05f);
            if (gameManager.player.gotHit && !gameManager.player.gotHitLast) playerColor.color = new Color(1,1,1,1);

            //if the player got hit, set hit reaction float to 15
            if (gameManager.player.gotHit && !gameManager.player.gotHitLast) playerHealthHitReaction = 10;

            //randomize player health position depending on hit reaction float
            healthHolder.anchoredPosition = new Vector3(playerHealthPosition.x + Random.Range(-playerHealthHitReaction, playerHealthHitReaction), playerHealthPosition.y + Random.Range(-playerHealthHitReaction, playerHealthHitReaction), playerHealthPosition.z);

            //ensure player health hit reaction is stable
            playerHealthHitReaction -= playerHealthHitReaction * 2 * Time.deltaTime;
            if (playerHealthHitReaction < 0.1f) playerHealthHitReaction = 0;
            playerHealthHitReaction = Mathf.Clamp(playerHealthHitReaction, 0, 10);


            #endregion

            #region Player Ammo

            //set the size of the ammo box
            ammoBar.sizeDelta = new Vector2((gameManager.ammo.currentClip / gameManager.ammo.clipSize) * playerAmmoBarSize, ammoBar.sizeDelta.y);

            if (gameManager.ammo.canShoot) ammoColor.color = Color.Lerp(ammoColor.color, new Color(0.38f, 0.7f, 0.38f, 0.85f), 0.05f); 
            else if (!gameManager.ammo.canShoot) ammoColor.color = new Color(1, 1, 1, 0.85f);

            #endregion

            #region Boss Health

            //set the size of the boss health box
            bossBar.sizeDelta = new Vector2((gameManager.boss.health / gameManager.boss.maxHealth) * bossHealthBarSize, bossBar.sizeDelta.y);

            //set the color of the boss health box depending on if the player got hit
            bossColor.color = Color.Lerp(bossColor.color, new Color(0.85f, 0.1f, 0.2f, 0.85f), 0.05f);
            if (gameManager.boss.gotHit && !gameManager.boss.gotHitLast) bossColor.color = new Color(1, 1, 1, 1);

            //if the boss got hit, set hit reaction float to 15
            if (gameManager.boss.gotHit && !gameManager.boss.gotHitLast) bossHealthHitReaction = 10;

            //randomize boss health position depending on hit reaction float
            bossHolder.anchoredPosition = new Vector3(bossHealthPosition.x + Random.Range(-bossHealthHitReaction, bossHealthHitReaction), bossHealthPosition.y + Random.Range(-bossHealthHitReaction, bossHealthHitReaction), bossHealthPosition.z);

            //ensure boss health hit reaction is stable
            bossHealthHitReaction -= bossHealthHitReaction*2 * Time.deltaTime;
            if (bossHealthHitReaction < 0.1f) bossHealthHitReaction = 0;
            bossHealthHitReaction = Mathf.Clamp(bossHealthHitReaction, 0, 10);

            #endregion
        }
    }

}
