using System;

namespace OpenTkControl.Extension.Attributes
{
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class OpenGLExtensionMethodAttribute : Attribute
    {
        public string MethodName { get; }

        public OpenGLExtensionMethodAttribute(string method_alias=default)
        {
            MethodName = method_alias;
        }
    }
}