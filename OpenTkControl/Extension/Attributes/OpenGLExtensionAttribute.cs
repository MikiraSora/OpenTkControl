using System;

namespace OpenTkControl.Extension.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class OpenGLExtensionAttribute : Attribute
    {
        public string ExtensionName { get; }

        public OpenGLExtensionAttribute(string extension_name = default)
        {
            ExtensionName = extension_name;
        }
    }
}