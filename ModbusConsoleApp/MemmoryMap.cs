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
    }
}
