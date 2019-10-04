using OpenTkControl.Extension.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenTkControl.Extension
{
    [OpenGLExtension]
    public class WGL_NV_DX_interop
    {
        public enum Access : uint
        {
            WGL_ACCESS_READ_ONLY_NV = 0x0000,
            WGL_ACCESS_READ_WRITE_NV = 0x0001,
            WGL_ACCESS_WRITE_DISCARD_NV = 0x0002
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate bool wglDXSetResourceShareHandleNV(IntPtr dxObject, IntPtr shareHandle);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate IntPtr wglDXOpenDeviceNV(IntPtr dxDevice);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate bool wglDXCloseDeviceNV(IntPtr hDevice);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate IntPtr wglDXRegisterObjectNV(IntPtr hDevice, IntPtr dxObject, uint name, uint typeEnum,[MarshalAs(UnmanagedType.U4)] Access accessEnum);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate bool wglDXUnregisterObjectNV(IntPtr hDevice, IntPtr hObject);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate bool wglDXObjectAccessNV(IntPtr hObject, uint accessEnum);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate bool wglDXLockObjectsNV(IntPtr hDevice, int count, IntPtr[] hObjectsPtr);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate bool wglDXUnlockObjectsNV(IntPtr hDevice, int count, IntPtr[] hObjectsPtr);

        [OpenGLExtensionMethod]
        public static wglDXCloseDeviceNV DXCloseDeviceNV;
        [OpenGLExtensionMethod]
        public static wglDXLockObjectsNV DXLockObjectsNV;
        [OpenGLExtensionMethod]
        public static wglDXObjectAccessNV DXObjectAccessNV;
        [OpenGLExtensionMethod]
        public static wglDXOpenDeviceNV DXOpenDeviceNV;
        [OpenGLExtensionMethod]
        public static wglDXRegisterObjectNV DXRegisterObjectNV;
        [OpenGLExtensionMethod]
        public static wglDXSetResourceShareHandleNV DXSetResourceShareHandleNV;
        [OpenGLExtensionMethod]
        public static wglDXUnlockObjectsNV DXUnlockObjectsNV;
        [OpenGLExtensionMethod]
        public static wglDXUnregisterObjectNV DXUnregisterObjectNV;
    }
}
