using System;
using System.Runtime.InteropServices;

namespace PVRTexLibNET
{
    public enum PixelFormat : ulong
    {
        PVRTCI_2bpp_RGB,
        PVRTCI_2bpp_RGBA,
        PVRTCI_4bpp_RGB,
        PVRTCI_4bpp_RGBA,
        PVRTCII_2bpp,
        PVRTCII_4bpp,
        ETC1,
        DXT1,
        DXT2,
        DXT3,
        DXT4,
        DXT5,

        //These formats are identical to some DXT formats.
        BC1 = DXT1,
        BC2 = DXT3,
        BC3 = DXT5,

        //These are currently unsupported:
        BC4,
        BC5,
        BC6,
        BC7,

        //These are supported
        UYVY,
        YUY2,
        BW1bpp,
        SharedExponentR9G9B9E5,
        RGBG8888,
        GRGB8888,
        ETC2_RGB,
        ETC2_RGBA,
        ETC2_RGB_A1,
        EAC_R11,
        EAC_RG11,

        RGB565 = 0x5060561626772,
        RGBA4444 = 0x404040461626772,
        RGBA8888 = 0x808080861626772,
    }

    public enum ResizeMode
    {
        Nearest,
		Linear,
		Cubic,
    }

    public enum VariableType
    {
        UnsignedByteNorm,
        SignedByteNorm,
        UnsignedByte,
        SignedByte,
        UnsignedShortNorm,
        SignedShortNorm,
        UnsignedShort,
        SignedShort,
        UnsignedIntegerNorm,
        SignedIntegerNorm,
        UnsignedInteger,
        SignedInteger,
        Float,
    }

    public enum ColourSpace
    {
        lRGB,
        sRGB
    }

    public enum CompressorQuality
    {
        PVRTCFast = 0,
		PVRTCNormal,
		PVRTCHigh,
		PVRTCBest,

		ETCFast = 0,
		ETCFastPerceptual,
		ETCMedium,
		ETCMediumPerceptual,
		ETCSlow,
		ETCSlowPerceptual
    }

    public static class PVRTexture
    {
        private const string dllName = "PVRTexLibWrapper.dll";

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CreateTexture(IntPtr data, uint u32Width, uint u32Height, uint u32Depth, PixelFormat ptFormat, bool preMultiplied, VariableType eChannelType, ColourSpace eColourspace);

        public static void CreateTexture<T>(T[] data, uint u32Width, uint u32Height, uint u32Depth, PixelFormat ptFormat, bool preMultiplied, VariableType eChannelType, ColourSpace eColourspace)
        {
            var gcHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            CreateTexture(gcHandle.AddrOfPinnedObject(), u32Width, u32Height, u32Depth, ptFormat, preMultiplied, eChannelType, eColourspace);
            gcHandle.Free();
        }

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CreateTexture(string filePath);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SaveTexture(string filePath);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DestroyTexture();

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Resize(uint u32NewWidth, uint u32NewHeight, uint u32NewDepth, ResizeMode eResizeMode);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GenerateMIPMaps(ResizeMode eFilterMode, uint uiMIPMapsToDo = int.MaxValue);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Transcode(PixelFormat ptFormat, VariableType eChannelType, ColourSpace eColourspace, CompressorQuality eQuality = CompressorQuality.PVRTCNormal, bool bDoDither = false);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint GetTextureDataSize(int iMIPLevel = -1);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetTextureData(IntPtr data, uint dataSize, uint uiMIPLevel = 0);

        public static void GetTextureData<T>(T[] data, uint dataSize, uint uiMIPLevel = 0)
        {
            var gcHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            GetTextureData(gcHandle.AddrOfPinnedObject(), dataSize, uiMIPLevel);
            gcHandle.Free();
        }
    }
}
