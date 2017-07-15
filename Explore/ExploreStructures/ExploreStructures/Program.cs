using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace ExploreStructures
{
    class Program
    {

        //        Sub WriteData()
        //    Dim PatientRecord As Person
        //    Dim recordNumber As Integer
        //    '    Open file for random access.
        //    FileOpen(1, "C:\TESTFILE.txt", OpenMode.Binary)
        //    ' Loop 5 times.
        //    For recordNumber = 1 To 5
        //        ' Define ID.
        //        PatientRecord.ID = recordNumber
        //        ' Create a string.
        //        PatientRecord.Name = "Name " & recordNumber
        //        ' Write record to file.
        //        FilePut(1, PatientRecord)
        //    Next recordNumber
        //    FileClose(1)
        //End Sub

        public struct MyStruct1
        {
            public Int16 i;
            public Int32 j;
            public string s1;
            public string s2;
        }

        public struct MyStruct2
        {
            public Int16 i;
            public Int32 j;
            [VBFixedString(10)]
            public string s1;
            [VBFixedString(20)]
            public string s2;
        }

        const string connectionString = @"<hwsW?orkufgoqqD#gudfjocW>2:@gnlvfikM#pplvdhporE<38>wwphojW" + "\"" + @"orkufgoqqD>fu:77Egq @fsrytvcQ>8MDJQ @fJ#tfvW<,+*zgmftp@GNDP`HEJYTFV**GGUDEJGGE@TFYTFV*>DVBGaUFGOQQD++*,3383>WTPS**pqd1eolgtdg/jpf0uluqxhubh?UVQI++QFV>OQDRVPUR)@UTHTEGC)@VTLN`VUFUFED*>QQJWRJUETHF)@gduwpV" + "\"" + "bwcE";

        const string connectionString2 = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=easeworks-eng.easeinc.com)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orclew)));User Id = PHAL6; Password=peB657td;Connection Timeout = 60; Connection Lifetime = 90; Validate Connection = True;";

        const string connectionString3 = @"Provider=Microsoft.ACE.OLEDB.12.0;Persist Security Info=False;Data Source=C:\inetpub\Cummins77\MCR\config\easesys.mdb;";

        static void Main(string[] args)
        {
            //TestStructures();

            VBStructures.Security sec = new VBStructures.Security();

            string encryptedString;

            string result;
            int length;

            //result = sec.EncryptConnectionString("Hello World");

            encryptedString = sec.EncryptConnectionString(connectionString2);
            length = encryptedString.Length;

            result = sec.DecodeConnectionString(encryptedString);
            length = result.Length;


            result = sec.EncryptConnectionString(connectionString3);

            result = sec.DecodeConnectionString(connectionString);
            

        }

        private static void TestStructures()
        {

            //VBStructures.Class1.MyStruct1 vbStruct1 = new VBStructures.Class1.MyStruct1();
            //VBStructures.Class1.MyStruct2 vbStruct2 = new VBStructures.Class1.MyStruct2();

            //CSharpStructures.Class1.MyStruct1 csStruct1 = new CSharpStructures.Class1.MyStruct1();
            //CSharpStructures.Class1.MyStruct2 csStruct2 = new CSharpStructures.Class1.MyStruct2();

            VBStructures.Class1.MyStruct1 vbStruct1;
            VBStructures.Class1.MyStruct2 vbStruct2;

            CSharpStructures.Class1.MyStruct1 csStruct1;
            CSharpStructures.Class1.MyStruct2 csStruct2;

            MyStruct1 s1;
            MyStruct2 s2;

            s1.i = 1;
            s1.j = 2;
            s1.s1 = "Hello";
            s1.s2 = "World";

            string testString = "This is a 58 character message\n\rthat exceeds 20 characters";

            vbStruct1.i = 1;
            vbStruct1.j = 2;
            vbStruct1.s1 = testString;
            vbStruct1.s2 = testString;

            vbStruct2.i = 16;
            vbStruct2.j = 17;
            vbStruct2.s1 = testString;
            vbStruct2.s2 = testString;

            csStruct1.i = 32;
            csStruct1.j = 33;
            csStruct1.s1 = testString;
            csStruct1.s2 = testString;

            csStruct2.i = 48;
            csStruct2.j = 49;
            csStruct2.s1 = testString;
            csStruct2.s2 = testString;

            FileSystem.FileOpen(1, @"C:\temp\testfile1.txt", OpenMode.Binary);

            FileSystem.FilePut(1, vbStruct1);
            FileSystem.FilePut(1, vbStruct2);
            FileSystem.FilePut(1, csStruct1);
            FileSystem.FilePut(1, csStruct2);

            FileSystem.FileClose(1);

            //Console.WriteLine(string.Format("sizeof({0}) = {1}", "vbStruct1", sizeof(vbStruct1)));
            //Console.WriteLine(string.Format("sizeof({0}) = {1}", "vbStruct1", sizeof(vbStruct2)));

            //Console.WriteLine(string.Format("sizeof({0}) = {1}", "csStruct1", sizeof(csStruct1)));
            //Console.WriteLine(string.Format("sizeof({0}) = {1}", "csStruct1", sizeof(csStruct2)));



            Console.WriteLine(string.Format("sizeof({0}) = {1}", "vbStruct1",
                System.Runtime.InteropServices.Marshal.SizeOf(vbStruct1)));
            Console.WriteLine(string.Format("sizeof({0}) = {1}", "vbStruct2",
                System.Runtime.InteropServices.Marshal.SizeOf(vbStruct2)));

            Console.WriteLine(string.Format("sizeof({0}) = {1}", "csStruct1",
                System.Runtime.InteropServices.Marshal.SizeOf(csStruct1)));
            Console.WriteLine(string.Format("sizeof({0}) = {1}", "csStruct2",
                System.Runtime.InteropServices.Marshal.SizeOf(csStruct2)));

            Console.WriteLine(string.Format("{0}=>{1}<", "vbStruct1.s1", vbStruct1.s1));
            Console.WriteLine(string.Format("{0}=>{1}<", "vbStruct1.s2", vbStruct1.s2));
            Console.WriteLine(string.Format("{0}=>{1}<", "vbStruct2.s1", vbStruct2.s1));
            Console.WriteLine(string.Format("{0}=>{1}<", "vbStruct2.s2", vbStruct2.s2));

            Console.WriteLine(string.Format("{0}=>{1}<", "csStruct1.s1", csStruct1.s1));
            Console.WriteLine(string.Format("{0}=>{1}<", "csStruct1.s2", csStruct1.s2));
            Console.WriteLine(string.Format("{0}=>{1}<", "csStruct2.s1", csStruct2.s1));
            Console.WriteLine(string.Format("{0}=>{1}<", "csStruct2.s2", csStruct2.s2));

            Console.ReadLine();
        }
    }
}
