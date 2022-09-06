
using System;
// using static GtmLogicalLayerTestingCSharpExample.TestStructClass;
// using static GtmLogicalLayerTestingCSharpExample.GtmLogicalLayerInterface;
using GtmLogicalLayerTestingCSharpExample;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using System.IO;

//using static System.Net.Mime.MediaTypeNames;
namespace GtmLogicalLayerTestingCSharpExample
{
    class TestCoreClass
    {
        #region Global Var Define
        private static readonly int[] gGtmTestItemsArray = {
                (int)TestStructClass.TestItemId.TP_VERSION_TEST_ITEM_ID,
                (int)TestStructClass.TestItemId.TP_CHIP_CFG_PROC_ITEM_ID,
                };

        private static TestStructClass.TestInfo gModuleTestInfo
            = new TestStructClass.TestInfo{
                curDevId = 0,
                status = TestStructClass.TestStatus.TEST_NOT_START,
            };

        #endregion

        #region PRIVATE INTERFACE

        /// <summary>
        /// Check and update Goodix Guitar Board version
        /// </summary>
        private static bool CheckAndUpdateGoodixGuitarBoardVersion(int boardId)
        {
            System.Threading.Thread.Sleep(3000);//ms
            //**********************************
            //Check test board connected or not
            //**********************************
            int TestBoardSta = -1;
            GtmLogicalLayerInterface.GtTestDevConnStatus(boardId, ref TestBoardSta);
            gModuleTestInfo.boardStatus = TestBoardSta;
            if (1 != TestBoardSta)
            {
                Console.WriteLine("Failed: Goodix Guitar Board not connect!!! {0}", TestBoardSta);
                return false;
            }

            int devDetailLen = 0;
            IntPtr pTestDevDetaiPtr = new IntPtr();

            pTestDevDetaiPtr = Marshal.AllocHGlobal(256);
            GtmLogicalLayerInterface.GtTestDevDetail(boardId, ref devDetailLen, pTestDevDetaiPtr);
            Console.WriteLine("Goodix Guitar Board Detail {0}", Marshal.PtrToStringAnsi(pTestDevDetaiPtr));

            //*************************************
            //check test board code update or not
            //*************************************
            byte bNeedUpdate = 0;
            if (0 == GtmLogicalLayerInterface.GtIsNeedUpdateTestDev(boardId, ref bNeedUpdate))
            {
                if (bNeedUpdate == 1)
                {
                    Console.WriteLine("Goodix Guitar Board Update Start");
                    int ret = GtmLogicalLayerInterface.GtUpdateTestDev(boardId);
                    if (0 == ret)
                    {
                        Console.WriteLine("Goodix Guitar Board Update Success");
                    }
                    else
                    {
                        Console.WriteLine("Failed: Goodix Guitar Board Update Fail");
                    }
                    System.Threading.Thread.Sleep(2000);//ms
                }
            }
            return true;
        }
        /// <summary>
        /// VersionTest测试开始前回调
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private static int VersionTestBeforeCallBack(IntPtr infoPtr)
        {
            TestStructClass.TestInfo info = (TestStructClass.TestInfo)Marshal.PtrToStructure(infoPtr, typeof(TestStructClass.TestInfo));
            Console.WriteLine("Check module version: {0}", TestStructClass.TestStatus.TEST_START);
            return 1;
        }
        /// <summary>
        /// VersionTest测试结束后回调
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private static int VersionTestFinishCallBack(IntPtr infoPtr)
        {
            TestStructClass.TestInfo info = (TestStructClass.TestInfo)Marshal.PtrToStructure(infoPtr, typeof(TestStructClass.TestInfo));
            byte[] moduleVerPtr = new byte[64];
            int verLen = 0;
            GtmLogicalLayerInterface.GtGetModuleVersion(info.curDevId, ref verLen, moduleVerPtr);
            string moduleVer = System.Text.Encoding.UTF8.GetString(moduleVerPtr);

            Console.WriteLine("Check module version status: {0},  module ver: {1} ",
                TestStructClass.TestStatus.TEST_FINISH, moduleVer);
            return 1;
        }

        /// <summary>
        /// 坐标获取回调函数
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private static int GetCoordDataCallback(IntPtr coordStruct)
        {
            try
            {
                object coordParamTmp = null;
                coordParamTmp = Marshal.PtrToStructure(coordStruct, typeof(TestStructClass.CoordParam));
                TestStructClass.CoordParam coordParam = (TestStructClass.CoordParam)coordParamTmp;
                object obj = Marshal.PtrToStructure(coordParam.pCoordData, typeof(TestStructClass.CoordData));
                TestStructClass.CoordData coord = (TestStructClass.CoordData)obj;

                //coordDataList.Add(coord);
                TestStructClass.CoordPoint[] point = coord.pointsArr;

                string output = string.Empty;
                string output_file = string.Empty;
                if (coord.pointNum > 0)
                {
                    //wchen
                    output_file = "[ID,X,Y,SIZE]";
                    for (int i = 0; i < coord.pointNum && i < TestStructClass.MAX_TOUCH_POINT_NUM; i++)
                    {
                        TestStructClass.CoordPoint p = point[i];
                        //wchen
                        output_file += string.Format("[{0,2:D}, {1,4:D}, {2,4:D}, {3,4:D}] ", p.trackId, p.x, p.y, p.size);
                    }
                }

                if (coord.bHaveRealTimeGesture == 1)
                {
                    if (coord.realtimeGestNum <= TestStructClass.REALTIME_GESTURE_PACK_NUM)
                    {
                        TestStructClass.TpRealTimeGestureData[] realTimeGest = coord.RealTimeGestAtrr;
                        for (int i = 0; i < coord.realtimeGestNum; i++)
                        {
                            TestStructClass.TpRealTimeGestureData r = realTimeGest[i];
                            //wchen
                            output_file += "RealTime Gesture ID1[";
                            output_file += r.trackId1.ToString();
                            output_file += "], ID2[";
                            output_file += r.trackId2.ToString();
                            output_file += "], Gesture Type[";
                            output_file += r.type.ToString();
                            output_file += "],[";
                            output_file += r.data1.ToString();
                            output_file += ",";
                            output_file += r.data2.ToString();
                            output_file += ",";
                            output_file += r.data3.ToString();
                            output_file += "]";
                        }
                    }

                }

                if (coord.bHaveGesture == 1)
                {
                    if (coord.handUpGestNum <= TestStructClass.HAND_UP_GESTRUE_PACK_NUM)
                    {
                        TestStructClass.TpHandUpGestureData[] handUpGest = coord.handUpGestAtrr;
                        for (int i = 0; i < coord.handUpGestNum && i < TestStructClass.MAX_TOUCH_POINT_NUM; i++)
                        {
                            output += "Hand Up Gesture Type = ";
                            output += handUpGest[i].type.ToString();
                            TestStructClass.TpHandUpGesturePoint[] handUpPo = handUpGest[i].pointsArr;
                            for (int j = 0; j < handUpGest[i].pointNum; j++)
                            {
                                output += ",ID = ";
                                output += handUpPo[j].trackID.ToString();
                                output += ", Start(";
                                output += handUpPo[j].start.x.ToString();
                                output += ",";
                                output += handUpPo[j].start.y.ToString();
                                output += "), End(";
                                output += handUpPo[j].end.x.ToString();
                                output += ",";
                                output += handUpPo[j].end.y.ToString();
                                output += ")]";
                            }
                        }
                    }
                    Console.WriteLine(output);
                    //disable timer
                    touch = false;
                    stateTimer.Change(Timeout.Infinite, Timeout.Infinite);
                }


                if (output_file != string.Empty)
                {
                    //first time touch
                    if (touch == false)
                    {
                        touch = true;
                        Console.WriteLine("Finger touched, detecting gesture...");
                        File.AppendAllText(file_path, "Finger touched, detecting gesture..." + '\n');
                    }  
                    //reset the timer to 1 second
                    stateTimer.Change(1000, Timeout.Infinite);  
                    File.AppendAllText(file_path, output_file + '\n');
                }

                //write coordinates
                Console.WriteLine(output_file);

                //write gesture
                if (output != string.Empty)
                {
                    File.AppendAllText(file_path, output + '\n' + '\n');
                    //Touch detected, disable timer
                    stateTimer.Change(Timeout.Infinite, Timeout.Infinite);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            return 1;
        }

        //timer to signaling gesture not detected
        public static System.Threading.Timer stateTimer;
        public static void  CheckStatus(Object stateInfo)
        {
            File.AppendAllText(file_path, "No Gesture Detected!" + '\n' +'\n');
            Console.WriteLine("No Gesture Detected!");
            touch = false;
            stateTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }


        /// <summary>
        /// Gtm logical layer library callback function
        /// </summary>
        private static IntPtr gCallbackPtr = new IntPtr();

        private static TestStructClass.CoordParam coordCallbackParam = new TestStructClass.CoordParam();
        private static IntPtr pCoordCallbackParam = new IntPtr();
        private static GtmLogicalLayerInterface.EXT_TEST_CALLBACK coordCallbackFunc = GetCoordDataCallback;

        private static GtmLogicalLayerInterface.EXT_TEST_CALLBACK VersionTestBeforeCallback = VersionTestBeforeCallBack;
        private static GtmLogicalLayerInterface.EXT_TEST_CALLBACK VersionTestFinishCallback = VersionTestFinishCallBack;

        private static void RegisterTestCallBack(int boardId)
        {
            TestStructClass.TestInfo info = gModuleTestInfo;
            int size = Marshal.SizeOf(info);
            gCallbackPtr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(info, gCallbackPtr, false);

            //some test items test before
            int versionTestItemId = (int)TestStructClass.TestItemId.TP_VERSION_TEST_ITEM_ID;
            GtmLogicalLayerInterface.GtRegisterDeviceFuncOfItemTestBefore(boardId, versionTestItemId, VersionTestBeforeCallback, gCallbackPtr);
            GtmLogicalLayerInterface.GtRegisterDeviceFuncOfItemTestFinished(boardId, versionTestItemId, VersionTestFinishCallback, gCallbackPtr);

            //注册坐标上报回调函数
            size = Marshal.SizeOf(coordCallbackParam);
            pCoordCallbackParam = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(coordCallbackParam, pCoordCallbackParam, false);
            GtmLogicalLayerInterface.GtRegisterCoordinateSubscribe(boardId, coordCallbackFunc, pCoordCallbackParam);
        }
        /// <summary>
        /// UnRegisterTestCallBack
        /// </summary>
        private static void UnRegisterTestCallBack(int boardId)
        {
            //some test items test before
            int versionTestItemId = (int)TestStructClass.TestItemId.TP_VERSION_TEST_ITEM_ID;
            GtmLogicalLayerInterface.GtUnRegisterDeviceFuncOfItemTestBefore(boardId, versionTestItemId, VersionTestBeforeCallback, gCallbackPtr);
            GtmLogicalLayerInterface.GtUnRegisterDeviceFuncOfItemTestFinished(boardId, versionTestItemId, VersionTestFinishCallback, gCallbackPtr);

            GtmLogicalLayerInterface.GtUnRegisterCoordinateSubscribe(boardId, coordCallbackFunc, pCoordCallbackParam);
        }
        /// <summary>
        /// 卸载GTM Logical layer library
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private static int UnRegisterGtmLogicalLayerLib(int boardId)
        {
            int ret = 0;
            //**********************************
            //UnRegister test call back func
            //**********************************
            UnRegisterTestCallBack(boardId);

            //****************************
            //UnInit Logical layer dll
            //****************************
            ret = GtmLogicalLayerInterface.GtTestUnInit();
            return ret;
        }

        #endregion

        #region PUBLIC INTERFACE
        /// <summary>
        /// GTModuleTest
        /// </summary>
        /// <param name="orderPath">工单绝对路径</param>
        public static void GTModuleTest(string orderPath)
        {
            try
            {
                int ret = 0;
                int boardId = 0;

                //***********************************
                //Init Logical_layer.dll by tpOrder
                //***********************************
                ret = GtmLogicalLayerInterface.GtTestInit(orderPath);
                if (0 != ret)
                {
                    Console.WriteLine("Failed: Import order and init failed");
                    return;
                }

                //***********************************
                //Get Logical_layer.dll version
                //***********************************
                int LogicalLayerVerLen = 0;
                StringBuilder sb = new StringBuilder();
                ret = GtmLogicalLayerInterface.GetLogicalLayerVersion(ref LogicalLayerVerLen, sb);
                string strLibVer = sb.ToString();
                Console.WriteLine("Logical layer lib ver:{0}", strLibVer);

                //**************************************
                //Check and Update Guitar TestTool code
                //**************************************
                bool bRet = CheckAndUpdateGoodixGuitarBoardVersion(boardId);
                if (!bRet){
                    return;
                }
    
                Console.WriteLine("GTM logical layer lib init OK");

                //******************************
                //create log folder and log file
                //******************************
                string Path = Environment.CurrentDirectory + "\\full_logs";
                bool exists = System.IO.Directory.Exists(Path);
                if (!exists)
                    System.IO.Directory.CreateDirectory(Path);



                //******************************
                //register test call back func
                //******************************
                RegisterTestCallBack(boardId);
                stateTimer = new Timer(CheckStatus, null, Timeout.Infinite, 3000);
                //*******************************
                //check TP module connected or not
                //*******************************
                int TpModuleSta = -1;
                GtmLogicalLayerInterface.GtModuleConnStauts(boardId, ref TpModuleSta);
                gModuleTestInfo.moduleStatus = TpModuleSta;
                if (1 != TpModuleSta)
                {
                    Console.WriteLine("Failed: Module Not Connect");
                    return;
                }

                //*************************
                //Start Test to confirm F/W and config are right
                //*************************
                do
                {
                    gModuleTestInfo.status = TestStructClass.TestStatus.TEST_START;
                    gModuleTestInfo.time.startTime = System.DateTime.Now;
                    ret = GtmLogicalLayerInterface.GtStartTest(boardId);

                    Console.WriteLine("start test at: {0}", gModuleTestInfo.time.startTime);

                    int testStatus = 0;
                    GtmLogicalLayerInterface.GtGetTestStatus(boardId, ref testStatus);
                    gModuleTestInfo.status = (TestStructClass.TestStatus)testStatus;
                    if (gModuleTestInfo.status == TestStructClass.TestStatus.TEST_NOT_START)
                    {
                        break;//not start test
                    }

                    //**********************************
                    //wait test finish
                    //**********************************
                    do
                    {
                        System.Threading.Thread.Sleep(1000);//ms
                        GtmLogicalLayerInterface.GtGetTestStatus(boardId, ref testStatus);
                        gModuleTestInfo.status = (TestStructClass.TestStatus)testStatus;
                    }while (TestStructClass.TestStatus.TEST_FINISH != gModuleTestInfo.status);

                    if (TestStructClass.TestStatus.TEST_DOING != gModuleTestInfo.status)
                    {
                        GtmLogicalLayerInterface.GtStopTest(boardId);
                    }

                    gModuleTestInfo.time.finishTime = System.DateTime.Now;
                    TimeSpan tStart = new TimeSpan(gModuleTestInfo.time.startTime.Ticks);
                    TimeSpan tFinish = new TimeSpan(gModuleTestInfo.time.finishTime.Ticks);
                    TimeSpan tSpent = tFinish.Subtract(tStart).Duration();

                    Console.WriteLine("Finish test at {0}, spent time: {1}min {2}s {3}ms",
                        boardId, tFinish, tSpent.Minutes, tSpent.Seconds, tSpent.Milliseconds);
                    Console.WriteLine("Success: Starting to report coordinates and gestures");
                } while (false)
                
                ;

                do
                {
                    int key = Console.Read();
                    if(key == 13)
                        break;
                    System.Threading.Thread.Sleep(500);//ms
                } while (true);

                //******************
                //UnRegister GtmLogicalLayer Library and callback
                //******************
                ret = UnRegisterGtmLogicalLayerLib(boardId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public static string file_path = string.Empty;
        private static bool touch = false;
        public static void CreateFile()
        {
            string Path = Environment.CurrentDirectory + "\\full_logs";
            bool exists = System.IO.Directory.Exists(Path);
            if (!exists)
                System.IO.Directory.CreateDirectory(Path);
            file_path = Path + "\\" + System.DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + "_log.txt";
        }
        #endregion
    }
}

