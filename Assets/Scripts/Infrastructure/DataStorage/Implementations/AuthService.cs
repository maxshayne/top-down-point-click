using System;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace Infrastructure.DataStorage.Implementations
{
    public class AuthService
    {
        public async void Initialize()
        {
            try
            {
                await UnityServices.InitializeAsync();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return;
            }
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return;
            }

            IsInitialized = true;
        }
        
        public bool IsInitialized { get; private set; }
    }
}