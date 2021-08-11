// GENERATED AUTOMATICALLY FROM 'Assets/InputActions/UIInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace InputActions
{
    public class @UIInputActions : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @UIInputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""UIInputActions"",
    ""maps"": [
        {
            ""name"": ""PauseMenu"",
            ""id"": ""60173227-978e-4821-bf12-a5bb81b3d0f2"",
            ""actions"": [
                {
                    ""name"": ""TogglePauseMenu"",
                    ""type"": ""Button"",
                    ""id"": ""47d35b62-831a-4ffb-8809-14ae0a121079"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""be46ccec-1738-4a9d-9650-cf0356ffe3a7"",
                    ""path"": ""<XRController>{RightHand}/thumbstickClicked"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TogglePauseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""DeveloperConsole"",
            ""id"": ""4af6fdb4-e28e-4163-9cd5-f63048c95c82"",
            ""actions"": [
                {
                    ""name"": ""ToggleDeveloperConsole"",
                    ""type"": ""Button"",
                    ""id"": ""f382c666-1bf2-42c7-8ebe-2e41e893e645"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2e5c4a23-2c9c-461f-be9b-ba290d459f43"",
                    ""path"": ""<XRController>{LeftHand}/thumbstickClicked"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleDeveloperConsole"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // PauseMenu
            m_PauseMenu = asset.FindActionMap("PauseMenu", throwIfNotFound: true);
            m_PauseMenu_TogglePauseMenu = m_PauseMenu.FindAction("TogglePauseMenu", throwIfNotFound: true);
            // DeveloperConsole
            m_DeveloperConsole = asset.FindActionMap("DeveloperConsole", throwIfNotFound: true);
            m_DeveloperConsole_ToggleDeveloperConsole = m_DeveloperConsole.FindAction("ToggleDeveloperConsole", throwIfNotFound: true);
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

        // PauseMenu
        private readonly InputActionMap m_PauseMenu;
        private IPauseMenuActions m_PauseMenuActionsCallbackInterface;
        private readonly InputAction m_PauseMenu_TogglePauseMenu;
        public struct PauseMenuActions
        {
            private @UIInputActions m_Wrapper;
            public PauseMenuActions(@UIInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @TogglePauseMenu => m_Wrapper.m_PauseMenu_TogglePauseMenu;
            public InputActionMap Get() { return m_Wrapper.m_PauseMenu; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PauseMenuActions set) { return set.Get(); }
            public void SetCallbacks(IPauseMenuActions instance)
            {
                if (m_Wrapper.m_PauseMenuActionsCallbackInterface != null)
                {
                    @TogglePauseMenu.started -= m_Wrapper.m_PauseMenuActionsCallbackInterface.OnTogglePauseMenu;
                    @TogglePauseMenu.performed -= m_Wrapper.m_PauseMenuActionsCallbackInterface.OnTogglePauseMenu;
                    @TogglePauseMenu.canceled -= m_Wrapper.m_PauseMenuActionsCallbackInterface.OnTogglePauseMenu;
                }
                m_Wrapper.m_PauseMenuActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @TogglePauseMenu.started += instance.OnTogglePauseMenu;
                    @TogglePauseMenu.performed += instance.OnTogglePauseMenu;
                    @TogglePauseMenu.canceled += instance.OnTogglePauseMenu;
                }
            }
        }
        public PauseMenuActions @PauseMenu => new PauseMenuActions(this);

        // DeveloperConsole
        private readonly InputActionMap m_DeveloperConsole;
        private IDeveloperConsoleActions m_DeveloperConsoleActionsCallbackInterface;
        private readonly InputAction m_DeveloperConsole_ToggleDeveloperConsole;
        public struct DeveloperConsoleActions
        {
            private @UIInputActions m_Wrapper;
            public DeveloperConsoleActions(@UIInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @ToggleDeveloperConsole => m_Wrapper.m_DeveloperConsole_ToggleDeveloperConsole;
            public InputActionMap Get() { return m_Wrapper.m_DeveloperConsole; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(DeveloperConsoleActions set) { return set.Get(); }
            public void SetCallbacks(IDeveloperConsoleActions instance)
            {
                if (m_Wrapper.m_DeveloperConsoleActionsCallbackInterface != null)
                {
                    @ToggleDeveloperConsole.started -= m_Wrapper.m_DeveloperConsoleActionsCallbackInterface.OnToggleDeveloperConsole;
                    @ToggleDeveloperConsole.performed -= m_Wrapper.m_DeveloperConsoleActionsCallbackInterface.OnToggleDeveloperConsole;
                    @ToggleDeveloperConsole.canceled -= m_Wrapper.m_DeveloperConsoleActionsCallbackInterface.OnToggleDeveloperConsole;
                }
                m_Wrapper.m_DeveloperConsoleActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @ToggleDeveloperConsole.started += instance.OnToggleDeveloperConsole;
                    @ToggleDeveloperConsole.performed += instance.OnToggleDeveloperConsole;
                    @ToggleDeveloperConsole.canceled += instance.OnToggleDeveloperConsole;
                }
            }
        }
        public DeveloperConsoleActions @DeveloperConsole => new DeveloperConsoleActions(this);
        public interface IPauseMenuActions
        {
            void OnTogglePauseMenu(InputAction.CallbackContext context);
        }
        public interface IDeveloperConsoleActions
        {
            void OnToggleDeveloperConsole(InputAction.CallbackContext context);
        }
    }
}
