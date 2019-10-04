using OpenTkControl.Extension.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenTkControl.Extension
{
    public static class OpenGLExtensionLoader
    {
        public static bool LoadWGLExtension<T>()
        {
            var type = typeof(T);

            try
            {
                if (type.GetCustomAttribute<OpenGLExtensionAttribute>() is OpenGLExtensionAttribute extension)
                {
                    var extension_name = string.IsNullOrWhiteSpace(extension.ExtensionName) ? type.Name : extension.ExtensionName;

                    var wgl = typeof(OpenTK.Platform.Windows.All).Assembly.GetType("OpenTK.Platform.Windows.Wgl", true);
                    var wgl_object = wgl.GetConstructor(new Type[] { }).Invoke(new object[] { });

                    var wglSupportsExtension = (Func<string, bool>)wgl.GetMethods().Where(m => m.Name == "SupportsExtension" && m.GetParameters().Length == 1).FirstOrDefault()?.CreateDelegate(typeof(Func<string, bool>));
                    var wglSupportsFunction = (Func<string, bool>)wgl.GetMethods().Where(m => m.Name == "SupportsFunction" && m.GetParameters().Length == 1).FirstOrDefault()?.CreateDelegate(typeof(Func<string, bool>));
                    var wglGetAddress = (Func<string, IntPtr>)wgl.GetMethod("GetAddress", BindingFlags.NonPublic | BindingFlags.Instance)?.CreateDelegate(typeof(Func<string, IntPtr>), wgl_object);
                    var wglIsValid = (Func<IntPtr, bool>)wgl.GetMethod("IsValid", BindingFlags.NonPublic | BindingFlags.Static)?.CreateDelegate(typeof(Func<IntPtr, bool>));

                    if (!(wglSupportsExtension?.Invoke(extension_name)??false))
                    {
                        Console.WriteLine($"Extension \"{extension_name}\" is not support.");
                        return false;
                    }

                    foreach (var item in type.GetFields().Select(x=>(x,x.GetCustomAttribute<OpenGLExtensionMethodAttribute>())).Where(x=>x.Item2!=null))
                    {
                        var extension_method_name = string.IsNullOrWhiteSpace(item.Item2.MethodName) ? item.x.FieldType.Name : item.Item2.MethodName;

                        var func_ptr=wglGetAddress?.Invoke(extension_method_name)??IntPtr.Zero;

                        if (wglIsValid?.Invoke(func_ptr)??false)
                        {
                            var method = Marshal.GetDelegateForFunctionPointer(func_ptr, item.x.FieldType);
                            item.x.SetValue(null, method);
                        }
                        else
                        {
                            Console.WriteLine($"Can't get OpenGL extension function named {extension_method_name}");
                            return false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }
    }
}
