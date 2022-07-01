using Pandora.Definitions.Attributes;
using System.ComponentModel;

namespace Pandora.Definitions.ResourceManager.AAD.v2020_01_01.DomainServices;

[ConstantType(ConstantTypeAttribute.ConstantType.String)]
internal enum NtlmV1Constant
{
    [Description("Disabled")]
    Disabled,

    [Description("Enabled")]
    Enabled,
}
