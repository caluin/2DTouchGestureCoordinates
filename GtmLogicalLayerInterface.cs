using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace GtmLogicalLayerTestingCSharpExample
{
    class GtmLogicalLayerInterface
    {
        /*******************************************************************************
        * Function Name  : GetLogicalLayerVersion
        * Description    : Get version of the GTM logical_layer.dll 
        * Input          : ref int len:ver buffer size
        * Output         : string ver:version buffer
        * Return         : int(0: ok; other: operation fails, error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtGetVersion", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetLogicalLayerVersion(ref int len, StringBuilder ver);
        /*******************************************************************************
        * Function Name : GtTestInit
        * Description : Test Initialization
        * Input : string tpOrderPath (work order path, ends with ’\0’)
        * Output : None
        * Return : int(0: Initialization succeeds; other: Initialization fails, error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtTestInit", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtTestInit(string tpOrderPath);
        /*******************************************************************************
        * Function Name : GtGetTestDevNum
        * Description : Obtain the number of test devices(test board)
        * Input : None
        * Output : None
        * Return : int(the number of test devices)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtGetTestDevNum", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtGetTestDevNum();
        /*******************************************************************************
        * Function Name : GtTestUnInit
        * Description : Release the resources requested by GtTestInit
        * Input : None
        * Output : None
        * Return : int(0: Succeed; other: Fail, error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtTestUnInit", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtTestUnInit();
        /*******************************************************************************
        * Function Name : GtTestDevDetail
        * Description : Obtain the details of a certain test device (test device refers to the device under test)
        * Input : int testDevId (test device ID, if there is only one test device, use 0)
        * Input : ref int pDevDetailLen (test device detail length pointer)
        * Input : string pBuf (test device detail buffer)
        * Output : ref int pDevDetailLen (test device detail length pointer)
        * Output : string pBuf (test device detail buffer, in xml format and ends with ’\0’)
                    //<TestDevDetail>
                    //  <BoardVersion></BoardVersion>
                    //	<BoardUid></BoardUid>
                    //<TestDevDetail>
        * Return : int(0: Succeed; other: Fail, error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtTestDevDetail", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtTestDevDetail(int testDevId, ref int pDevDetailLen, IntPtr pBuf);
        /*******************************************************************************
        * Function Name  : GtTestDevId
        * Description    : Get Test Device Id In logical layer,not is device SN number
        * Input          : int pDevMarkLen
        * Input          : string pBuf
        * Output         : ref int pTestDevId
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtGetChipUID", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtGetChipUID(int testDevId, byte[] pBuf, ref short buflen);
        /*******************************************************************************
        * Function Name  : GtTestDevId
        * Description    : Get Test Device Id In logical layer,not is device SN number
        * Input          : int pDevMarkLen
        * Input          : string pBuf
        * Output         : ref int pTestDevId
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtTestDevId", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtTestDevId(int pDevMarkLen, string pBuf, ref int pTestDevId);
        /*******************************************************************************
        * Function Name  : GtTestDevConnStatus
        * Description    : Get Test Device Connnected Status
        * Input          : int testDevId（device id,start from 0）
        * Output         : ref int pConnectedStatus
                            0:disconnected
                            1:connected
                            2:connect abnormal
                            3:power off connected
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtTestDevConnStatus", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtTestDevConnStatus(int testDevId, ref int pConnectedStatus);
        /*******************************************************************************
        * Function Name  : GtTestDevConnCommId
        * Description : Get Test Device SN number(need wrote it by GTMTOrderGen.exe before use it)
        * Input: int testDevId（device id,start from 0）
        * Output : ref byte  pCommDevId(0:no id,start from 1)
        * Return: int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtTestDevConnCommId", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtTestDevConnCommId(int testDevId, ref byte pCommDevId);
        /*******************************************************************************
        * Function Name : GtIsNeedUpdateTestDev
        * Description : Is it necessary to update test board firmware or not
        * Input : int testDevId  test board ID
        * Input   :ref byte  bNeed  stores the result pointer
        * Output   :ref byte  bNeed  1: Need to update ; 0: Unnecessary to update
        * Return : int(0: Succeed; other: Fail, error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtIsNeedUpdateTestDev", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtIsNeedUpdateTestDev(int testDevId, ref byte bNeed);
        /*******************************************************************************
        * Function Name : GtUpdateTestDev
        * Description : Upgrade the firmware of a certain test device
        * Input : int testDevId (test device ID, if there is only test device, use 0)
        * Output : None
        * Return : int(0: Succeed; other: Fail, error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtUpdateTestDev", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtUpdateTestDev(int testDevId);
        /*******************************************************************************
        * Function Name : GtTestDevLatestVersion
        * Description : Obtain the latest version of test board
        * Output : ref int pVersionLen (pointer length)
        * Output : string pBuf (version number buffer)
        * Return : int(0: Succeed; other: Fail, error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtTestDevLatestVersion", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtTestDevLatestVersion(ref int pVersionLen, string pBuf);
        /*******************************************************************************
        * Function Name : GtModuleConnStauts
        * Description : Obtain the module connection status
        * Input : int testDevId  The ID of the test board that is connected to the module
        * Output : ref int pConnectedStatus  0： Unconnected; 1: Connected
        * Return : int(0: Succeed; other: Fail, error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtModuleConnStauts", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtModuleConnStauts(int testDevId, ref int pConnectedStatus);
        /*******************************************************************************
        * Function Name : GtGetModuleVersion
        * Description : Obtain Module Version Number
        * Input : int testDevId  The ID of the test board that is connected to the module
        * Output : ref int pVerLen  String length , 
        * Output : string pBuf  version number string
        * Return : int(0: Succeed; other: Fail, error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtGetModuleVersion", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtGetModuleVersion(int testDevId, ref int pVerLen, byte[] pBuf);
        /*******************************************************************************
        * Function Name : GtGetModuleCurrent
        * Description : Obtain module current
        * Input : int testDevId The ID of the test board that is connected to the module int* pCurrent  The pointer that stores the current value
        * Output: ref int pCurrent  The pointer that stores the current value
        * Return : int(0: Succeed; other: Fail, error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtGetModuleCurrent", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtGetModuleCurrent(int testDevId, ref int pCurrent);
        /*******************************************************************************
        * Function Name : GtGetModuleVddioCurrent
        * Description : Obtain module Vddio current
        * Input : int testDevId The ID of the test board that is connected to the module int* pCurrent  The pointer that stores the current value, (device id,start from 0)
        * Output: ref int pCurrent  The pointer that stores the current value
        * Return : int(0: Succeed; other: Fail, error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtGetModuleVddioCurrent", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtGetModuleVddioCurrent(int testDevId, ref int pCurrent);
        /*******************************************************************************
        * Function Name : GtModulePanelConnStauts
        * Description : Obtain the touch panel connection status
        * Input : int testDevId the ID of the test board that is connected to the touch panel int * pConnectedStatus stores the connection status pointer
        * Output: ref int pConnectedStatus stores the connection status pointer 1: connected; 0: unconnected
        * Return : int(0: Succeed; other: Fail, error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtModulePanelConnStauts", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtModulePanelConnStauts(int testDevId, ref int pConnectedStatus);
        /*******************************************************************************
        * Function Name : GtGetTestLogPath
        * Description : Obtain the test log path of a certain test device
        * Input : int testDevId (test device ID, if there is only test device, use 0)
        * Input : ref int pLogPathLen (test log path length)
        * Input : string pBuf (test path storage buffer)
        * Output : ref int pLogPathLen (test log path length)
        * Output : string pBuf (test path storage buffer)
        * Return : int(0: Succeed; other: Fail, error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtGetTestLogPath", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtGetTestLogPath(int testDevId, ref int pLogPathLen, IntPtr pBuf);
        /*******************************************************************************
        * Function Name : GtSetProjectName
        * Description : Set project name, this function affects the name of the test log
        * Input :  int len (the recommended length does not exceed 16bytes)
        * Input :  string pBuf
        * Output : None
        * Return : int(0: Succeed; other: Fail, error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtSetProjectName", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtSetProjectName(int len, string pBuf);
        /*******************************************************************************
        * Function Name : GtSetProductBatch
        * Description : Set product batch, this function affects the name of the test log
        * Input :  int len (the recommended length does not exceed 16bytes)
        * Input :  string pBuf
        * Output : None
        * Return : int(0: Succeed; other: Fail, error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtSetProductBatch", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtSetProductBatch(int len, string pBuf);
        /*******************************************************************************
        * Function Name : GtSetTestStation
        * Description : Set test station, this function affects the name of the test log
        * Input :  int len (the recommended length does not exceed 16bytes)
        * Input :  string pBuf
        * Output : None
        * Return : int(0: Succeed; other: Fail, error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtSetTestStation", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtSetTestStation(int len, string pBuf);
        /*******************************************************************************
        * Function Name : GtSetWorkerId
        * Description : Set worker ID, this function will affect the name of the test log
        * Input :  int len (the recommended length does not exceed 16bytes)
        * Input :  string pBuf
        * Output : None
        * Return : int(0: Succeed; other: Fail, error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtSetWorkerId", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtSetWorkerId(int len, string pBuf);
        /*******************************************************************************
        * Function Name : GtSetBarcode
        * Description : Set barcode/QR code for a certain test device
        * Input : int testDevId (test device ID, if there is only test device, use 0)
        * Input : int barcodeLen (barcode/QR code length, the recommended length does not exceed 64bytes)
        * Input : string pBuf (barcode/QR code buffer)
        * Output : None
        * Return : int(0: Succeed; other: Fail, error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtSetBarcode", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtSetBarcode(int testDevId, int barcodeLen, string pBuf);
        /*******************************************************************************
        * Function Name : GtStartTest
        * Description : start test one device by test device id
        * Input : int testDevId (test device ID, if there is only test device, use 0)
        * Output : None
        * Return : int(0: Succeed; other: Fail, error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtStartTest", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtStartTest(int testDevId);
        /*******************************************************************************
        * Function Name : GtStartTestAll
        * Description: start test all devices
        * Input : int testDevId (test device ID, if there is only test device, use 0)
        * Output : None
        * Return : int(0: Succeed; other: Fail, error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtStartTestAll", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtStartTestAll();
        /*******************************************************************************
        * Function Name : GtStopTest
        * Description: stop test one device by test device id
        * Input : None
        * Output : None
        * Return : int(0: Succeed; other: Fail, error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtStopTest", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtStopTest(int testDevId);
        /*******************************************************************************
        * Function Name  : GtStopTestAll
        * Description    : Stop Test All devices
        * Input          : none
        * Output         : none
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtStopTestAll", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtStopTestAll();
        /*******************************************************************************
        * Function Name  : GtGetTestDaemonStatus
        * Description    : get test status of the logical layer lib
        * Input          : none
        * Output         : int* pDaemonStatus
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtGetTestDaemonStatus", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtGetTestDaemonStatus(ref int pDaemonStatus);
        /*******************************************************************************
        * Function Name  : GtSetTestDaemonStatus
        * Description    : set test daemon status(you can only set TestDaemonRuning = 2,TestDaemonPaused = 3)
                           will return fail if you set others.
        * Input          : int DaemonStatus
                            TestDaemonPrepared = 0,
                            TestDaemonStarting = 1,
                            TestDaemonRuning = 2,
                            TestDaemonPaused = 3,
                            TestDaemonStoping = 4,
                            TestDaemonStopped = 5
        * Output         : 
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtSetTestDaemonStatus", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtSetTestDaemonStatus(int daemonStatus);
        /*******************************************************************************
        * Function Name  : GtGetTestStatus
        * Description    : get test status of one test device by id
        * Input          : int testDevId（device id,start from 0）
        * Output         : ref int pTestStatus
                                TEST_NOT_START = 0x00,
                                TEST_START = 0x01,
                                TEST_DOING = 0x02,
                                TEST_FINISH = 0x03,
                                TEST_ABORTED = 0x04
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtGetTestStatus", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtGetTestStatus(int testDevId, ref int pTestStatus);
        /*******************************************************************************
        * Function Name  : GtGetTestBreakEventFlag
        * Description    : get test break event flag
        * Input          : int testDevId（device id,start from 0）
        * Output         : ref int pTestBreakFlag
                            TEST_BREAK_NONE		0x00
                            TEST_BREAK_BY_USER	0x01
                            TEST_BREAK_BY_NG	0x02

        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtGetTestBreakEventFlag", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtGetTestBreakEventFlag(int testDevId, ref int pTestBreakFlag);
        /*******************************************************************************
        * Function Name  : GtGetTestTimes
        * Description    : get current test n times
        * Input          : int testDevId（device id,start from 0）
        * Output         : int* pTestTimes
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtGetTestTimes", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtGetTestTimes(int testDevId, ref int pTestTimes);
        /*******************************************************************************
        * Function Name  : GtTestResult
        * Description    : Get Test Result Of Test Device
        * Input          : int testDevId（device id,start from 0）
        * Output         : ref int pDevDetailLen
        * Output         : string pBuf("OK","NG","WARNING","BREAK")
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtTestResult", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtTestResult(int testDevId, ref int pResultLen, byte[] pBuf);
        /*******************************************************************************
        * Function Name  : GtTestItemResult
        * Description: Obtain the test result of a certain test device for a certain test item
        * Input : int testDevId (test device ID, if there is only test device, use 0)
        * Input : int testItemId (test item Id)
        * Output : ref int pResultLen (byte length of the test result )
        * Output : string pBuf (test result byte stream, can be in xml format and ends with ’\0’)
                   like this:
                <Item name="Version Test">
                    <ItemHeader>
                        <TestId>600</TestId>
                        <TestResult>0</TestResult>
                    </ItemHeader>
                    <CurVerDataHex>
                    0x37,0x32,0x38,0x38,0x08,0x00,0x01
                    </CurVerDataHex>
                </Item>
        * Return  : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtTestItemResult", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtTestItemResult(int testDevId, int testItemId, ref int pResultLen, string pBuf);
        /*******************************************************************************
        * Function Name  : GetModuleRealTimeConSta
        * Description    : Get the real-time connection status of the module
        * Input          : int devId
        * Return         : int(0:ok，other:err)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtGetModuleRealTimeConSta", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtGetModuleRealTimeConSta(int devId);

        /*******************************************************************************
         * CALLBACK FUNCTION INTERFACE DEFINE
         *******************************************************************************/
        [UnmanagedFunctionPointerAttribute(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public delegate int EXT_TEST_CALLBACK(IntPtr pParam);

        /*******************************************************************************
        * Function Name  : GtRegisterFuncOfItemsTestBefore
        * Description    : Register function that called before all test items.
                           When you call interface "GtStartTest",before all test items,
                           this callback will be called.
        * Input          : EXT_TEST_CALLBACK func
        * Input          : void* pParam
        * Output         : none
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtRegisterFuncOfItemsTestBefore", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtRegisterFuncOfItemsTestBefore(EXT_TEST_CALLBACK func, IntPtr pParam);
        /*******************************************************************************
        * Function Name  : GtUnRegisterFuncOfItemsTestBefore
        * Description    : UnRegister function that called before all test items.
        * Input          : EXT_TEST_CALLBACK func
        * Input          : void* pParam
        * Output         : none
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtUnRegisterFuncOfItemsTestBefore", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtUnRegisterFuncOfItemsTestBefore(EXT_TEST_CALLBACK func, IntPtr pParam);

        /*******************************************************************************
        * Function Name  : GtRegisterFuncOfItemsTestFinished
        * Description    : Register function that called after all test items Finished.
        * Input          : EXT_TEST_CALLBACK func
        * Input          : void* pParam
        * Output         : none
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtRegisterFuncOfItemsTestFinished", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtRegisterFuncOfItemsTestFinished(EXT_TEST_CALLBACK func, IntPtr pParam);
        /*******************************************************************************
        * Function Name  : GtRegisterFuncOfItemsTestFinished
        * Description    : Register function that called after all test items finished.
        * Input          : EXT_TEST_CALLBACK func
        * Input          : void* pParam
        * Output         : none
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtUnRegisterFuncOfItemsTestFinished", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtUnRegisterFuncOfItemsTestFinished(EXT_TEST_CALLBACK func, IntPtr pParam);

        /*******************************************************************************
        * Function Name  : GtRegisterDeviceFuncOfItemsTestBefore
        * Description    : Register function that called before all test items.
        When you call interface "GtStartTest",before all test items,
        this callback will be called.
        * Input          : int devId:the test device number need register 
        * Input          : EXT_TEST_CALLBACK func
        * Input          : void* pParam
        * Output         : none
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtRegisterDeviceFuncOfItemsTestBefore", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtRegisterDeviceFuncOfItemsTestBefore(int devId, EXT_TEST_CALLBACK func, IntPtr pParam);
        /*******************************************************************************
        * Function Name  : GtUnRegisterFuncOfItemsTestBefore
        * Description    : UnRegister function that called before all test items.
        * Input          : int devId
        * Input          : EXT_TEST_CALLBACK func
        * Input          : void* pParam
        * Output         : none
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtUnRegisterDeviceFuncOfItemsTestBefore", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtUnRegisterDeviceFuncOfItemsTestBefore(int devId, EXT_TEST_CALLBACK func, IntPtr pParam);

        /*******************************************************************************
        * Function Name  : GtRegisterDeviceFuncOfItemsTestFinished
        * Description    : Register function that called after all test items Finished.
        * Input          : int devId
        * Input          : EXT_TEST_CALLBACK func
        * Input          : void* pParam
        * Output         : none
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtRegisterDeviceFuncOfItemsTestFinished", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtRegisterDeviceFuncOfItemsTestFinished(int devId, EXT_TEST_CALLBACK func, IntPtr pParam);
        /*******************************************************************************
        * Function Name  : GtUnRegisterFuncOfItemsTestFinished
        * Description    : Register function that called after all test items finished.
        * Input          : int devId
        * Input          : EXT_TEST_CALLBACK func
        * Input          : void* pParam
        * Output         : none
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtUnRegisterDeviceFuncOfItemsTestFinished", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtUnRegisterDeviceFuncOfItemsTestFinished(int devId, EXT_TEST_CALLBACK func, IntPtr pParam);

        /*******************************************************************************
        * Function Name  : GtRegisterFuncOfItemTestBefore
        * Description    : Register function that called before some test item starting.
        * Input          : EXT_TEST_CALLBACK func
        * Input          : void* pParam
        * Output         : none
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtRegisterFuncOfItemTestBefore", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtRegisterFuncOfItemTestBefore(int itemId, EXT_TEST_CALLBACK func, IntPtr pParam);
        /*******************************************************************************
        * Function Name  : GtUnRegisterFuncOfItemTestBefore
        * Description    : UnRegister function that called before some test item starting.
        * Input          : EXT_TEST_CALLBACK func
        * Input          : void* pParam
        * Output         : none
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtUnRegisterFuncOfItemTestBefore", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtUnRegisterFuncOfItemTestBefore(int itemId, EXT_TEST_CALLBACK func, IntPtr pParam);

        /*******************************************************************************
        * Function Name  : GtRegisterFuncOfItemTestFinished
        * Description    : Register function that called after some test item finished.
        * Input          : EXT_TEST_CALLBACK func
        * Input          : void* pParam
        * Output         : none
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtRegisterFuncOfItemTestFinished", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtRegisterFuncOfItemTestFinished(int itemId, EXT_TEST_CALLBACK func, IntPtr pParam);
        /*******************************************************************************
        * Function Name  : GtUnRegisterFuncOfItemTestFinished
        * Description    : UnRegister function that called after some test item finished.
        * Input          : EXT_TEST_CALLBACK func
        * Input          : void* pParam
        * Output         : none
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtUnRegisterFuncOfItemTestFinished", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtUnRegisterFuncOfItemTestFinished(int itemId, EXT_TEST_CALLBACK func, IntPtr pParam);

        /*******************************************************************************
        * Function Name  : GtRegisterDeviceFuncOfItemTestBefore
        * Description    : Register function that called before some test item starting.
        * Input          : int devId,
        * Input          : int itemId,
        * Input          : EXT_TEST_CALLBACK func
        * Input          : void* pParam
        * Output         : none
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtRegisterDeviceFuncOfItemTestBefore", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtRegisterDeviceFuncOfItemTestBefore(int devId, int itemId, EXT_TEST_CALLBACK func, IntPtr pParam);
        /*******************************************************************************
        * Function Name  : GtUnRegisterDeviceFuncOfItemTestBefore
        * Description    : UnRegister function that called before some test item starting.
        * Input          : int devId
        * Input          : EXT_TEST_CALLBACK func
        * Input          : void* pParam
        * Output         : none
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtUnRegisterDeviceFuncOfItemTestBefore", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtUnRegisterDeviceFuncOfItemTestBefore(int devId, int itemId, EXT_TEST_CALLBACK func, IntPtr pParam);

        /*******************************************************************************
        * Function Name  : GtRegisterDeviceFuncOfItemTestFinished
        * Description    : Register function that called after some test item finished.
        * Input          : int itemId
        * Input          : EXT_TEST_CALLBACK func
        * Input          : void* pParam
        * Output         : none
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtRegisterDeviceFuncOfItemTestFinished", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtRegisterDeviceFuncOfItemTestFinished(int devId, int itemId, EXT_TEST_CALLBACK func, IntPtr pParam);
        /*******************************************************************************
        * Function Name  : GtUnRegisterDeviceFuncOfItemTestFinished
        * Description    : UnRegister function that called after some test item finished.
        * Input          : int devId
        * Input          : EXT_TEST_CALLBACK func
        * Input          : void* pParam
        * Output         : none
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtUnRegisterDeviceFuncOfItemTestFinished", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtUnRegisterDeviceFuncOfItemTestFinished(int devId, int itemId, EXT_TEST_CALLBACK func, IntPtr pParam);

        /*******************************************************************************
        * Function Name  : GtRegisterFuncOfItemTestGeneral
        * Description    : Register function that called while some test item General.
        * Input          : EXT_TEST_CALLBACK func
        * Input          : void* pParam
        * Output         : none
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtRegisterFuncOfItemTestGeneral", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtRegisterFuncOfItemTestGeneral(int itemId, EXT_TEST_CALLBACK func, IntPtr pParam);
        /*******************************************************************************
        * Function Name  : GtUnRegisterFuncOfItemTestGeneral
        * Description    : UnRegister function that called while some test item General.
        * Input          : EXT_TEST_CALLBACK func
        * Input          : void* pParam
        * Output         : none
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtUnRegisterFuncOfItemTestGeneral", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtUnRegisterFuncOfItemTestGeneral(int itemId, EXT_TEST_CALLBACK func, IntPtr pParam);

        /*******************************************************************************
        * Function Name  : GtRegisterDeviceFuncOfItemTestGeneral
        * Description    : Register function that called while some test item General.
        * Input          : int itemId
        * Input          : EXT_TEST_CALLBACK func
        * Input          : void* pParam
        * Output         : none
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtRegisterDeviceFuncOfItemTestGeneral", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtRegisterDeviceFuncOfItemTestGeneral(int devId, int itemId, EXT_TEST_CALLBACK func, IntPtr pParam);
        /*******************************************************************************
        * Function Name  : GtUnRegisterDeviceFuncOfItemTestGeneral
        * Description    : UnRegister function that called while some test item General.
        * Input          : int devId
        * Input          : EXT_TEST_CALLBACK func
        * Input          : void* pParam
        * Output         : none
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtUnRegisterDeviceFuncOfItemTestGeneral", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtUnRegisterDeviceFuncOfItemTestGeneral(int devId, int itemId, EXT_TEST_CALLBACK func, IntPtr pParam);

        /*******************************************************************************
        * Function Name  : GtRegisterCoordinateSubscribe
        * Description    : Register Coordinate Subscribe
        * Input          : int testDevId
        * Input          : EXT_TEST_CALLBACK func
        * Input          : void* pParam(PST_COORD_WRAP_DATA defined in TpCoordDataStruct.h)
        * Output         : none
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtRegisterCoordinateSubscribe", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtRegisterCoordinateSubscribe(int testDevId, EXT_TEST_CALLBACK func, IntPtr pParam);
        /*******************************************************************************
        * Function Name  : GtUnRegisterCoordinateSubscribe
        * Description    : UnRegister Coordinate Subscribe
        * Input          : int testDevId
        * Input          : EXT_TEST_CALLBACK func
        * Input          : void* pParam(PST_COORD_WRAP_DATA defined in TpCoordDataStruct.h)
        * Output         : none
        * Return         : int(0:Ok，other:error code)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtUnRegisterCoordinateSubscribe", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtUnRegisterCoordinateSubscribe(int testDevId, EXT_TEST_CALLBACK func, IntPtr pParam);

        /*******************************************************************************
        * Function Name  : GtCallTpDevTestBeforeOpr
        * Description    : Call the operation before testing for TP
        * Input          : int testDevId(Device ID)
        * Return         : uint(1:handled, 0:unhandled)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtCallTpDevTestBeforeOpr", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtCallTpDevTestBeforeOpr(int testDevId);
        /*******************************************************************************
        * Function Name  : GtCallTpDevTestFinishOpr
        * Description    : Call the operation after testing for TP
        * Input          : int testDevId(Device ID)
        * Return         : uint(1:handled, 0:unhandled)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtCallTpDevTestFinishOpr", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtCallTpDevTestFinishOpr(int testDevId);
        /*******************************************************************************
        * Function Name  : GtSwitchDataMode
        * Description    : switch chip report data mode
        * Input          : int testDevId(Device ID)
        * Input          : int mode
        *                :  typedef enum CurrentDataMode
                            {
                                COORD_DATA_MODE = 0x00,
                                RAW_DATA_MODE = 0x01,
                                DIFF_DATA_MODE = 0x02,
                                BASE_DATA_MODE = 0x03,
                                SELF_RAW_DATA_MODE = 0x04,
                                CHIP_GESTURE_MODE = 0x05,
                                UNKNOWN_DATA_MODE = 0x06,
                                STOPPED_DATA_MODE = 0x07,
                                SELF_DIFF_DATA_MODE = 0x08,
                                SELF_BASE_DATA_MODE = 0x09,
                                SHARE_SYNCDATA_MODE = 0x0A,
                            };
        * Input          : byte sw,1:report data,0:not report data
        * Return         : uint(1:handled, 0:unhandled)
        *******************************************************************************/
        [DllImport("logical_layer.dll", CharSet = CharSet.Ansi, EntryPoint = "GtSwitchDataMode", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GtSwitchDataMode(int testDevId,int mode,byte sw);
    }
}