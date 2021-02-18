// GENERATED AUTOMATICALLY FROM 'Assets/Registrate.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputHandler : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputHandler()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Registrate"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""a8148260-0589-47b0-a783-6ac19d767f3c"",
            ""actions"": [
                {
                    ""name"": ""Enter"",
                    ""type"": ""Button"",
                    ""id"": ""2010dc40-7aa7-4332-b4c5-d5002ab722a9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""62286fd8-5240-4bbf-bd4f-1fba893998ca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DpadUp"",
                    ""type"": ""Button"",
                    ""id"": ""039c477a-daec-43d6-9fb7-ea8058a09a20"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DpadDown"",
                    ""type"": ""Button"",
                    ""id"": ""3a8ee462-6480-4de7-87de-3179569eecc3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DpadRight"",
                    ""type"": ""Button"",
                    ""id"": ""b60c9c5d-e7a2-48d1-84d8-df301d4e3ea5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DpadLeft"",
                    ""type"": ""Button"",
                    ""id"": ""fe5c158b-63d8-4bb6-99b5-fa7b989a7de9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""StartButton"",
                    ""type"": ""Button"",
                    ""id"": ""be9b4ed9-6761-4821-9fb1-37ecb5e66a5d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftStick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""76da2a8f-0742-4d1e-a839-0d195b470429"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1251af35-cfea-49f0-a738-bf6814d73362"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxControlScheme"",
                    ""action"": ""Enter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db511cfb-8a3e-454a-b5ca-eb12be7195e0"",
                    ""path"": ""<XInputController>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxControlScheme"",
                    ""action"": ""DpadUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""73cff741-e986-44e0-8b02-76ab3f7b8e8c"",
                    ""path"": ""<XInputController>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxControlScheme"",
                    ""action"": ""DpadDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb8462d3-3e74-41bc-8be3-c1503484f686"",
                    ""path"": ""<XInputController>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxControlScheme"",
                    ""action"": ""DpadRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c9369c9c-0bd6-4d9e-af7b-2fc14064c1ce"",
                    ""path"": ""<XInputController>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxControlScheme"",
                    ""action"": ""DpadLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bacb38a1-6cd9-42f7-8079-2b5b12f13e8a"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StartButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b1cec19c-f0c8-48f4-8986-16ca1b421543"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftStick"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a5ef3795-5920-460b-8df9-de2c6f1c33c2"",
                    ""path"": ""<XInputController>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxControlScheme"",
                    ""action"": ""LeftStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e08c50dc-62a1-45a8-ba33-970caf295977"",
                    ""path"": ""<XInputController>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxControlScheme"",
                    ""action"": ""LeftStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""369dd17b-8a50-4342-a39a-2a094064058e"",
                    ""path"": ""<XInputController>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxControlScheme"",
                    ""action"": ""LeftStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0eb5548f-cc03-48cd-9afb-67cb232338d4"",
                    ""path"": ""<XInputController>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxControlScheme"",
                    ""action"": ""LeftStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a2abd335-0730-4a36-9fa3-1a719fa89da7"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxControlScheme"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UserInterface"",
            ""id"": ""20aeeb44-1c8f-4214-a254-0ea8e0916091"",
            ""actions"": [
                {
                    ""name"": ""Enter"",
                    ""type"": ""Button"",
                    ""id"": ""b30aad93-ed46-4c25-9bff-f941f556eff0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Start"",
                    ""type"": ""Button"",
                    ""id"": ""6e759aae-1c2c-4b34-aa56-9f1faa5cfa1b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DpadDown"",
                    ""type"": ""Button"",
                    ""id"": ""218e3e2f-5138-4df7-b909-ed9d9dcb52c4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DpadUp"",
                    ""type"": ""Button"",
                    ""id"": ""9af58747-92fe-4767-8616-54ead2ace411"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""e3a375a0-2c34-4182-aefd-9734c107fdac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""123c7a8c-60cb-45da-ab5e-5683c44b0bbb"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxControlScheme"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5962a6c9-fef8-48ec-8194-d74b8febd779"",
                    ""path"": ""<XInputController>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DpadUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""91affbc9-0275-497b-9801-f8432bbdca93"",
                    ""path"": ""<XInputController>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DpadDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a1157a62-e609-4a46-b7ba-cbceb01fdd6a"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxControlScheme"",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16d39791-f13b-4bec-8ee4-171d4e0064b5"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxControlScheme"",
                    ""action"": ""Enter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Joystick"",
            ""bindingGroup"": ""Joystick"",
            ""devices"": [
                {
                    ""devicePath"": ""<Joystick>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""XR"",
            ""bindingGroup"": ""XR"",
            ""devices"": [
                {
                    ""devicePath"": ""<XRController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""XboxControlScheme"",
            ""bindingGroup"": ""XboxControlScheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Enter = m_Player.FindAction("Enter", throwIfNotFound: true);
        m_Player_Back = m_Player.FindAction("Back", throwIfNotFound: true);
        m_Player_DpadUp = m_Player.FindAction("DpadUp", throwIfNotFound: true);
        m_Player_DpadDown = m_Player.FindAction("DpadDown", throwIfNotFound: true);
        m_Player_DpadRight = m_Player.FindAction("DpadRight", throwIfNotFound: true);
        m_Player_DpadLeft = m_Player.FindAction("DpadLeft", throwIfNotFound: true);
        m_Player_StartButton = m_Player.FindAction("StartButton", throwIfNotFound: true);
        m_Player_LeftStick = m_Player.FindAction("LeftStick", throwIfNotFound: true);
        // UserInterface
        m_UserInterface = asset.FindActionMap("UserInterface", throwIfNotFound: true);
        m_UserInterface_Enter = m_UserInterface.FindAction("Enter", throwIfNotFound: true);
        m_UserInterface_Start = m_UserInterface.FindAction("Start", throwIfNotFound: true);
        m_UserInterface_DpadDown = m_UserInterface.FindAction("DpadDown", throwIfNotFound: true);
        m_UserInterface_DpadUp = m_UserInterface.FindAction("DpadUp", throwIfNotFound: true);
        m_UserInterface_Back = m_UserInterface.FindAction("Back", throwIfNotFound: true);
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
    private readonly InputAction m_Player_Enter;
    private readonly InputAction m_Player_Back;
    private readonly InputAction m_Player_DpadUp;
    private readonly InputAction m_Player_DpadDown;
    private readonly InputAction m_Player_DpadRight;
    private readonly InputAction m_Player_DpadLeft;
    private readonly InputAction m_Player_StartButton;
    private readonly InputAction m_Player_LeftStick;
    public struct PlayerActions
    {
        private @InputHandler m_Wrapper;
        public PlayerActions(@InputHandler wrapper) { m_Wrapper = wrapper; }
        public InputAction @Enter => m_Wrapper.m_Player_Enter;
        public InputAction @Back => m_Wrapper.m_Player_Back;
        public InputAction @DpadUp => m_Wrapper.m_Player_DpadUp;
        public InputAction @DpadDown => m_Wrapper.m_Player_DpadDown;
        public InputAction @DpadRight => m_Wrapper.m_Player_DpadRight;
        public InputAction @DpadLeft => m_Wrapper.m_Player_DpadLeft;
        public InputAction @StartButton => m_Wrapper.m_Player_StartButton;
        public InputAction @LeftStick => m_Wrapper.m_Player_LeftStick;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Enter.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEnter;
                @Enter.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEnter;
                @Enter.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEnter;
                @Back.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBack;
                @DpadUp.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDpadUp;
                @DpadUp.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDpadUp;
                @DpadUp.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDpadUp;
                @DpadDown.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDpadDown;
                @DpadDown.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDpadDown;
                @DpadDown.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDpadDown;
                @DpadRight.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDpadRight;
                @DpadRight.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDpadRight;
                @DpadRight.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDpadRight;
                @DpadLeft.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDpadLeft;
                @DpadLeft.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDpadLeft;
                @DpadLeft.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDpadLeft;
                @StartButton.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStartButton;
                @StartButton.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStartButton;
                @StartButton.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStartButton;
                @LeftStick.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftStick;
                @LeftStick.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftStick;
                @LeftStick.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftStick;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Enter.started += instance.OnEnter;
                @Enter.performed += instance.OnEnter;
                @Enter.canceled += instance.OnEnter;
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
                @DpadUp.started += instance.OnDpadUp;
                @DpadUp.performed += instance.OnDpadUp;
                @DpadUp.canceled += instance.OnDpadUp;
                @DpadDown.started += instance.OnDpadDown;
                @DpadDown.performed += instance.OnDpadDown;
                @DpadDown.canceled += instance.OnDpadDown;
                @DpadRight.started += instance.OnDpadRight;
                @DpadRight.performed += instance.OnDpadRight;
                @DpadRight.canceled += instance.OnDpadRight;
                @DpadLeft.started += instance.OnDpadLeft;
                @DpadLeft.performed += instance.OnDpadLeft;
                @DpadLeft.canceled += instance.OnDpadLeft;
                @StartButton.started += instance.OnStartButton;
                @StartButton.performed += instance.OnStartButton;
                @StartButton.canceled += instance.OnStartButton;
                @LeftStick.started += instance.OnLeftStick;
                @LeftStick.performed += instance.OnLeftStick;
                @LeftStick.canceled += instance.OnLeftStick;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // UserInterface
    private readonly InputActionMap m_UserInterface;
    private IUserInterfaceActions m_UserInterfaceActionsCallbackInterface;
    private readonly InputAction m_UserInterface_Enter;
    private readonly InputAction m_UserInterface_Start;
    private readonly InputAction m_UserInterface_DpadDown;
    private readonly InputAction m_UserInterface_DpadUp;
    private readonly InputAction m_UserInterface_Back;
    public struct UserInterfaceActions
    {
        private @InputHandler m_Wrapper;
        public UserInterfaceActions(@InputHandler wrapper) { m_Wrapper = wrapper; }
        public InputAction @Enter => m_Wrapper.m_UserInterface_Enter;
        public InputAction @Start => m_Wrapper.m_UserInterface_Start;
        public InputAction @DpadDown => m_Wrapper.m_UserInterface_DpadDown;
        public InputAction @DpadUp => m_Wrapper.m_UserInterface_DpadUp;
        public InputAction @Back => m_Wrapper.m_UserInterface_Back;
        public InputActionMap Get() { return m_Wrapper.m_UserInterface; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UserInterfaceActions set) { return set.Get(); }
        public void SetCallbacks(IUserInterfaceActions instance)
        {
            if (m_Wrapper.m_UserInterfaceActionsCallbackInterface != null)
            {
                @Enter.started -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnEnter;
                @Enter.performed -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnEnter;
                @Enter.canceled -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnEnter;
                @Start.started -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnStart;
                @Start.performed -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnStart;
                @Start.canceled -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnStart;
                @DpadDown.started -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnDpadDown;
                @DpadDown.performed -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnDpadDown;
                @DpadDown.canceled -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnDpadDown;
                @DpadUp.started -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnDpadUp;
                @DpadUp.performed -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnDpadUp;
                @DpadUp.canceled -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnDpadUp;
                @Back.started -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnBack;
            }
            m_Wrapper.m_UserInterfaceActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Enter.started += instance.OnEnter;
                @Enter.performed += instance.OnEnter;
                @Enter.canceled += instance.OnEnter;
                @Start.started += instance.OnStart;
                @Start.performed += instance.OnStart;
                @Start.canceled += instance.OnStart;
                @DpadDown.started += instance.OnDpadDown;
                @DpadDown.performed += instance.OnDpadDown;
                @DpadDown.canceled += instance.OnDpadDown;
                @DpadUp.started += instance.OnDpadUp;
                @DpadUp.performed += instance.OnDpadUp;
                @DpadUp.canceled += instance.OnDpadUp;
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
            }
        }
    }
    public UserInterfaceActions @UserInterface => new UserInterfaceActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_TouchSchemeIndex = -1;
    public InputControlScheme TouchScheme
    {
        get
        {
            if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
            return asset.controlSchemes[m_TouchSchemeIndex];
        }
    }
    private int m_JoystickSchemeIndex = -1;
    public InputControlScheme JoystickScheme
    {
        get
        {
            if (m_JoystickSchemeIndex == -1) m_JoystickSchemeIndex = asset.FindControlSchemeIndex("Joystick");
            return asset.controlSchemes[m_JoystickSchemeIndex];
        }
    }
    private int m_XRSchemeIndex = -1;
    public InputControlScheme XRScheme
    {
        get
        {
            if (m_XRSchemeIndex == -1) m_XRSchemeIndex = asset.FindControlSchemeIndex("XR");
            return asset.controlSchemes[m_XRSchemeIndex];
        }
    }
    private int m_XboxControlSchemeSchemeIndex = -1;
    public InputControlScheme XboxControlSchemeScheme
    {
        get
        {
            if (m_XboxControlSchemeSchemeIndex == -1) m_XboxControlSchemeSchemeIndex = asset.FindControlSchemeIndex("XboxControlScheme");
            return asset.controlSchemes[m_XboxControlSchemeSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnEnter(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
        void OnDpadUp(InputAction.CallbackContext context);
        void OnDpadDown(InputAction.CallbackContext context);
        void OnDpadRight(InputAction.CallbackContext context);
        void OnDpadLeft(InputAction.CallbackContext context);
        void OnStartButton(InputAction.CallbackContext context);
        void OnLeftStick(InputAction.CallbackContext context);
    }
    public interface IUserInterfaceActions
    {
        void OnEnter(InputAction.CallbackContext context);
        void OnStart(InputAction.CallbackContext context);
        void OnDpadDown(InputAction.CallbackContext context);
        void OnDpadUp(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
    }
}
