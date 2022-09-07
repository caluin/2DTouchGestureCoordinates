using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using static GtmLogicalLayerTestingCSharpExample.TestCoreClass;
using GtmLogicalLayerTestingCSharpExample;

namespace GtmLogicalLayerTestingCSharpExample
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("2DTouchCoordinatesGesture");
            //*********************************************************
            //Get tpOrder path,or import one *.tporder file
            //*********************************************************
            string TpOrderName = string.Empty;
            string Input = string.Empty;
            string DeviceConfig = string.Empty;
            try
            {
                Input = args[0].ToString();
                DeviceConfig = args[1].ToString();
                if (Input != "h" && Input != "H" && Input != "L" && Input != "l")
                {
                    Input = "H";
                }
                if (DeviceConfig!="1" && DeviceConfig != "2" && DeviceConfig != "3" && DeviceConfig != "4")
                {
                    DeviceConfig = "1";
                }
            }
            catch
            {
                Input = "H";
                DeviceConfig = "1";
            }

            if (Input == "H" || Input == "h")
            {
                Console.WriteLine("Device will be configured to high resolution.");
                if (DeviceConfig == "1")
                {
                    TpOrderName = "GT6311_21.00.FF.08_CFGV1_20220808_Rx24Tx3_01_AA1&AA2&AA3.tporder";
                }
                else if (DeviceConfig == "2")
                {
                    TpOrderName = "GT6311_21.00.FF.08_CFGV1_20220808_Rx24Tx3_02_AA1&AA2.tporder";
                }
                else if (DeviceConfig == "3")
                {
                    TpOrderName = "GT6311_21.00.FF.08_CFGV1_20220808_Rx24Tx3_03_AA2&AA3.tporder";
                }
                else if (DeviceConfig == "4")
                {
                    TpOrderName = "GT6311_21.00.FF.08_CFGV1_20220808_Rx24Tx3_04_AA2.tporder";
                }
            }

            else if (Input == "L" || Input == "l")
            {
                Console.WriteLine("Device will be configured to low resolution.");

                if (DeviceConfig == "1")
                {
                    TpOrderName = "GT6311_21.00.FF.08_CFGV1_20220809_Rx16Tx3_01_AA1&AA2&AA3.tporder";
                }
                else if (DeviceConfig == "2")
                {
                    TpOrderName = "GT6311_21.00.FF.08_CFGV1_20220809_Rx16Tx3_02_AA1&AA2.tporder";
                }
                else if (DeviceConfig == "3")
                {
                    TpOrderName = "GT6311_21.00.FF.08_CFGV1_20220809_Rx16Tx3_03_AA2&AA3.tporder";
                }
                else if (DeviceConfig == "4")
                {
                    TpOrderName = "GT6311_21.00.FF.08_CFGV1_20220809_Rx16Tx3_04_AA2.tporder";
                }
            }

            Console.WriteLine("Device Configuration is: " + DeviceConfig);
            Console.WriteLine("Device TpOrder name is: " + TpOrderName);

            string TpOrderPath = "\\Tp_System\\Order\\" + TpOrderName;
            string ExePath = Environment.CurrentDirectory;
            //get full tporder file path
            string orderPathStr = ExePath + TpOrderPath;
            int strLength = orderPathStr.Length;
            Console.WriteLine("TpOrderPathFile:{0}", orderPathStr);

            TestCoreClass.CreateFile();
            TestCoreClass.GTModuleTest(orderPathStr);

            Console.WriteLine("GTM LOGICAL LAYER UNREGISTER");
            Console.ReadLine();
        }
    }
}
