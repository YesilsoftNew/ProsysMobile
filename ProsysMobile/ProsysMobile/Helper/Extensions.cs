using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WiseMobile.Helper
{
    public static class Extensions
    {
        public static T Clone<T>(this T instance)
        {
            var json = JsonConvert.SerializeObject(instance);
            return JsonConvert.DeserializeObject<T>(json);
        } 
    }
}