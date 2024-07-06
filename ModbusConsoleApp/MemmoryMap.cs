namespace ModbusConsoleApp
{
    public static class MemmoryMap
    {
        public static int READ_WATER_PROGRAM_A = 0x04;
        public static int READ_WATER_PROGRAM_B = READ_WATER_PROGRAM_A + 1;
        public static int READ_WATER_PROGRAM_C = READ_WATER_PROGRAM_A + 2;
        public static int READ_WATER_PROGRAM_D = READ_WATER_PROGRAM_A + 3;
        public static int READ_WATER_PROGRAM_E = READ_WATER_PROGRAM_A + 4;
        public static int READ_WATER_PROGRAM_F = READ_WATER_PROGRAM_A + 5;

        public static int WRITE_WATER_PROGRAM_A = 10;
        public static int WRITE_WATER_PROGRAM_B = WRITE_WATER_PROGRAM_A + 1;
        public static int WRITE_WATER_PROGRAM_C = WRITE_WATER_PROGRAM_A + 2;
        public static int WRITE_WATER_PROGRAM_D = WRITE_WATER_PROGRAM_A + 3;
        public static int WRITE_WATER_PROGRAM_E = WRITE_WATER_PROGRAM_A + 4;
        public static int WRITE_WATER_PROGRAM_F = WRITE_WATER_PROGRAM_A + 5;


        public static int LEN_GET_START_PROGRAM = 6;
        public static int MODBUS_GET_START_PROGRAM_A = 0x10;
        public static int MODBUS_GET_START_PROGRAM_B = (MODBUS_GET_START_PROGRAM_A + LEN_GET_START_PROGRAM * 1);
        public static int MODBUS_GET_START_PROGRAM_C = (MODBUS_GET_START_PROGRAM_A + LEN_GET_START_PROGRAM * 2);
        public static int MODBUS_GET_START_PROGRAM_D = (MODBUS_GET_START_PROGRAM_A + LEN_GET_START_PROGRAM * 3);
        public static int MODBUS_GET_START_PROGRAM_E = (MODBUS_GET_START_PROGRAM_A + LEN_GET_START_PROGRAM * 4);
        public static int MODBUS_GET_START_PROGRAM_F = (MODBUS_GET_START_PROGRAM_A + LEN_GET_START_PROGRAM * 5);

        public static int LEN_SET_START_PROGRAM = 6;
        public static int MODBUS_SET_START_PROGRAM_A = 820;
        public static int MODBUS_SET_START_PROGRAM_B = (MODBUS_SET_START_PROGRAM_A + LEN_SET_START_PROGRAM * 1);
        public static int MODBUS_SET_START_PROGRAM_C = (MODBUS_SET_START_PROGRAM_A + LEN_SET_START_PROGRAM * 2);
        public static int MODBUS_SET_START_PROGRAM_D = (MODBUS_SET_START_PROGRAM_A + LEN_SET_START_PROGRAM * 3);
        public static int MODBUS_SET_START_PROGRAM_E = (MODBUS_SET_START_PROGRAM_A + LEN_SET_START_PROGRAM * 4);
        public static int MODBUS_SET_START_PROGRAM_F = (MODBUS_SET_START_PROGRAM_A + LEN_SET_START_PROGRAM * 5);
    }
}
