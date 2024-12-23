//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Input/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""KeyboardActions1"",
            ""id"": ""c9a5f50f-d0ea-4e5f-96d5-f379ac5038fa"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""960eb106-23be-47ed-b741-5d47bd6decce"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""362ba671-cde8-446e-a653-851d3dae5618"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Water"",
                    ""type"": ""Button"",
                    ""id"": ""9d5bed5a-209f-47b3-bd06-9a7590c6ec1f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PlantSeed"",
                    ""type"": ""Button"",
                    ""id"": ""26d41a63-5e2f-46ea-b46a-dc6c05dccaa5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""f6b11372-3e58-4028-8922-b2e1dbbdb9aa"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""35b4de0d-44be-4cb9-89f5-605a5b4677c3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""6a137656-088f-4d54-b0d0-41a7428e071e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""73e76757-ad7e-40a2-a4f1-e8ad62d25f60"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a433612e-a28e-45d7-88dc-9b0947f21871"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""572048b8-06c7-4b84-a688-6f0e3fe4c836"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""140336a0-b1f9-431c-9285-a12e1923d4f1"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ed3304b2-e181-42af-afd9-df9d8b97d6b1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f061e3d0-11c3-4ff9-9b17-20296ae1d4b5"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Water"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80bd9852-2c9e-4c65-976e-616c44b35176"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""PlantSeed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b34695a9-acf0-481e-8d45-c10774c7002f"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48afd1b1-3b92-4c51-902b-f2dc6320444c"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""ControlActions1"",
            ""id"": ""fb482c4d-1d8a-422f-ad39-ae2e7f6169e8"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""5c56610c-f020-403d-a4be-03bddc3eebcc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""a244c18f-9894-48cc-ac66-16406f8778dd"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""d7d55869-27e1-4c78-b0ba-796175e0c725"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Water"",
                    ""type"": ""Button"",
                    ""id"": ""6d244ce4-365f-4ba9-84af-aced4e1f7a52"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PlantSeed"",
                    ""type"": ""Button"",
                    ""id"": ""1f91cfc5-2742-465e-a41c-1d3b00db2dac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Value"",
                    ""id"": ""c7d85696-4d96-48d7-8c4e-c1da9199344a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Join"",
                    ""type"": ""Button"",
                    ""id"": ""5a810e6a-3f5e-4fb0-a4cf-7f691e38d47d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""NextDialogue"",
                    ""type"": ""Button"",
                    ""id"": ""8b37be16-9d85-4f6f-82f4-dd09cbab4178"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PickUp"",
                    ""type"": ""Button"",
                    ""id"": ""e714669b-8a5e-423b-a759-707649f215c8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""27efcaf0-f028-4126-8d50-06648c1921d3"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""148640b4-fb7d-4100-a5cc-3ee292161866"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe18dfbe-4cd0-4c59-923a-0201b03eab8d"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82433dfd-8f37-4aca-8785-c16393469ff4"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""Water"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3036fd63-4ef5-4a6c-bfc2-a80504bef658"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""PlantSeed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48aa2e12-a47c-40b2-b1eb-3a49924e5423"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88e0631e-9a21-403d-a608-66a5c07dbb30"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a141322-f336-48ba-87ff-788a3d31d5ba"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""NextDialogue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bb1e02b9-7b1e-4016-94df-9700aaec3fee"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Control"",
                    ""action"": ""PickUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Control"",
            ""bindingGroup"": ""Control"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
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
        }
    ]
}");
        // KeyboardActions1
        m_KeyboardActions1 = asset.FindActionMap("KeyboardActions1", throwIfNotFound: true);
        m_KeyboardActions1_Move = m_KeyboardActions1.FindAction("Move", throwIfNotFound: true);
        m_KeyboardActions1_Fire = m_KeyboardActions1.FindAction("Fire", throwIfNotFound: true);
        m_KeyboardActions1_Water = m_KeyboardActions1.FindAction("Water", throwIfNotFound: true);
        m_KeyboardActions1_PlantSeed = m_KeyboardActions1.FindAction("PlantSeed", throwIfNotFound: true);
        m_KeyboardActions1_Rotate = m_KeyboardActions1.FindAction("Rotate", throwIfNotFound: true);
        m_KeyboardActions1_Newaction = m_KeyboardActions1.FindAction("New action", throwIfNotFound: true);
        // ControlActions1
        m_ControlActions1 = asset.FindActionMap("ControlActions1", throwIfNotFound: true);
        m_ControlActions1_Move = m_ControlActions1.FindAction("Move", throwIfNotFound: true);
        m_ControlActions1_Rotate = m_ControlActions1.FindAction("Rotate", throwIfNotFound: true);
        m_ControlActions1_Fire = m_ControlActions1.FindAction("Fire", throwIfNotFound: true);
        m_ControlActions1_Water = m_ControlActions1.FindAction("Water", throwIfNotFound: true);
        m_ControlActions1_PlantSeed = m_ControlActions1.FindAction("PlantSeed", throwIfNotFound: true);
        m_ControlActions1_Dash = m_ControlActions1.FindAction("Dash", throwIfNotFound: true);
        m_ControlActions1_Join = m_ControlActions1.FindAction("Join", throwIfNotFound: true);
        m_ControlActions1_NextDialogue = m_ControlActions1.FindAction("NextDialogue", throwIfNotFound: true);
        m_ControlActions1_PickUp = m_ControlActions1.FindAction("PickUp", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // KeyboardActions1
    private readonly InputActionMap m_KeyboardActions1;
    private List<IKeyboardActions1Actions> m_KeyboardActions1ActionsCallbackInterfaces = new List<IKeyboardActions1Actions>();
    private readonly InputAction m_KeyboardActions1_Move;
    private readonly InputAction m_KeyboardActions1_Fire;
    private readonly InputAction m_KeyboardActions1_Water;
    private readonly InputAction m_KeyboardActions1_PlantSeed;
    private readonly InputAction m_KeyboardActions1_Rotate;
    private readonly InputAction m_KeyboardActions1_Newaction;
    public struct KeyboardActions1Actions
    {
        private @PlayerInputActions m_Wrapper;
        public KeyboardActions1Actions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_KeyboardActions1_Move;
        public InputAction @Fire => m_Wrapper.m_KeyboardActions1_Fire;
        public InputAction @Water => m_Wrapper.m_KeyboardActions1_Water;
        public InputAction @PlantSeed => m_Wrapper.m_KeyboardActions1_PlantSeed;
        public InputAction @Rotate => m_Wrapper.m_KeyboardActions1_Rotate;
        public InputAction @Newaction => m_Wrapper.m_KeyboardActions1_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_KeyboardActions1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardActions1Actions set) { return set.Get(); }
        public void AddCallbacks(IKeyboardActions1Actions instance)
        {
            if (instance == null || m_Wrapper.m_KeyboardActions1ActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_KeyboardActions1ActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Fire.started += instance.OnFire;
            @Fire.performed += instance.OnFire;
            @Fire.canceled += instance.OnFire;
            @Water.started += instance.OnWater;
            @Water.performed += instance.OnWater;
            @Water.canceled += instance.OnWater;
            @PlantSeed.started += instance.OnPlantSeed;
            @PlantSeed.performed += instance.OnPlantSeed;
            @PlantSeed.canceled += instance.OnPlantSeed;
            @Rotate.started += instance.OnRotate;
            @Rotate.performed += instance.OnRotate;
            @Rotate.canceled += instance.OnRotate;
            @Newaction.started += instance.OnNewaction;
            @Newaction.performed += instance.OnNewaction;
            @Newaction.canceled += instance.OnNewaction;
        }

        private void UnregisterCallbacks(IKeyboardActions1Actions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Fire.started -= instance.OnFire;
            @Fire.performed -= instance.OnFire;
            @Fire.canceled -= instance.OnFire;
            @Water.started -= instance.OnWater;
            @Water.performed -= instance.OnWater;
            @Water.canceled -= instance.OnWater;
            @PlantSeed.started -= instance.OnPlantSeed;
            @PlantSeed.performed -= instance.OnPlantSeed;
            @PlantSeed.canceled -= instance.OnPlantSeed;
            @Rotate.started -= instance.OnRotate;
            @Rotate.performed -= instance.OnRotate;
            @Rotate.canceled -= instance.OnRotate;
            @Newaction.started -= instance.OnNewaction;
            @Newaction.performed -= instance.OnNewaction;
            @Newaction.canceled -= instance.OnNewaction;
        }

        public void RemoveCallbacks(IKeyboardActions1Actions instance)
        {
            if (m_Wrapper.m_KeyboardActions1ActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IKeyboardActions1Actions instance)
        {
            foreach (var item in m_Wrapper.m_KeyboardActions1ActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_KeyboardActions1ActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public KeyboardActions1Actions @KeyboardActions1 => new KeyboardActions1Actions(this);

    // ControlActions1
    private readonly InputActionMap m_ControlActions1;
    private List<IControlActions1Actions> m_ControlActions1ActionsCallbackInterfaces = new List<IControlActions1Actions>();
    private readonly InputAction m_ControlActions1_Move;
    private readonly InputAction m_ControlActions1_Rotate;
    private readonly InputAction m_ControlActions1_Fire;
    private readonly InputAction m_ControlActions1_Water;
    private readonly InputAction m_ControlActions1_PlantSeed;
    private readonly InputAction m_ControlActions1_Dash;
    private readonly InputAction m_ControlActions1_Join;
    private readonly InputAction m_ControlActions1_NextDialogue;
    private readonly InputAction m_ControlActions1_PickUp;
    public struct ControlActions1Actions
    {
        private @PlayerInputActions m_Wrapper;
        public ControlActions1Actions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_ControlActions1_Move;
        public InputAction @Rotate => m_Wrapper.m_ControlActions1_Rotate;
        public InputAction @Fire => m_Wrapper.m_ControlActions1_Fire;
        public InputAction @Water => m_Wrapper.m_ControlActions1_Water;
        public InputAction @PlantSeed => m_Wrapper.m_ControlActions1_PlantSeed;
        public InputAction @Dash => m_Wrapper.m_ControlActions1_Dash;
        public InputAction @Join => m_Wrapper.m_ControlActions1_Join;
        public InputAction @NextDialogue => m_Wrapper.m_ControlActions1_NextDialogue;
        public InputAction @PickUp => m_Wrapper.m_ControlActions1_PickUp;
        public InputActionMap Get() { return m_Wrapper.m_ControlActions1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControlActions1Actions set) { return set.Get(); }
        public void AddCallbacks(IControlActions1Actions instance)
        {
            if (instance == null || m_Wrapper.m_ControlActions1ActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ControlActions1ActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Rotate.started += instance.OnRotate;
            @Rotate.performed += instance.OnRotate;
            @Rotate.canceled += instance.OnRotate;
            @Fire.started += instance.OnFire;
            @Fire.performed += instance.OnFire;
            @Fire.canceled += instance.OnFire;
            @Water.started += instance.OnWater;
            @Water.performed += instance.OnWater;
            @Water.canceled += instance.OnWater;
            @PlantSeed.started += instance.OnPlantSeed;
            @PlantSeed.performed += instance.OnPlantSeed;
            @PlantSeed.canceled += instance.OnPlantSeed;
            @Dash.started += instance.OnDash;
            @Dash.performed += instance.OnDash;
            @Dash.canceled += instance.OnDash;
            @Join.started += instance.OnJoin;
            @Join.performed += instance.OnJoin;
            @Join.canceled += instance.OnJoin;
            @NextDialogue.started += instance.OnNextDialogue;
            @NextDialogue.performed += instance.OnNextDialogue;
            @NextDialogue.canceled += instance.OnNextDialogue;
            @PickUp.started += instance.OnPickUp;
            @PickUp.performed += instance.OnPickUp;
            @PickUp.canceled += instance.OnPickUp;
        }

        private void UnregisterCallbacks(IControlActions1Actions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Rotate.started -= instance.OnRotate;
            @Rotate.performed -= instance.OnRotate;
            @Rotate.canceled -= instance.OnRotate;
            @Fire.started -= instance.OnFire;
            @Fire.performed -= instance.OnFire;
            @Fire.canceled -= instance.OnFire;
            @Water.started -= instance.OnWater;
            @Water.performed -= instance.OnWater;
            @Water.canceled -= instance.OnWater;
            @PlantSeed.started -= instance.OnPlantSeed;
            @PlantSeed.performed -= instance.OnPlantSeed;
            @PlantSeed.canceled -= instance.OnPlantSeed;
            @Dash.started -= instance.OnDash;
            @Dash.performed -= instance.OnDash;
            @Dash.canceled -= instance.OnDash;
            @Join.started -= instance.OnJoin;
            @Join.performed -= instance.OnJoin;
            @Join.canceled -= instance.OnJoin;
            @NextDialogue.started -= instance.OnNextDialogue;
            @NextDialogue.performed -= instance.OnNextDialogue;
            @NextDialogue.canceled -= instance.OnNextDialogue;
            @PickUp.started -= instance.OnPickUp;
            @PickUp.performed -= instance.OnPickUp;
            @PickUp.canceled -= instance.OnPickUp;
        }

        public void RemoveCallbacks(IControlActions1Actions instance)
        {
            if (m_Wrapper.m_ControlActions1ActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IControlActions1Actions instance)
        {
            foreach (var item in m_Wrapper.m_ControlActions1ActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ControlActions1ActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ControlActions1Actions @ControlActions1 => new ControlActions1Actions(this);
    private int m_ControlSchemeIndex = -1;
    public InputControlScheme ControlScheme
    {
        get
        {
            if (m_ControlSchemeIndex == -1) m_ControlSchemeIndex = asset.FindControlSchemeIndex("Control");
            return asset.controlSchemes[m_ControlSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IKeyboardActions1Actions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnWater(InputAction.CallbackContext context);
        void OnPlantSeed(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnNewaction(InputAction.CallbackContext context);
    }
    public interface IControlActions1Actions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnWater(InputAction.CallbackContext context);
        void OnPlantSeed(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnJoin(InputAction.CallbackContext context);
        void OnNextDialogue(InputAction.CallbackContext context);
        void OnPickUp(InputAction.CallbackContext context);
    }
}
