using System.Xml;
using System.Xml.Serialization;

namespace EVE_API
{
    public class Error
    {
        private string currentTimeField;

        private string cachedUntilField;

        private eveapiError[] errorField;

        private string versionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string currentTime
        {
            get
            {
                return this.currentTimeField;
            }
            set
            {
                this.currentTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string cachedUntil
        {
            get
            {
                return this.cachedUntilField;
            }
            set
            {
                this.cachedUntilField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("error", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public eveapiError[] error
        {
            get
            {
                return this.errorField;
            }
            set
            {
                this.errorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        public class eveapiError
        {

            private string codeField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string code
            {
                get
                {
                    return this.codeField;
                }
                set
                {
                    this.codeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }

        }
    }
}
