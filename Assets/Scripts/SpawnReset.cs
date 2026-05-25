using UnityEngine;
using System.Collections;

public class SpawnReset : MonoBehaviour
{
    public Transform respawnPoint;
    public CanvasGroup fadeGroup;
    public float fadeDuration = 0.5f;
    public float stayBlackDuration = 0.2f;

    private bool isResetting = false;

    private void Start()
    {
        if (fadeGroup == null)
        {
            GameObject go = GameObject.Find("FadeImage");
            if (go != null) fadeGroup = go.GetComponent<CanvasGroup>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isResetting) return;

        if (other.CompareTag("Player") || other.GetComponent<KidsController>() != null)
        {
            StartCoroutine(ResetSequence(other.gameObject));
        }
    }

    private IEnumerator ResetSequence(GameObject player)
    {
        isResetting = true;

        // Play animation if available
        Animator anim = player.GetComponent<Animator>();
        if (anim != null)
        {
            // We use damage or fall as a "falling into water" reaction
            anim.SetTrigger("DamageTrigger"); 
        }

        // Fade Out (To Black)
        if (fadeGroup != null)
        {
            float elapsed = 0;
            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                fadeGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
                yield return null;
            }
            fadeGroup.alpha = 1;
        }

        yield return new WaitForSeconds(stayBlackDuration);

        // Teleport
        TeleportPlayer(player);

        yield return new WaitForSeconds(stayBlackDuration);

        // Fade In (To Game)
        if (fadeGroup != null)
        {
            float elapsed = 0;
            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                fadeGroup.alpha = Mathf.Clamp01(1 - (elapsed / fadeDuration));
                yield return null;
            }
            fadeGroup.alpha = 0;
        }

        isResetting = false;
    }

    private void TeleportPlayer(GameObject player)
    {
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false;
            player.transform.position = respawnPoint.position;
            player.transform.rotation = respawnPoint.rotation;
            cc.enabled = true;
        }
        else
        {
            player.transform.position = respawnPoint.position;
            player.transform.rotation = respawnPoint.rotation;
        }
        
        Debug.Log("Player reset to spawn point.");
    }
}
