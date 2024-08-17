using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class DeathMenuManager : MonoBehaviour
    {
        [SerializeField] private DeathMenu deathMenu;

        private static bool IsDead { get; set; }
    
        public static DeathMenuManager MenuManager { get; private set; }

        private void Awake()
        {
            IsDead = false;
        
            if (MenuManager != null)
            {
                Debug.Log("An instance of the save manager already exists, destroying the newest one");
                Destroy(gameObject);
                return;
            }
            MenuManager = this;
        
            if (SceneManager.GetActiveScene().buildIndex != 0)
                DontDestroyOnLoad(gameObject);

        }

        // Update is called once per frame
        public void Death()
        {
            if (IsDead) return;

            IsDead = true;
        
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(transform.GetChild(i).gameObject);
            }
        
            deathMenu.DeathSequence();
        }
    
    
    }
}
