using RealityCollective.ServiceFramework.Services;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace MRTKExtensions.Services
{
    [System.Runtime.InteropServices.Guid("9105a4ec-337e-48fa-8bf0-c9c86053d9ea")]
    public class UserPresenceService : BaseServiceWithConstructor, IUserPresenceService
    {
        private readonly UserPresenceServiceProfile profile;
        private readonly InputActionReference gazeTrackingState;
        private bool isInitialized = false;
        private float lastStateChangeTime;
        private bool lastRequestedState = false;
        private int gazeStateResult = 0;
        
        public bool IsUserPresent { get; private set; }
        public UnityEvent<bool> UserPresenceChanged { get; } = new();
        
        public UserPresenceService(string name, uint priority, UserPresenceServiceProfile profile)
            : base(name, priority)
        {
            this.profile = profile;
            gazeTrackingState = profile.GazeTrackingState;
        }

        public override void Enable()
        {
            if (isInitialized)
            {
                return;
            }

            isInitialized = true;
            gazeTrackingState.action.performed += GazeTrackingStateChanged;
        }

        private void GazeTrackingStateChanged(InputAction.CallbackContext ctx)
        {
            gazeStateResult = ctx.ReadValue<int>();
        }

        public override void Update()
        {
            var newState = gazeStateResult == 3;
            if (newState == IsUserPresent)
            {
                lastRequestedState = newState;
                return;
            }
            
            if( newState != lastRequestedState)
            {
                lastRequestedState = newState;
                lastStateChangeTime = Time.time;
            }
            
            if( Time.time - lastStateChangeTime > (lastRequestedState ? profile.UserPresentWaitTime : profile.UserAwayWaitTime))
            {
                IsUserPresent = lastRequestedState;
                UserPresenceChanged.Invoke(IsUserPresent);
            }
        }

        public override void Destroy()
        {
            gazeTrackingState.action.performed -= GazeTrackingStateChanged;
        }
    }
}
