using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System;

namespace Vem2Avro.Data
{
    public class DynamicFormData : DynamicObject
    {
        private Dictionary<string, object> Fields = new Dictionary<string, object>();

        public int Count { get { return Fields.Keys.Count; } }

        public void Add(string name, string val = null)
        {
            if (!Fields.ContainsKey(name))
            {
                Fields.Add(name, val);
            }
            else
            {
                Fields[name] = val;
            }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (Fields.ContainsKey(binder.Name))
            {
                result = Fields[binder.Name];
                return true;
            }
            return base.TryGetMember(binder, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (!Fields.ContainsKey(binder.Name))
            {
                Fields.Add(binder.Name, value);
            }
            else
            {
                Fields[binder.Name] = value;
            }
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if (Fields.ContainsKey(binder.Name) &&
                Fields[binder.Name] is Delegate)
            {
                Delegate del = Fields[binder.Name] as Delegate;
                result = del.DynamicInvoke(args);
                return true;
            }
            return base.TryInvokeMember(binder, args, out result);
        }
    }

    public class Field
    {
        public Field(string name, Type type)
        {
            this.FieldName = name;
            this.FieldType = type;
        }

        public string FieldName;

        public Type FieldType;
    }
    public class DynamicClass : DynamicObject
    {
        private Dictionary<string, KeyValuePair<Type, object>> _fields;

        public DynamicClass(List<Field> fields)
        {
            _fields = new Dictionary<string, KeyValuePair<Type, object>>();
            fields.ForEach(x => _fields.Add(x.FieldName,
                new KeyValuePair<Type, object>(x.FieldType, null)));
        }
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (_fields.ContainsKey(binder.Name))
            {
                var type = _fields[binder.Name].Key;
                if (value.GetType() == type)
                {
                    _fields[binder.Name] = new KeyValuePair<Type, object>(type, value);
                    return true;
                }
                else throw new Exception("Value " + value + " is not of type " + type.Name);
            }
            return false;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = _fields[binder.Name].Value;
            return true;
        }
    }
    public static class DatabaseAccess
    {
        public static int? ToNullableInt(this string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }

        public static decimal? ToNullableDecimal(this string s)
        {
            decimal i;
            if (decimal.TryParse(s, out i)) return i;
            return null;
        }

        private static string _connString = ConnectionString();

        public static string ConnectionString()
        {
            return "Data Source=10.218.65.16;Initial Catalog=HIS_Vem_Lite;Persist Security Info=True;User ID=HISv5;Password=HISv534fsm";

        }
        public static IEnumerable<T> Query<T>(string cmd,
        string filter = null,
        object parameters = null)
        {
            using (SqlConnection c = new SqlConnection(_connString))
            {
                return c.Query<T>(string.Format("{0} {1}", cmd, filter),
                    parameters);
            }
        }

        public static T Single<T>(string cmd,
            string filter = null,
            object parameters = null)
        {
            try
            {
                return Query<T>(cmd, filter, parameters).FirstOrDefault();
            }
            catch (System.Exception)
            {

                throw;
            }

        }


        public static T Insert<T>(string cmd,
            string filter = null,
            object parameters = null)
        {
            return Query<T>(cmd, filter, parameters).Single();
        }
    }
}
