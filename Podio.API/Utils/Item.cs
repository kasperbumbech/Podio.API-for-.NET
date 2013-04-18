using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podio.API.Model
{
    public partial class Item
    {
        public Item() {
            this.Fields = new List<ItemField>();
            this.FileIds = new List<int>();
            this.Tags = new List<string>();
        }

        public T Field<T>(string externalId)
            where T : ItemField, new()
        {
            var genericField = this.Fields.Find(field => field.ExternalId == externalId);
            return fieldInstance<T>(genericField, externalId);
        }

        public T Field<T>(int fieldId)
            where T : ItemField, new()
        {
            var genericField = this.Fields.Find(field => field.FieldId == fieldId);
            return fieldInstance<T>(genericField, null, fieldId);
        }

        protected T fieldInstance<T>(ItemField genericField, string externalId = null, int? fieldId = null)
                    where T : ItemField, new()
        {
            T specificField = new T();
            if(genericField != null) {
                foreach (var property in genericField.GetType().GetProperties())
                {
                    specificField.GetType().GetProperty(property.Name).SetValue(specificField, property.GetValue(genericField, null), null);
                }
            }
            else {
                specificField.ExternalId = externalId;
                specificField.FieldId = fieldId;
                this.Fields.Add(specificField);
            }
            return specificField;
        }
    }
}
