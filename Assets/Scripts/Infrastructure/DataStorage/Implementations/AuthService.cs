using System;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace Infrastructure.DataStorage.Implementations
{
    public class AuthService
    {
        public void Initialize()
        {
            UniTask.Create(Factory);
        }

        private async UniTask Factory()
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

        public bool IsInitialized { get; set; }
    }
}