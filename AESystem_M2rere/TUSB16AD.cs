//-----------------------------------------------
//API for TUSB-0216AD Driver
//Visual C# Module
//
//Feb. 05 2014
//Copyright (C) 2014 Turtle Industry Co.,Ltd.
//-----------------------------------------------

using System.Runtime.InteropServices;

class TUSB16AD
{
    [DllImport("TUSB16AD.DLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern short TUSB0216AD_Device_Open(short Id);
    [DllImport("TUSB16AD.DLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern void TUSB0216AD_Device_Close(short Id);
    [DllImport("TUSB16AD.DLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern short TUSB0216AD_DIO_In(short Id, out byte Data);
    [DllImport("TUSB16AD.DLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern short TUSB0216AD_DIO_Out(short Id,byte Data);
    [DllImport("TUSB16AD.DLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern short TUSB0216AD_DIO_Chk(short Id,out byte Data);
    [DllImport("TUSB16AD.DLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern short TUSB0216AD_Ad_Single(short Id, [Out] int[] Data);
    [DllImport("TUSB16AD.DLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern short TUSB0216AD_Start(short Id,byte Ch,int PreLen,byte TrgType,byte TrgCh);
    [DllImport("TUSB16AD.DLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern short TUSB0216AD_Stop(short Id);
    [DllImport("TUSB16AD.DLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern short TUSB0216AD_Ad_Status(short Id, out byte Status, [Out] byte[] OverFlow, [Out] int[] DataLen);
    [DllImport("TUSB16AD.DLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern short TUSB0216AD_Ad_Data(short Id, byte Ch, [Out] int[] Data, out int DataLen);
    [DllImport("TUSB16AD.DLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern short TUSB0216AD_AdClk_Set(short Id, int ClkTime, byte Sel);
    [DllImport("TUSB16AD.DLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern short TUSB0216AD_Level_Set(short Id, int Level, short Hys);
    [DllImport("TUSB16AD.DLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern short TUSB0216AD_Input_Set(short Id, byte Type1, byte Type2);
    [DllImport("TUSB16AD.DLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern short TUSB0216AD_Trigger(short Id);
    [DllImport("TUSB16AD.DLL", CallingConvention = CallingConvention.Cdecl)]
    public static extern short TUSB0216AD_Input_Check(short Id, out byte Type1, out byte Type2);

    public static string GetErrMessage(short retcode)
    {
        switch (retcode)
        {
            case 0:
                return "正常終了しました";
            case 1:
                return "ID番号が不正です";
            case 2:
                return "ドライバがインストールされていません";
            case 3:
                return "すでにデバイスはオープンされています";
            case 4:
                return "接続されている台数が多すぎます";
            case 5:
                return "オープンできませんでした";
            case 6:
                return "デバイスがみつかりません";
            case 8:
                return "パラメータエラー";
            case 9:
                return "USB通信エラーです";
            case 11:
                return "連続取込動作中です";
            default:
                return "不明なエラーです";
        }
    }
}
