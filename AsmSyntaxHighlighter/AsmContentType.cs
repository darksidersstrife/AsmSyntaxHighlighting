using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Utilities;

namespace VSIXProject2.AsmContentType
{
    public class AsmContentType
    {
        [Export]
        [Name("asm")]
        [BaseDefinition("code")]
        internal static ContentTypeDefinition RstContentTypeDefinition;

        [Export]
        [FileExtension(".asm")]
        [ContentType("asm")]
        internal static FileExtensionToContentTypeDefinition RstFileExtensionDefinition;
    }
}
