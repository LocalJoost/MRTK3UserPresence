using System.Threading.Tasks;
using MRTKExtensions.Services;
using RealityCollective.ServiceFramework.Services;
using TMPro;
using UnityEngine;

public class UserPresenceDisplayer : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro displayText;

    [SerializeField]
    AudioClip userPresentClip;

    [SerializeField]
    private AudioClip userAwayClip;

    private AudioSource audioSource;
    private IUserPresenceService userPresenceService;

    private async Task Start()
    {
        audioSource = GetComponent<AudioSource>();
        await ServiceManager.WaitUntilInitializedAsync();
        userPresenceService = ServiceManager.Instance.GetService<IUserPresenceService>();
        userPresenceService.UserPresenceChanged.AddListener(OnUserPresenceChanged);
    }

    private void OnUserPresenceChanged(bool currentPresence)
    {
        displayText.text = $"User is {(currentPresence ? "present" : "away")}";
        audioSource.PlayOneShot(currentPresence ? userPresentClip : userAwayClip);
    }

    private void OnDestroy()
    {
        if (userPresenceService != null)
        {
            userPresenceService.UserPresenceChanged.RemoveListener(OnUserPresenceChanged);
        }
    }
}