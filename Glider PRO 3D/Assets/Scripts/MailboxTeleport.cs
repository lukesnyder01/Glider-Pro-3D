using UnityEngine;

public class MailboxTeleport : MonoBehaviour
{


    public MailboxTeleport targetMailbox;

    public GameObject player;
    public GliderController gliderController;

    public Transform localTeleportTarget;

    void Start()
    {
        player = GameObject.Find("Player");
        gliderController = player.GetComponent<GliderController>();
    }

    public void StartTeleport()
    {
        if (targetMailbox == null)
        {
            Debug.Log("No target mailbox");
            return;
        }
        else
        {
            Debug.Log("Teleporting player");
            TeleportPlayer(targetMailbox);
        }
      
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartTeleport();
        }
    }

    void TeleportPlayer(MailboxTeleport target)
    {
        Vector3 targetPos = targetMailbox.localTeleportTarget.position;
        Quaternion targetRot = targetMailbox.localTeleportTarget.rotation;
        Debug.Log(targetPos);

        gliderController.TeleportGlider(targetPos, targetRot);

    }

}
