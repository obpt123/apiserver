using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace GoodsService.WebApi
{
    public class Class1
    {
        public Type[] LoadCoreInterfaces(string folder)
        {
            return LoadCoreInterfaces(folder, null, "*.core.dll");
        }
        public Type[] LoadCoreInterfaces(string folder,string[] excludes, params string[] includes)
        {
            var excludes_regexs = (excludes ?? Enumerable.Empty<string>()).Select(WildcardToRegex);
            var includes_regexs = (includes ?? Enumerable.Empty<string>()).Select(WildcardToRegex);

            var assemblys = from p in System.IO.Directory.GetFiles(folder)
                       let name = System.IO.Path.GetFileName(p)
                       where !excludes_regexs.Any(t => Regex.IsMatch(name, t))
                          && includes_regexs.Any(t => Regex.IsMatch(name, t))
                       select Assembly.ReflectionOnlyLoadFrom(p);
            return assemblys.SelectMany(p => {
                return p.GetTypes().Where(t=>t.IsInterface && t.IsPublic);
            }).ToArray();
        }

        private static string WildcardToRegex(string pattern)
        {
            //. 为正则表达式的通配符，表示：与除 \n 之外的任何单个字符匹配。
            //* 为正则表达式的限定符，表示：匹配上一个元素零次或多次
            //? 为正则表达式的限定符，表示：匹配上一个元素零次或一次
            return "^" + Regex.Escape(pattern).Replace("\\*", ".*").Replace("\\?", ".") + "$";
        }
    }
}
