﻿using System;
using System.Xml;

namespace XSerializer
{
    public class EnumSerializer<T> : IXmlSerializer<T>
    {
        private readonly string _elementName;

        public EnumSerializer(IXmlSerializerOptions options)
        {
            if (!typeof(T).IsEnum)
            {
                throw new InvalidOperationException("Generic argument of EnumSerializer<T> must be an Enum");
            }

            _elementName = options.RootElementName;
        }

        public void Serialize(SerializationXmlTextWriter writer, T value, ISerializeOptions options)
        {
            SerializeObject(writer, value, options);
        }

        public void SerializeObject(SerializationXmlTextWriter writer, object value, ISerializeOptions options)
        {
            if (value != null)
            {
                writer.WriteElementString(_elementName, value.ToString());
            }
        }

        public T Deserialize(XmlReader reader)
        {
            return (T)Enum.Parse(typeof(T), reader.ReadString());
        }

        public object DeserializeObject(XmlReader reader)
        {
            return Deserialize(reader);
        }
    }
}