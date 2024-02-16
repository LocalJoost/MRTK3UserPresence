using RealityCollective.ServiceFramework.Definitions;
using RealityCollective.ServiceFramework.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace MRTKExtensions.Services
{
    [CreateAssetMenu(menuName = "MRTKExtensions/UserPresenceServiceProfile", fileName = "UserPresenceServiceProfile",
        order = (int)CreateProfileMenuItemIndices.ServiceConfig)]
    public class UserPresenceServiceProfile : BaseServiceProfile<IServiceModule>
    {
        [SerializeField]
        private InputActionReference gazeTrackingState;
        
        [SerializeField]
        private float userAWayWaitTime = 3.0f;

        [SerializeField]
        private float userPresentWaitTime = 0.5f;
        
        public InputActionReference GazeTrackingState => gazeTrackingState;
        public float UserAWayWaitTime => userAWayWaitTime;
        public float UserPresentWaitTime => userPresentWaitTime;
    }
}