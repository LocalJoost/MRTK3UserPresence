using RealityCollective.ServiceFramework.Interfaces;
using UnityEngine.Events;

namespace MRTKExtensions.Services
{
    public interface IUserPresenceService : IService
    {
        public bool IsUserPresent { get; }
        public UnityEvent<bool> UserPresenceChanged { get; } 
    }
}