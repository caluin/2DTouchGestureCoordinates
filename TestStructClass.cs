using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace GtmLogicalLayerTestingCSharpExample
{
    class TestStructClass
    {
        public const int MAX_TOUCH_POINT_NUM = 10;
        public const int REALTIME_GESTURE_PACK_NUM = 3;
        public const int HAND_UP_GESTRUE_PACK_NUM = 1;

        [Serializable]
        [FlagsAttribute]
        public enum TestStatus
        {
            TEST_NOT_START = 0x00,
            TEST_START = 0x01,
            TEST_DOING = 0x02,
            TEST_FINISH = 0x03,
            TEST_ABORTED = 0x04,
        };

        [Serializable]
        [FlagsAttribute]
        public enum TestItemId
        {
            //open test
            TP_RAWDATA_TEST_ITEMS_SET_ID = 5,
            TP_DIFFDATA_TEST_ITEMS_SET_ID = 6,
            TP_SELFRAWDATA_TEST_ITEMS_SET_ID = 7,
            TP_MULTI_CONFIG_OPEN_TEST_ITEM_ID=8,
            TP_SELF_NOISE_TEST_ITEM_ID=9,
            TP_KEY_OPEN_TEST_ITEM_ID=10,
            TP_SELFDIFFDATA_TEST_ITEMS_SET_ID=11,

            //hardware test
            TP_SHORT_TEST_ITEM_ID = 200,
            TP_RST_VOLT_TEST_ITEM_ID = 201,
            TP_I2C_VOLT_TEST_ITEM_ID = 202,
            TP_INT_VOLT_TEST_ITEM_ID = 203,
            TP_PIN1_TEST_ITEM_ID = 204,
            TP_PIN2_TEST_ITEM_ID = 205,

            TP_ACTIVE_CURRENT_TEST_ITEM_ID = 206,
            TP_SLEEP_CURRENT_TEST_ITEM_ID = 207,
            TP_INT_CAP_TEST_ITEM_ID = 208,
            TP_MODULE_TYPE_TEST_ITEM_ID = 209,
            TP_GESTURE_TEST_ITEM_ID = 210,
            TP_SPI_VOLT_TEST_ITEM_ID = 211,
            TP_EXT_OSC_TEST_ITEM_ID = 212,
            TP_PIN3_TEST_ITEM_ID = 213,
            TP_PIN4_TEST_ITEM_ID = 214,
            TP_PIN5_TEST_ITEM_ID = 215,
            TP_PIN6_TEST_ITEM_ID = 216,
            TP_I2C_DEV_TEST_ITEM_ID = 217,
            TP_REG_VALUE_CHECK_ITEM_ID = 218,
            TP_MBIST_TEST_ITEM_ID = 219,

            //firmware test
            TP_VERSION_TEST_ITEM_ID = 600,
            TP_CHK_FW_RUN_STATE_ITEM_ID = 601,
            //TP_9886_REG_CHECK_ITEM_ID = 602,

            //line test or key touch test
            TP_KEY_TOUCH_TEST_ITEM_ID = 800,
            //TP_FINGER_LINE_TEST_ITEM_ID = 801,
            //TP_PEN_LINE_TEST_ITEM_ID = 802,
            //TP_TOUCH_PAD_LINE_TEST_ITEM_ID = 803,
            //TP_MULTI_KEY_TEST_ITEM_ID = 804,

            //other test
            TP_CHIP_CFG_PROC_ITEM_ID = 1200,		//cfg check,check cfg in chip whether ok or not,or send config
            TP_FLASH_TEST_ITEM_ID = 1201,		//flash test using send cfg ,reset chip,then read and compare cfg
            TP_SEND_SPEC_CFG_ITEM_ID = 1202,		//after test send special config 
            TP_UPDATE_SPEC_FW_ITEM_ID = 1203,		//after test update special firmware
            TP_CUSTOM_INFO_TEST_ITEM_ID = 1204,		//custom information:including write information or check information
            TP_CHK_CHIP_TEST_RESULT_ITEM_ID = 1205,		//check chip test result
            TP_CHK_MODULE_TEST_RESULT_ITEM_ID = 1206,		//check module test result
            TP_RECOVER_CFG_ITEM_ID = 1207,		//recovery config to chip
            TP_SAVE_RES_TO_IC_STORAGE_ITEM_ID = 1208,		//save result to ic storage 
            TP_CHIP_UID_PROCESS_ITEM_ID = 1209,		//tp chip uid process
            //TP_LCD_NOISE_DETECT_ITEM_ID = 1210,		//lcd noise test
            //TP_FPC_TEST_ITEM_ID = 1211,		//fpc test
            TP_CHK_CHIP_KEY_INFO_ITEM_ID = 1212,        //check chip key info test
            //TP_7863_NFC_ITEM_ID = 1213,        //7863 NFC test
            TP_BARCODE_TEST_ITEM_ID = 1214,        //barcode test (write to chip or read from chip and match with scanned barcode)
            //TP_FORCE_SENSOR_TEST_ITEM_ID = 1215,        //force sensor test
            //TP_HAPTIC_TEST_ITEM_ID = 1216,        //haptic test
            TP_ADC_JITTER_CHECK_TEST_ITEM_ID = 1217,        //adc jitter check test for gt9897s
            TP_DRVSEN_AS_GIO_TEST_ITEM_ID = 1218,        //drv and sense as gio test
        };

        [Serializable]
        public struct TestTime
        {
            public DateTime startTime;
            public DateTime finishTime;
            public string totalTime;
            TestTime(DateTime st, DateTime ft,string tt)
            {
                startTime = st;
                finishTime = ft;
                totalTime = tt;
            }
        }

        [Serializable]
        public struct TestInfo
        {
            public TestTime time;
            public TestStatus status;
            public int boardStatus;
            public int boardSN;
            public int moduleStatus;
            public int curDevId;
            TestInfo(TestTime t,TestStatus s, int boardSta, int boardSn,int moduleSta,int devId)
            {
                time = t;
                status = s;
                boardStatus = boardSta;
                boardSN = boardSn;
                moduleStatus = moduleSta;
                curDevId = devId;
            }
        };

        [Serializable]
        public struct ItemTestCallbackParam
        {
            public int boardId;
            public int itemId;
        };

        [Serializable]
        [FlagsAttribute]
        public enum GtmTPTouchType : byte
        {
            GTM_NORMAL_STATION = 0,
            GTM_HOVER_STATION = 1,
            GTM_FLIP_COVER_STATION = 2,
            GTM_GLOVE_STATION = 3,
            GTM_STYLUS_STATION = 4,
            GTM_PLAM_STATION = 5,
            GTM_WET_STATION = 6,
            GTM_PROXIMITY_STATION = 7,
        };

        [Serializable]
        [FlagsAttribute]
        public enum GtmTpPacketType:byte
        {
            GTM_GOODIX_FIGNER_PACK = 0,
            GTM_GOODIX_STYLUS_PACK = 1,
            GTM_GOODIX_STYLUS_FINGER_PACK = 2,
            GTM_SAMSUNG_TP_PACK = 3,
        };

        [Serializable]
        [FlagsAttribute]
        public enum GtmTpGestureType:byte
        {
            GestTypeNull = 0x00,
            GestSingleFingerSlideRight = 0xAA,
            GestSingleFingerSlideLeft = 0xBB,
            GestSingleFingerSlideDown = 0xAB,
            GestSingleFingerSlideUp = 0xBA,
            GestSingleFingerOneClick = (byte)'L',
            GestSingleFingerDoubleClick = 0xCC,
            GestSingleFingerTripleClick = 0xCD,
            GestSingleFingerLongPress = (byte)'a',
            GestTwoFingerSlideRight = (byte)'p',
            GestTwoFingerSlideLeft = (byte)'q',
            GestTwoFingerSlideDown = (byte)'H',
            GestTwoFingerSlideUp = (byte)'N',
            GestTwoFingerOneClick = 0x04,
            GestTwoFingerDoubleClick = (byte)'c',
            GestTwoFingerTripleClick = (byte)'d',
            GestTwoFingerLongPress = (byte)'e',
            GestTwoFingerZoomIn = (byte)'O',
            GestTwoFingerZoomOut = (byte)'I',
        };

        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct XYPoint
        {
            public UInt16 x;
            public UInt16 y;
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct TpRealTimeGestureData
        {
            public byte trackId1;
            public byte trackId2;
            public byte type;

            public byte data1;
            public byte data2;
            public byte data3;
            public byte res1;
            public byte res2;

        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct TpHandUpGesturePoint
        {
            public byte trackID;
            public byte res1;
            public byte res2;
            public byte res3;
            public XYPoint start;
            public XYPoint end;
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct TpHandUpGestureData
        {
            public GtmTpGestureType type;
            public byte pointNum;
            public byte res1;
            public byte res2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_TOUCH_POINT_NUM, ArraySubType = UnmanagedType.Struct)]
            public TpHandUpGesturePoint[] pointsArr;
        }
        
        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct CoordPoint
        {
            public byte bPenDwon;
            public byte bPenHover;
            public byte trackId;
            public GtmTPTouchType station;

            public byte majorX;
            public byte minorY;
            public UInt16 x;
            public UInt16 y;
            public UInt16 size;
            public Int16 angle_X;
            public Int16 angle_Y;

            public byte userCustomization1;
            public byte userCustomization2;
            public byte userCustomization3;
            public byte userCustomization4;

            public byte userCustomization5;
            public byte userCustomization6;
            public byte userCustomization7;
            public byte userCustomization8;
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct CoordData
        {
            public byte bProximity;
            public byte bLargeTouch;
            public byte bHaveKey;
            public byte bCorrdChkSumErr;

            public byte bTouched;
            public GtmTpPacketType packetType;
            public byte intCnt;

            public byte pointNum;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_TOUCH_POINT_NUM, ArraySubType = UnmanagedType.Struct)]
            public CoordPoint[] pointsArr;

            public byte bHaveGesture;
            public byte bHaveRealTimeGesture;
            public byte realtimeGestNum;
            public byte handUpGestNum;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = REALTIME_GESTURE_PACK_NUM, ArraySubType = UnmanagedType.Struct)]
            public TpRealTimeGestureData[] RealTimeGestAtrr;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HAND_UP_GESTRUE_PACK_NUM, ArraySubType = UnmanagedType.Struct)]
            public TpHandUpGestureData[] handUpGestAtrr;

            public byte key;
            public byte key2;
            public byte key3;
            public byte key4;
            public byte key5;
            public byte key6;
            public byte key7;

            public byte penKey;
            public UInt16 penPower;

            public UInt16 refreshRate;//Hz

            public byte bHaveSlider;
            public byte sliderId;
            public UInt16 sliderVal;

            public byte res0;
            public byte res1;
            public byte res2;
            public byte res3;


            public ulong timeStamps;
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct CoordParam
        {
            public IntPtr pThis;
            public IntPtr pCoordData;
        }
    }
}
