// GENERATED AUTOMATICALLY FROM 'Assets/InputActions/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace InputActions
{
    public class @PlayerInputActions : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerInputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""726e86e5-bd13-489f-90e2-cb6fd8b7653c"",
            ""actions"": [
                {
                    ""name"": ""Two Handed Manipulation"",
                    ""type"": ""Button"",
                    ""id"": ""81fcb387-af9c-4f2f-85e7-d7157f611672"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GrabLeftHand"",
                    ""type"": ""Button"",
                    ""id"": ""711b34c5-c506-4e10-826b-6f76df6a3a49"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GrabRightHand"",
                    ""type"": ""Button"",
                    ""id"": ""4d1b25f7-a317-43bf-bc7d-b1ade1b6e4ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Two grips pressed"",
                    ""id"": ""507d68e7-1ad2-4c4e-9b1c-66ff995fb297"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Two Handed Manipulation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Modifier"",
                    ""id"": ""c5f0adbc-6dc8-4355-9aef-fa13ccf52fb9"",
                    ""path"": ""<XRController>{RightHand}/gripPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Two Handed Manipulation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Button"",
                    ""id"": ""36581aa0-4edc-4bf5-bc32-4bbd176299ca"",
                    ""path"": ""<XRController>{LeftHand}/gripPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Two Handed Manipulation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""32bf7708-3edc-48c7-be9f-0f32bafd750c"",
                    ""path"": ""<XRController>{LeftHand}/triggerPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GrabLeftHand"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8b3592e-de5b-4ce8-8f7b-6259042ffaf4"",
                    ""path"": ""<XRController>{RightHand}/triggerPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GrabRightHand"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_TwoHandedManipulation = m_Player.FindAction("Two Handed Manipulation", throwIfNotFound: true);
            m_Player_GrabLeftHand = m_Player.FindAction("GrabLeftHand", throwIfNotFound: true);
            m_Player_GrabRightHand = m_Player.FindAction("GrabRightHand", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Player
        private readonly InputActionMap m_Player;
        private IPlayerActions m_PlayerActionsCallbackInterface;
        private readonly InputAction m_Player_TwoHandedManipulation;
        private readonly InputAction m_Player_GrabLeftHand;
        private readonly InputAction m_Player_GrabRightHand;
        public struct PlayerActions
        {
            private @PlayerInputActions m_Wrapper;
            public PlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @TwoHandedManipulation => m_Wrapper.m_Player_TwoHandedManipulation;
            public InputAction @GrabLeftHand => m_Wrapper.m_Player_GrabLeftHand;
            public InputAction @GrabRightHand => m_Wrapper.m_Player_GrabRightHand;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
                {
                    @TwoHandedManipulation.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTwoHandedManipulation;
                    @TwoHandedManipulation.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTwoHandedManipulation;
                    @TwoHandedManipulation.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTwoHandedManipulation;
                    @GrabLeftHand.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrabLeftHand;
                    @GrabLeftHand.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrabLeftHand;
                    @GrabLeftHand.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrabLeftHand;
                    @GrabRightHand.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrabRightHand;
                    @GrabRightHand.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrabRightHand;
                    @GrabRightHand.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrabRightHand;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @TwoHandedManipulation.started += instance.OnTwoHandedManipulation;
                    @TwoHandedManipulation.performed += instance.OnTwoHandedManipulation;
                    @TwoHandedManipulation.canceled += instance.OnTwoHandedManipulation;
                    @GrabLeftHand.started += instance.OnGrabLeftHand;
                    @GrabLeftHand.performed += instance.OnGrabLeftHand;
                    @GrabLeftHand.canceled += instance.OnGrabLeftHand;
                    @GrabRightHand.started += instance.OnGrabRightHand;
                    @GrabRightHand.performed += instance.OnGrabRightHand;
                    @GrabRightHand.canceled += instance.OnGrabRightHand;
                }
            }
        }
        public PlayerActions @Player => new PlayerActions(this);
        public interface IPlayerActions
        {
            void OnTwoHandedManipulation(InputAction.CallbackContext context);
            void OnGrabLeftHand(InputAction.CallbackContext context);
            void OnGrabRightHand(InputAction.CallbackContext context);
        }
    }
}
