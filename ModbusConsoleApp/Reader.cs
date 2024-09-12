using ModbusLib.Protocols;
using ModbusLib;
using System;

namespace ModbusConsoleApp
{
    public class Reader
    {
        private readonly ModbusClient _driver;
        private readonly ICommClient _portClient;
        private int _transactionId;
        public Reader(ModbusClient driver, ICommClient portClient)
        {
            _driver = driver;
            _portClient = portClient;
        }

        public int ReadOneHoldingRegister(int StartAddress)
        {
            int dataLength = 1;
            var command = new ModbusCommand(ModbusCommand.FuncReadMultipleRegisters) { Offset = StartAddress, Count = dataLength, TransId = _transactionId++ };
            var result = _driver.ExecuteGeneric(_portClient, command);
            if (result.Status == CommResponse.Ack)
            {
                foreach (var data in command.Data)
                {
                    return data;
                }
            }
            Console.WriteLine(String.Format("Read ERROR => Function code:{0}.", result.Status));
            return 0;
        }
    }
}
