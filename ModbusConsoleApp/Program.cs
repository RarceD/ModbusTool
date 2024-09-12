using ModbusLib;
using ModbusLib.Protocols;
using System;
using System.IO.Ports;
using System.Threading;

namespace ModbusConsoleApp
{
    internal class Program
    {
        static private ModbusClient _driver;
        static private ICommClient _portClient;
        static private int _transactionId;

        static void Main(string[] args)
        {
            // ONLY CHANGE THIS PARAMS:
            string PortName = "COM10";
            byte SlaveId = 1;

            // Actual test code:
            int Baud = 9600;
            int DataBits = 8;
            StopBits StopBits = StopBits.One;
            Parity Parity = System.IO.Ports.Parity.None;

            var _uart = new SerialPort(PortName, Baud, Parity, DataBits, StopBits);
            _uart.Open();
            _portClient = _uart.GetClient();
            _driver = new ModbusClient(new ModbusRtuCodec()) { Address = SlaveId };
            var reader = new Reader(_driver, _portClient);

            // Run actual test code:
            // RunSetGetTimeTest();
            // RunValveProgramIrrigationTest();
            // RunWaterPercentageTest();
            // RunHourProgramTimeTest();
            int time = reader.ReadOneHoldingRegister(MemmoryMap.MODBUS_GENERAL_TIME_GET);
            var hours = Math.Truncate((double)(time / 60));
            var min = time - hours * 60;

            _uart.Close();
            while (true) { }
        }

        private static void WriteHoldingRegister(int StartAddress, int DataLength, ushort[] data)
        {
            var command = new ModbusCommand(ModbusCommand.FuncWriteMultipleRegisters)
            {
                Offset = StartAddress,
                Count = DataLength,
                TransId = _transactionId++,
                Data = data
            };
            var result = _driver.ExecuteGeneric(_portClient, command);
            if (result.Status == CommResponse.Ack)
            {
                foreach (var d in command.Data)
                {
                    Console.WriteLine($"Write succeeded: FC: {ModbusCommand.FuncWriteMultipleRegisters} -> {d}");
                }
            }
            else
            {
                Console.WriteLine(String.Format("Read ERRRO => Function code:{0}.", result.Status));
            }
        }
        private static void ReadHoldingRegister(int StartAddress, int DataLength)
        {
            var command = new ModbusCommand(ModbusCommand.FuncReadMultipleRegisters) { Offset = StartAddress, Count = DataLength, TransId = _transactionId++ };
            var result = _driver.ExecuteGeneric(_portClient, command);
            if (result.Status == CommResponse.Ack)
            {
                foreach (var data in command.Data)
                {
                    Console.WriteLine($"Read succeeded: FC: {ModbusCommand.FuncReadMultipleRegisters} -> {data}");
                }
            }
            else
            {
                Console.WriteLine(String.Format("Read ERROR => Function code:{0}.", result.Status));
            }
        }
        public static void RunSetGetTimeTest()
        {
            ReadHoldingRegister(MemmoryMap.MODBUS_GENERAL_TIME_GET, 1);
            var DataLength = 2;
            DateTime now = DateTime.Now;
            ushort timeInMinutes = (ushort)(now.Hour * 60 + now.Minute);
            ushort weekday = (ushort)now.DayOfWeek;
            WriteHoldingRegister(MemmoryMap.MODBUS_GENERAL_TIME_SET, DataLength, new ushort[] { timeInMinutes, weekday });
        }
        public static void RunValveProgramIrrigationTest()
        {

            var DataLength = 1;
            ushort[] timeInMinutes = new ushort[] { (ushort)(120 + 10) };
            for (ushort i = 0; i < 20; i++)
            {
                WriteHoldingRegister(MemmoryMap.MODBUS_SET_VALVE_TIME_PROGRAM_A + i, DataLength, timeInMinutes);
                ReadHoldingRegister(MemmoryMap.MODBUS_VALVE_TIME_PROGRAM_A + i, DataLength);
                WriteHoldingRegister(MemmoryMap.MODBUS_SET_VALVE_TIME_PROGRAM_B + i, DataLength, timeInMinutes);
                ReadHoldingRegister(MemmoryMap.MODBUS_VALVE_TIME_PROGRAM_B + i, DataLength);
                Thread.Sleep(200);
                WriteHoldingRegister(MemmoryMap.MODBUS_SET_VALVE_TIME_PROGRAM_C + i, DataLength, timeInMinutes);
                ReadHoldingRegister(MemmoryMap.MODBUS_VALVE_TIME_PROGRAM_C + i, DataLength);
                WriteHoldingRegister(MemmoryMap.MODBUS_SET_VALVE_TIME_PROGRAM_D + i, DataLength, timeInMinutes);
                ReadHoldingRegister(MemmoryMap.MODBUS_VALVE_TIME_PROGRAM_D + i, DataLength);
                WriteHoldingRegister(MemmoryMap.MODBUS_SET_VALVE_TIME_PROGRAM_E + i, DataLength, timeInMinutes);
                Thread.Sleep(200);
                ReadHoldingRegister(MemmoryMap.MODBUS_VALVE_TIME_PROGRAM_E + i, DataLength);
                WriteHoldingRegister(MemmoryMap.MODBUS_SET_VALVE_TIME_PROGRAM_F + i, DataLength, timeInMinutes);
                ReadHoldingRegister(MemmoryMap.MODBUS_VALVE_TIME_PROGRAM_F + i, DataLength);
            }


        }
        public static void RunHourProgramTimeTest()
        {
            var DataLength = 1;
            for (ushort i = 0; i < 6; i++)
            {
                ushort[] timeInMinutes = new ushort[] { (ushort)(120 + i * 10) };
                WriteHoldingRegister(MemmoryMap.MODBUS_SET_START_PROGRAM_A + i, DataLength, timeInMinutes);
                ReadHoldingRegister(MemmoryMap.MODBUS_GET_START_PROGRAM_A + i, DataLength);
                Thread.Sleep(200);

                WriteHoldingRegister(MemmoryMap.MODBUS_SET_START_PROGRAM_B + i, DataLength, timeInMinutes);
                ReadHoldingRegister(MemmoryMap.MODBUS_GET_START_PROGRAM_B + i, DataLength);

                WriteHoldingRegister(MemmoryMap.MODBUS_SET_START_PROGRAM_C + i, DataLength, timeInMinutes);
                ReadHoldingRegister(MemmoryMap.MODBUS_GET_START_PROGRAM_C + i, DataLength);
                Thread.Sleep(200);

                WriteHoldingRegister(MemmoryMap.MODBUS_SET_START_PROGRAM_D + i, DataLength, timeInMinutes);
                ReadHoldingRegister(MemmoryMap.MODBUS_GET_START_PROGRAM_D + i, DataLength);

                WriteHoldingRegister(MemmoryMap.MODBUS_SET_START_PROGRAM_E + i, DataLength, timeInMinutes);
                ReadHoldingRegister(MemmoryMap.MODBUS_GET_START_PROGRAM_E + i, DataLength);
                Thread.Sleep(200);

                WriteHoldingRegister(MemmoryMap.MODBUS_SET_START_PROGRAM_F + i, DataLength, timeInMinutes);
                ReadHoldingRegister(MemmoryMap.MODBUS_GET_START_PROGRAM_F + i, DataLength);
            }
        }
        public static void RunWaterPercentageTest()
        {
            ushort[] waterPercentage = new ushort[] { 123 };
            var DataLength = 1;

            WriteHoldingRegister(MemmoryMap.WRITE_WATER_PROGRAM_A, DataLength, waterPercentage);
            ReadHoldingRegister(MemmoryMap.READ_WATER_PROGRAM_A, DataLength);

            WriteHoldingRegister(MemmoryMap.WRITE_WATER_PROGRAM_B, DataLength, waterPercentage);
            ReadHoldingRegister(MemmoryMap.READ_WATER_PROGRAM_B, DataLength);

            WriteHoldingRegister(MemmoryMap.WRITE_WATER_PROGRAM_C, DataLength, waterPercentage);
            ReadHoldingRegister(MemmoryMap.READ_WATER_PROGRAM_C, DataLength);

            WriteHoldingRegister(MemmoryMap.WRITE_WATER_PROGRAM_D, DataLength, waterPercentage);
            ReadHoldingRegister(MemmoryMap.READ_WATER_PROGRAM_D, DataLength);

            WriteHoldingRegister(MemmoryMap.WRITE_WATER_PROGRAM_E, DataLength, waterPercentage);
            ReadHoldingRegister(MemmoryMap.READ_WATER_PROGRAM_E, DataLength);

            WriteHoldingRegister(MemmoryMap.WRITE_WATER_PROGRAM_F, DataLength, waterPercentage);
            ReadHoldingRegister(MemmoryMap.READ_WATER_PROGRAM_F, DataLength);
        }

    }
}
