using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Specialized;
using System;
using System.Text;

public class LogitechGSDK
{

    //ARX CONTROL SDK
    public const int LOGI_ARX_ORIENTATION_PORTRAIT = 0x01;
    public const int LOGI_ARX_ORIENTATION_LANDSCAPE = 0x10;
    public const int LOGI_ARX_EVENT_FOCUS_ACTIVE = 0x01;
    public const int LOGI_ARX_EVENT_FOCUS_INACTIVE = 0x02;
    public const int LOGI_ARX_EVENT_TAP_ON_TAG = 0x04;
    public const int LOGI_ARX_EVENT_MOBILEDEVICE_ARRIVAL = 0x08;
    public const int LOGI_ARX_EVENT_MOBILEDEVICE_REMOVAL = 0x10;
    public const int LOGI_ARX_DEVICETYPE_IPHONE = 0x01;
    public const int LOGI_ARX_DEVICETYPE_IPAD = 0x02;
    public const int LOGI_ARX_DEVICETYPE_ANDROID_SMALL = 0x03;
    public const int LOGI_ARX_DEVICETYPE_ANDROID_NORMAL = 0x04;
    public const int LOGI_ARX_DEVICETYPE_ANDROID_LARGE = 0x05;
    public const int LOGI_ARX_DEVICETYPE_ANDROID_XLARGE = 0x06;
    public const int LOGI_ARX_DEVICETYPE_ANDROID_OTHER = 0x07;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void logiArxCB(int eventType, int eventValue, [MarshalAs(UnmanagedType.LPWStr)]String eventArg, IntPtr context);

    public struct logiArxCbContext
    {
        public logiArxCB arxCallBack;
        public IntPtr arxContext;
    }

    [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiArxInit(String identifier, String friendlyName, ref logiArxCbContext callback);

    [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiArxAddFileAs(String filePath, String fileName, String mimeType);

    [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiArxAddContentAs(byte[] content, int size, String fileName, String mimeType = "");

    [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiArxAddUTF8StringAs(String stringContent, String fileName, String mimeType = "");

    [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiArxAddImageFromBitmap(byte[] bitmap, int width, int height, String fileName);

    [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiArxSetIndex(String fileName);

    [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiArxSetTagPropertyById(String tagId, String prop, String newValue);

    [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiArxSetTagsPropertyByClass(String tagsClass, String prop, String newValue);

    [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiArxSetTagContentById(String tagId, String newContent);

    [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiArxSetTagsContentByClass(String tagsClass, String newContent);

    [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern int LogiArxGetLastError();

    [DllImport("LogitechGArxControlEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern void LogiArxShutdown();


    //LED SDK
    public const int LOGI_LED_BITMAP_WIDTH = 21;
    public const int LOGI_LED_BITMAP_HEIGHT = 6;
    public const int LOGI_LED_BITMAP_BYTES_PER_KEY = 4;
    public const int LOGI_LED_BITMAP_SIZE = LOGI_LED_BITMAP_WIDTH * LOGI_LED_BITMAP_HEIGHT * LOGI_LED_BITMAP_BYTES_PER_KEY;

    public const int LOGI_LED_DURATION_INFINITE = 0;

    private const int LOGI_DEVICETYPE_MONOCHROME_ORD = 0;
    private const int LOGI_DEVICETYPE_RGB_ORD = 1;
    private const int LOGI_DEVICETYPE_PERKEY_RGB_ORD = 2;

    public const int LOGI_DEVICETYPE_MONOCHROME = (1 << LOGI_DEVICETYPE_MONOCHROME_ORD);
    public const int LOGI_DEVICETYPE_RGB = (1 << LOGI_DEVICETYPE_RGB_ORD);
    public const int LOGI_DEVICETYPE_PERKEY_RGB = (1 << LOGI_DEVICETYPE_PERKEY_RGB_ORD);

    public enum keyboardNames
    {
        ESC = 0x01,
        F1 = 0x3b,
        F2 = 0x3c,
        F3 = 0x3d,
        F4 = 0x3e,
        F5 = 0x3f,
        F6 = 0x40,
        F7 = 0x41,
        F8 = 0x42,
        F9 = 0x43,
        F10 = 0x44,
        F11 = 0x57,
        F12 = 0x58,
        PRINT_SCREEN = 0x137,
        SCROLL_LOCK = 0x46,
        PAUSE_BREAK = 0x45,
        TILDE = 0x29,
        ONE = 0x02,
        TWO = 0x03,
        THREE = 0x04,
        FOUR = 0x05,
        FIVE = 0x06,
        SIX = 0x07,
        SEVEN = 0x08,
        EIGHT = 0x09,
        NINE = 0x0A,
        ZERO = 0x0B,
        MINUS = 0x0C,
        EQUALS = 0x0D,
        BACKSPACE = 0x0E,
        INSERT = 0x152,
        HOME = 0x147,
        PAGE_UP = 0x149,
        NUM_LOCK = 0x145,
        NUM_SLASH = 0x135,
        NUM_ASTERISK = 0x37,
        NUM_MINUS = 0x4A,
        TAB = 0x0F,
        Q = 0x10,
        W = 0x11,
        E = 0x12,
        R = 0x13,
        T = 0x14,
        Y = 0x15,
        U = 0x16,
        I = 0x17,
        O = 0x18,
        P = 0x19,
        OPEN_BRACKET = 0x1A,
        CLOSE_BRACKET = 0x1B,
        BACKSLASH = 0x2B,
        KEYBOARD_DELETE = 0x153,
        END = 0x14F,
        PAGE_DOWN = 0x151,
        NUM_SEVEN = 0x47,
        NUM_EIGHT = 0x48,
        NUM_NINE = 0x49,
        NUM_PLUS = 0x4E,
        CAPS_LOCK = 0x3A,
        A = 0x1E,
        S = 0x1F,
        D = 0x20,
        F = 0x21,
        G = 0x22,
        H = 0x23,
        J = 0x24,
        K = 0x25,
        L = 0x26,
        SEMICOLON = 0x27,
        APOSTROPHE = 0x28,
        ENTER = 0x1C,
        NUM_FOUR = 0x4B,
        NUM_FIVE = 0x4C,
        NUM_SIX = 0x4D,
        LEFT_SHIFT = 0x2A,
        Z = 0x2C,
        X = 0x2D,
        C = 0x2E,
        V = 0x2F,
        B = 0x30,
        N = 0x31,
        M = 0x32,
        COMMA = 0x33,
        PERIOD = 0x34,
        FORWARD_SLASH = 0x35,
        RIGHT_SHIFT = 0x36,
        ARROW_UP = 0x148,
        NUM_ONE = 0x4F,
        NUM_TWO = 0x50,
        NUM_THREE = 0x51,
        NUM_ENTER = 0x11C,
        LEFT_CONTROL = 0x1D,
        LEFT_WINDOWS = 0x15B,
        LEFT_ALT = 0x38,
        SPACE = 0x39,
        RIGHT_ALT = 0x138,
        RIGHT_WINDOWS = 0x15C,
        APPLICATION_SELECT = 0x15D,
        RIGHT_CONTROL = 0x11D,
        ARROW_LEFT = 0x14B,
        ARROW_DOWN = 0x150,
        ARROW_RIGHT = 0x14D,
        NUM_ZERO = 0x52,
        NUM_PERIOD = 0x53,

    };

    public enum DeviceType
    {
        Keyboard = 0x0,
        Mouse = 0x3,
        Mousemat = 0x4,
        Headset = 0x8,
        Speaker = 0xe
    };

    [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLedInit();

    [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLedSetTargetDevice(int targetDevice);

    [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLedGetSdkVersion(ref int majorNum, ref int minorNum, ref int buildNum);

    [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLedSaveCurrentLighting();

    [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLedSetLighting(int redPercentage, int greenPercentage, int bluePercentage);

    [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLedRestoreLighting();

    [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLedFlashLighting(int redPercentage, int greenPercentage, int bluePercentage, int milliSecondsDuration, int milliSecondsInterval);

    [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLedPulseLighting(int redPercentage, int greenPercentage, int bluePercentage, int milliSecondsDuration, int milliSecondsInterval);

    [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLedStopEffects();

    [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLedSetLightingFromBitmap(byte[] bitmap);

    [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLedSetLightingForKeyWithScanCode(int keyCode, int redPercentage, int greenPercentage, int bluePercentage);

    [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLedSetLightingForKeyWithHidCode(int keyCode, int redPercentage, int greenPercentage, int bluePercentage);

    [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLedSetLightingForKeyWithQuartzCode(int keyCode, int redPercentage, int greenPercentage, int bluePercentage);

    [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLedSetLightingForKeyWithKeyName(keyboardNames keyCode, int redPercentage, int greenPercentage, int bluePercentage);

    [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLedSetLightingForTargetZone(LogitechGSDK.DeviceType deviceType, int zone, int redPercentage, int greenPercentage, int bluePercentage);

    [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLedSaveLightingForKey(keyboardNames keyName);

    [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLedRestoreLightingForKey(keyboardNames keyName);

    [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLedFlashSingleKey(keyboardNames keyName, int redPercentage, int greenPercentage, int bluePercentage, int msDuration, int msInterval);

    [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLedPulseSingleKey(keyboardNames keyName, int startRedPercentage, int startGreenPercentage, int startBluePercentage, int finishRedPercentage, int finishGreenPercentage, int finishBluePercentage, int msDuration, bool isInfinite);

    [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLedStopEffectsOnKey(keyboardNames keyName);

    [DllImport("LogitechLedEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
    public static extern void LogiLedShutdown();

    //END OF LED SDK


    //LCD SDK
    public const int LOGI_LCD_COLOR_BUTTON_LEFT = (0x00000100);
    public const int LOGI_LCD_COLOR_BUTTON_RIGHT = (0x00000200);
    public const int LOGI_LCD_COLOR_BUTTON_OK = (0x00000400);
    public const int LOGI_LCD_COLOR_BUTTON_CANCEL = (0x00000800);
    public const int LOGI_LCD_COLOR_BUTTON_UP = (0x00001000);
    public const int LOGI_LCD_COLOR_BUTTON_DOWN = (0x00002000);
    public const int LOGI_LCD_COLOR_BUTTON_MENU = (0x00004000);

    public const int LOGI_LCD_MONO_BUTTON_0 = (0x00000001);
    public const int LOGI_LCD_MONO_BUTTON_1 = (0x00000002);
    public const int LOGI_LCD_MONO_BUTTON_2 = (0x00000004);
    public const int LOGI_LCD_MONO_BUTTON_3 = (0x00000008);

    public const int LOGI_LCD_MONO_WIDTH = 160;
    public const int LOGI_LCD_MONO_HEIGHT = 43;

    public const int LOGI_LCD_COLOR_WIDTH = 320;
    public const int LOGI_LCD_COLOR_HEIGHT = 240;


    public const int LOGI_LCD_TYPE_MONO = (0x00000001);
    public const int LOGI_LCD_TYPE_COLOR = (0x00000002);



    [DllImport("LogitechLcdEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLcdInit(String friendlyName, int lcdType);

    [DllImport("LogitechLcdEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLcdIsConnected(int lcdType);

    [DllImport("LogitechLcdEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLcdIsButtonPressed(int button);

    [DllImport("LogitechLcdEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern void LogiLcdUpdate();

    [DllImport("LogitechLcdEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern void LogiLcdShutdown();

    // Monochrome LCD functions
    [DllImport("LogitechLcdEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLcdMonoSetBackground(byte[] monoBitmap);

    [DllImport("LogitechLcdEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLcdMonoSetText(int lineNumber, String text);

    // Color LCD functions
    [DllImport("LogitechLcdEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLcdColorSetBackground(byte[] colorBitmap);

    [DllImport("LogitechLcdEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLcdColorSetTitle(String text, int red, int green, int blue);

    [DllImport("LogitechLcdEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiLcdColorSetText(int lineNumber, String text, int red, int green, int blue);

    //END OF LCD SDK

    //G-KEY SDK

    public const int LOGITECH_MAX_MOUSE_BUTTONS = 20;
    public const int LOGITECH_MAX_GKEYS = 29;
    public const int LOGITECH_MAX_M_STATES = 3;


    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct GkeyCode
    {
        public ushort complete;
        // index of the G key or mouse button, for example, 6 for G6 or Button 6
        public int keyIdx
        {
            get
            {
                return complete & 255;
            }
        }
        // key up or down, 1 is down, 0 is up
        public int keyDown
        {
            get
            {
                return (complete >> 8) & 1;
            }
        }
        // mState (1, 2 or 3 for M1, M2 and M3)
        public int mState
        {
            get
            {
                return (complete >> 9) & 3;
            }
        }
        // indicate if the Event comes from a mouse, 1 is yes, 0 is no.
        public int mouse
        {
            get
            {
                return (complete >> 11) & 15;
            }
        }
        // reserved1
        public int reserved1
        {
            get
            {
                return (complete >> 15) & 1;
            }
        }
        // reserved2
        public int reserved2
        {
            get
            {
                return (complete >> 16) & 131071;
            }
        }
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void logiGkeyCB(GkeyCode gkeyCode, [MarshalAs(UnmanagedType.LPWStr)]String gkeyOrButtonString, IntPtr context);

    public struct logiGKeyCbContext
    {
        public logiGkeyCB gkeyCallBack;
        public IntPtr gkeyContext;
    }

    [DllImport("LogitechGKeyEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern int LogiGkeyInitWithoutCallback();

    [DllImport("LogitechGKeyEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern int LogiGkeyInitWithoutContext(logiGkeyCB gkeyCB);

    [DllImport("LogitechGKeyEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern int LogiGkeyInit(ref logiGKeyCbContext cbStruct);

    [DllImport("LogitechGKeyEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern int LogiGkeyIsMouseButtonPressed(int buttonNumber);

    [DllImport("LogitechGKeyEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr LogiGkeyGetMouseButtonString(int buttonNumber);

    public static String LogiGkeyGetMouseButtonStr(int buttonNumber)
    {
        String str = Marshal.PtrToStringUni(LogiGkeyGetMouseButtonString(buttonNumber));
        return str;
    }

    [DllImport("LogitechGKeyEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern int LogiGkeyIsKeyboardGkeyPressed(int gkeyNumber, int modeNumber);

    [DllImport("LogitechGKeyEnginesWrapper")]
    private static extern IntPtr LogiGkeyGetKeyboardGkeyString(int gkeyNumber, int modeNumber);

    public static String LogiGkeyGetKeyboardGkeyStr(int gkeyNumber, int modeNumber)
    {
        String str = Marshal.PtrToStringUni(LogiGkeyGetKeyboardGkeyString(gkeyNumber, modeNumber));
        return str;
    }

    [DllImport("LogitechGKeyEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern void LogiGkeyShutdown();

    //STEERING WHEEL SDK
    public const int LOGI_MAX_CONTROLLERS = 2;

    //Force types

    public const int LOGI_FORCE_NONE = -1;
    public const int LOGI_FORCE_SPRING = 0;
    public const int LOGI_FORCE_CONSTANT = 1;
    public const int LOGI_FORCE_DAMPER = 2;
    public const int LOGI_FORCE_SIDE_COLLISION = 3;
    public const int LOGI_FORCE_FRONTAL_COLLISION = 4;
    public const int LOGI_FORCE_DIRT_ROAD = 5;
    public const int LOGI_FORCE_BUMPY_ROAD = 6;
    public const int LOGI_FORCE_SLIPPERY_ROAD = 7;
    public const int LOGI_FORCE_SURFACE_EFFECT = 8;
    public const int LOGI_NUMBER_FORCE_EFFECTS = 9;
    public const int LOGI_FORCE_SOFTSTOP = 10;
    public const int LOGI_FORCE_CAR_AIRBORNE = 11;


    //Periodic types  for surface effect

    public const int LOGI_PERIODICTYPE_NONE = -1;
    public const int LOGI_PERIODICTYPE_SINE = 0;
    public const int LOGI_PERIODICTYPE_SQUARE = 1;
    public const int LOGI_PERIODICTYPE_TRIANGLE = 2;


    //Devices types

    public const int LOGI_DEVICE_TYPE_NONE = -1;
    public const int LOGI_DEVICE_TYPE_WHEEL = 0;
    public const int LOGI_DEVICE_TYPE_JOYSTICK = 1;
    public const int LOGI_DEVICE_TYPE_GAMEPAD = 2;
    public const int LOGI_DEVICE_TYPE_OTHER = 3;
    public const int LOGI_NUMBER_DEVICE_TYPES = 4;

    //Manufacturer types

    public const int LOGI_MANUFACTURER_NONE = -1;
    public const int LOGI_MANUFACTURER_LOGITECH = 0;
    public const int LOGI_MANUFACTURER_MICROSOFT = 1;
    public const int LOGI_MANUFACTURER_OTHER = 2;


    //Model types

    public const int LOGI_MODEL_G27 = 0;
    public const int LOGI_MODEL_DRIVING_FORCE_GT = 1;
    public const int LOGI_MODEL_G25 = 2;
    public const int LOGI_MODEL_MOMO_RACING = 3;
    public const int LOGI_MODEL_MOMO_FORCE = 4;
    public const int LOGI_MODEL_DRIVING_FORCE_PRO = 5;
    public const int LOGI_MODEL_DRIVING_FORCE = 6;
    public const int LOGI_MODEL_NASCAR_RACING_WHEEL = 7;
    public const int LOGI_MODEL_FORMULA_FORCE = 8;
    public const int LOGI_MODEL_FORMULA_FORCE_GP = 9;
    public const int LOGI_MODEL_FORCE_3D_PRO = 10;
    public const int LOGI_MODEL_EXTREME_3D_PRO = 11;
    public const int LOGI_MODEL_FREEDOM_24 = 12;
    public const int LOGI_MODEL_ATTACK_3 = 13;
    public const int LOGI_MODEL_FORCE_3D = 14;
    public const int LOGI_MODEL_STRIKE_FORCE_3D = 15;
    public const int LOGI_MODEL_G940_JOYSTICK = 16;
    public const int LOGI_MODEL_G940_THROTTLE = 17;
    public const int LOGI_MODEL_G940_PEDALS = 18;
    public const int LOGI_MODEL_RUMBLEPAD = 19;
    public const int LOGI_MODEL_RUMBLEPAD_2 = 20;
    public const int LOGI_MODEL_CORDLESS_RUMBLEPAD_2 = 21;
    public const int LOGI_MODEL_CORDLESS_GAMEPAD = 22;
    public const int LOGI_MODEL_DUAL_ACTION_GAMEPAD = 23;
    public const int LOGI_MODEL_PRECISION_GAMEPAD_2 = 24;
    public const int LOGI_MODEL_CHILLSTREAM = 25;
    public const int LOGI_NUMBER_MODELS = 26;

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct LogiControllerPropertiesData
    {
        public bool forceEnable;
        public int overallGain;
        public int springGain;
        public int damperGain;
        public bool defaultSpringEnabled;
        public int defaultSpringGain;
        public bool combinePedals;
        public int wheelRange;
        public bool gameSettingsEnabled;
        public bool allowGameSettings;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct DIJOYSTATE2ENGINES
    {
        public int lX;                     /* x-axis position              */
        public int lY;                     /* y-axis position              */
        public int lZ;                     /* z-axis position              */
        public int lRx;                    /* x-axis rotation              */
        public int lRy;                    /* y-axis rotation              */
        public int lRz;                    /* z-axis rotation              */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] rglSlider;              /* extra axes positions         */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public uint[] rgdwPOV;                          /* POV directions               */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte[] rgbButtons;                     /* 128 buttons                  */
        public int lVX;                    /* x-axis velocity              */
        public int lVY;                    /* y-axis velocity              */
        public int lVZ;                    /* z-axis velocity              */
        public int lVRx;                   /* x-axis angular velocity      */
        public int lVRy;                   /* y-axis angular velocity      */
        public int lVRz;                   /* z-axis angular velocity      */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] rglVSlider;             /* extra axes velocities        */
        public int lAX;                    /* x-axis acceleration          */
        public int lAY;                    /* y-axis acceleration          */
        public int lAZ;                    /* z-axis acceleration          */
        public int lARx;                   /* x-axis angular acceleration  */
        public int lARy;                   /* y-axis angular acceleration  */

        public int lARz;                   /* z-axis angular acceleration  */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] rglASlider;             /* extra axes accelerations     */
        public int lFX;                    /* x-axis force                 */
        public int lFY;                    /* y-axis force                 */
        public int lFZ;                    /* z-axis force                 */
        public int lFRx;                   /* x-axis torque                */
        public int lFRy;                   /* y-axis torque                */
        public int lFRz;                   /* z-axis torque                */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] rglFSlider;                        /* extra axes forces            */
    };

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiSteeringInitialize(bool ignoreXInputControllers);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiSteeringShutdown();

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiUpdate();

    [DllImport("LogitechSteeringWheelEnginesWrapper", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr LogiGetStateENGINES(int index);

    public static DIJOYSTATE2ENGINES LogiGetStateUnity(int index)
    {
        DIJOYSTATE2ENGINES ret = new DIJOYSTATE2ENGINES();
        ret.rglSlider = new int[2];
        ret.rgdwPOV = new uint[4];
        ret.rgbButtons = new byte[128];
        ret.rglVSlider = new int[2];
        ret.rglASlider = new int[2];
        ret.rglFSlider = new int[2];
        try
        {
            ret = (DIJOYSTATE2ENGINES)Marshal.PtrToStructure(LogiGetStateENGINES(index), typeof(DIJOYSTATE2ENGINES));
        }
        catch (System.ArgumentException)
        {
            Debug.Log("Exception catched");
        }
        return ret;
    }

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiGetFriendlyProductName(int index, StringBuilder buffer, int bufferSize);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiIsConnected(int index);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiIsDeviceConnected(int index, int deviceType);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiIsManufacturerConnected(int index, int manufacturerName);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiIsModelConnected(int index, int modelName);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiButtonTriggered(int index, int buttonNbr);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiButtonReleased(int index, int buttonNbr);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiButtonIsPressed(int index, int buttonNbr);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiGenerateNonLinearValues(int index, int nonLinCoeff);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern int LogiGetNonLinearValue(int index, int inputValue);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiHasForceFeedback(int index);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiIsPlaying(int index, int forceType);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiPlaySpringForce(int index, int offsetPercentage, int saturationPercentage, int coefficientPercentage);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiStopSpringForce(int index);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiPlayConstantForce(int index, int magnitudePercentage);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiStopConstantForce(int index);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiPlayDamperForce(int index, int coefficientPercentage);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiStopDamperForce(int index);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiPlaySideCollisionForce(int index, int magnitudePercentage);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiPlayFrontalCollisionForce(int index, int magnitudePercentage);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiPlayDirtRoadEffect(int index, int magnitudePercentage);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiStopDirtRoadEffect(int index);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiPlayBumpyRoadEffect(int index, int magnitudePercentage);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiStopBumpyRoadEffect(int index);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiPlaySlipperyRoadEffect(int index, int magnitudePercentage);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiStopSlipperyRoadEffect(int index);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiPlaySurfaceEffect(int index, int type, int magnitudePercentage, int period);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiStopSurfaceEffect(int index);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiPlayCarAirborne(int index);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiStopCarAirborne(int index);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiPlaySoftstopForce(int index, int usableRangePercentage);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiStopSoftstopForce(int index);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiSetPreferredControllerProperties(LogiControllerPropertiesData properties);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiGetCurrentControllerProperties(int index, ref LogiControllerPropertiesData properties);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern int LogiGetShifterMode(int index);

    [DllImport("LogitechSteeringWheelEnginesWrapper", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool LogiPlayLeds(int index, float currentRPM, float rpmFirstLedTurnsOn, float rpmRedLine);
}
