using System;
using System.ComponentModel;

namespace BusinessObjects.Orders
{
    public enum OrderStatus // 4 states, from 0 to 3, to describe the orders
    {
        [Description("Waiting for payment to proceed")]
        Waiting,
        [Description("The payment is confirmed")]
        Approved,
        [Description("The item has been shipped")]
        Shipped,
        [Description("The shipment has been received")]
        Received
    }

    public static class ExtensionClass
    {
        public static string GetOrderStatus(this OrderStatus @enum)
        {

            Type genericEnumType = @enum.GetType();
            System.Reflection.MemberInfo[] memberInfo = genericEnumType.GetMember(@enum.ToString());

            if ((memberInfo != null && memberInfo.Length > 0))
            {
                dynamic _Attribs = memberInfo[0].GetCustomAttributes
                      (typeof(DescriptionAttribute), false);
                if ((_Attribs != null && _Attribs.Length > 0))
                {
                    return ((DescriptionAttribute)_Attribs[0]).Description;
                }
            }

            return @enum.ToString();
        }
    }
}