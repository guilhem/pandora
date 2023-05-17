using Pandora.Definitions.Attributes;
using System.ComponentModel;

namespace Pandora.Definitions.ResourceManager.Network.v2022_11_01.NetworkManagerEffectiveSecurityAdminRules;

[ConstantType(ConstantTypeAttribute.ConstantType.String)]
internal enum SecurityConfigurationRuleProtocolConstant
{
    [Description("Ah")]
    Ah,

    [Description("Any")]
    Any,

    [Description("Esp")]
    Esp,

    [Description("Icmp")]
    Icmp,

    [Description("Tcp")]
    Tcp,

    [Description("Udp")]
    Udp,
}
