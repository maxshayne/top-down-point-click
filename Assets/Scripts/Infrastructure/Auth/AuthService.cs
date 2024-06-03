using System;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace Infrastructure.Auth
{
    public class AuthService
    {
        public bool IsInitialized { get; private set; }
        
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
    }
}