using System;

namespace ISIS
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, 
        Inherited=true, AllowMultiple=false)]
    public class IdAttribute : Attribute 
    {

    }

}
