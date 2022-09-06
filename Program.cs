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
            try
            {
                Input = args[0].ToString();
            }
            catch
            {
                Input = "H";
            }
            if (Input == "H" || Input == "h")
            {
                Console.WriteLine("Device will be configured to high resolution.");
                TpOrderName = "GT6311_21.00.FF.08_CFGV1_20220808_Rx24Tx3_01_AA1&AA2&AA3.tporder";
            }
            else if (Input == "L" || Input == "l")
            {
                Console.WriteLine("Device will be configured to low resolution.");
                TpOrderName = "GT6311_21.00.FF.08_CFGV1_20220808_Rx24Tx3_04_AA2.tporder";
            }
            else
            {
                Console.WriteLine("Device will be configured to default configuration, high resolution.");
                TpOrderName = "GT6311-Glass-Base.tporder";
            }

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
