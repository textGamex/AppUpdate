using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppUpdate
{
    public class AppVersion
    {
        private readonly List<int> _version;
        private int? _hashCode;

        /// <summary>
        /// 使用字符串构建对象
        /// </summary>
        /// <param name="version">表示版本号的字符串,可带v前缀</param>
        public AppVersion(string version = "v1.0.0")
        {
            if (version.Length == 0)
            {
                throw new ArgumentException(nameof(version));
            }
            _version = new List<int>(GetVersionArray(version));
        }

        /// <summary>
        /// 用给定的版本参数构建对象
        /// </summary>
        /// <param name="version">版本号的数值表示</param>
        public AppVersion(params int[] version)
        {
            if (version.Length == 0)
            {
                throw new ArgumentException(nameof(version));
            }
            _version = new List<int>(version);
        }

        private static IEnumerable<int> GetVersionArray(string version)
        {
            version = version.Trim().ToUpperInvariant();
            if (version[0] == 'V')
            {
                version = version.Substring(1).Trim();
            }

            var rawText = version.Contains("-") ? version.Split('-')[0] : version;
            var array = rawText.Split('.');
            int[] arrayInt = new int[array.Length];
            for (int i = 0; i < arrayInt.Length; ++i)
            {
                try
                {
                    arrayInt[i] = int.Parse(array[i]);
                }
                catch (FormatException ex)
                {
                    throw new ArgumentException(array[i], ex);
                }
            }
            return arrayInt;
        }

        public override int GetHashCode()
        {
            if (_hashCode is null)
            {
                int result = _version.Aggregate(31, (current, num) => current * 31 + num);
                _hashCode = result;
            }
            return _hashCode.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (this.GetType() != obj.GetType()) return false;
            return this == (AppVersion)obj;
        }

        public override string ToString()
        {
            if (_version.Count == 0)
            {
                return "[]";
            }
            var sb = new StringBuilder("[", (_version.Count - 1) * 3 + 3);
            for (int i = 0, max = _version.Count - 1; i < max; ++i)
            {
                sb.Append($"{_version[i]}, ");
            }
            sb.Append($"{_version[_version.Count - 1]}]");
            return sb.ToString();
        }

        #region 运算符重载
        public static bool operator ==(AppVersion left, AppVersion right)
        {
            if (left._version.Count != right._version.Count)
            {
                return false;
            }
            if (ReferenceEquals(left, right))
                return true;
            for (int i = 0; i < left._version.Count; ++i)
            {
                if (left._version[i] != right._version[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator !=(AppVersion left, AppVersion right)
        {
            return !(left == right);
        }

        public static bool operator >(AppVersion left, AppVersion right)
        {
            int length;
            //防止数组越界
            if (left._version.Count < right._version.Count)
            {
                length = left._version.Count;
            }
            else
            {
                length = right._version.Count;
            }

            for (int i = 0; i < length; ++i)
            {
                if (left._version[i] > right._version[i])
                {
                    return true;
                }
                if (left._version[i] < right._version[i])
                {
                    return false;
                }
            }
            return false;
        }

        public static bool operator <(AppVersion left, AppVersion right)
        {
            if (left == right)
            {
                return false;
            }
            return !(left > right);
        }
        #endregion
    }
}
