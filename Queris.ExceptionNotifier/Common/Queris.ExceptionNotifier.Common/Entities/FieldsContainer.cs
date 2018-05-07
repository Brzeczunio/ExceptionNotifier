using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Queris.ExceptionNotifier.Common.Entities
{
    public class FieldsContainer
    {
        private readonly List<FieldInfo> _fields;

        public FieldsContainer()
        {
            _fields = new List<FieldInfo>();
        }

        public void Add(string fieldName, string value)
        {
            Add(new FieldInfo{Name = fieldName, Value = value});
        }

        private void Add(FieldInfo fi)
        {
            if (IsExist(fi.Name)) return;
            _fields.Add(fi);
        }

        public string GetValue(string fieldName)
        {
            return IsExist(fieldName) ? _fields.First(s => s.Name == fieldName).Value : string.Empty;
        }

        public void Clear() => _fields.Clear();

        public void Replace(List<FieldInfo> fields)
        {
            Clear();
            if (!fields.Any()) return;
            foreach (var field in fields) { Add(field); }
        }

        public List<FieldInfo> Fields => _fields;

        private bool IsExist(string fieldName)
        {
            return _fields.Any(s => s.Name == fieldName);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Fields: {_fields.Count}");
            if (!_fields.Any()) return sb.ToString();

            foreach (var field in _fields)
            { sb.AppendLine($"\t{field.Name}: {field.Value}"); }
            return sb.ToString();
        }
    }
}