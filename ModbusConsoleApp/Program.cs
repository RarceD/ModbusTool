using ModbusLib;
using ModbusLib.Protocols;
using System;
using System.Globalization;
using System.IO.Ports;

namespace ModbusConsoleApp
{
    internal class Program
    {
        static private ModbusClient _driver;
        static private ICommClient _portClient;
        static private SerialPort _uart;
        static private int _transactionId;
        static ushort[] _registerData = new ushort[65000];

        static void Main(string[] args)
        {

            string PortName = "COM10";
            int Baud = 9600;
            int DataBits = 8;
            StopBits StopBits = StopBits.One;
            var Parity = System.IO.Ports.Parity.None;
            byte SlaveId = 1;

            _uart = new SerialPort(PortName, Baud, Parity, DataBits, StopBits);
            _uart.Open();
            _portClient = _uart.GetClient();
            _driver = new ModbusClient(new ModbusRtuCodec()) { Address = SlaveId };

            var StartAddress = 45;
            var DataLength = 1;
            var function = ModbusCommand.FuncReadMultipleRegisters;

            var command = new ModbusCommand(function) { Offset = StartAddress, Count = DataLength, TransId = _transactionId++ };
            var result = _driver.ExecuteGeneric(_portClient, command);
            if (result.Status == CommResponse.Ack)
            {
                Console.WriteLine(String.Format("Read succeeded: Function code:{0}.", function));
                foreach (var data in command.Data)
                {
                    Console.WriteLine($" Received : {data}");
                }
            }
            else
            {
                Console.WriteLine(String.Format("Read ERRRO => Function code:{0}.", result.Status));
            }
            while (true) { }

        }
    }
}
