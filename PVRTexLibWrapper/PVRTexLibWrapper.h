// PVRTexLibC.h

#pragma once

#include <PVRTGlobal.h>
#include <PVRTextureDefines.h>
#include <PVRTextureFormat.h>

using namespace pvrtexture;

extern "C" {
    __declspec(dllexport) void* __cdecl CreateTexture(void* data, uint32 u32Width, uint32 u32Height, uint32 u32Depth, PixelType ptFormat, bool preMultiplied, EPVRTVariableType eChannelType, EPVRTColourSpace eColourspace);

    __declspec(dllexport) void* __cdecl CreateTextureFromFile(const char* filePath);

    __declspec(dllexport) bool __cdecl SaveTexture(void* pPvrTexture, const char* filePath);

    __declspec(dllexport) void __cdecl DestroyTexture(void* pPvrTexture);

    __declspec(dllexport) bool __cdecl Resize(void* pPvrTexture, uint32 u32NewWidth, uint32 u32NewHeight, uint32 u32NewDepth, EResizeMode eResizeMode);

    __declspec(dllexport) bool __cdecl PremultiplyAlpha(void* pPvrTexture);

    __declspec(dllexport) bool __cdecl GenerateMIPMaps(void* pPvrTexture, EResizeMode eFilterMode, uint32 uiMIPMapsToDo = PVRTEX_ALLMIPLEVELS);

    __declspec(dllexport) bool __cdecl Transcode(void* pPvrTexture, PixelType ptFormat, EPVRTVariableType eChannelType, EPVRTColourSpace eColourspace, ECompressorQuality eQuality = ePVRTCNormal, bool bDoDither = false);

    __declspec(dllexport) uint32 __cdecl GetTextureDataSize(void* pPvrTexture, int32 iMIPLevel = PVRTEX_ALLMIPLEVELS);

    __declspec(dllexport) void __cdecl GetTextureData(void* pPvrTexture, void* data, uint32 dataSize, uint32 uiMIPLevel = 0);
}
