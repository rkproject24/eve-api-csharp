using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace EVE_API
{
    [XmlRoot("eveapi")]
    public class ServerStatus
    {
        private string currentTimeField;

        private string cachedUntilField;

        private eveapiResult resultField;

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
        [System.Xml.Serialization.XmlElementAttribute("result", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public eveapiResult result
        {
            get
            {
                return this.resultField;
            }
            set
            {
                this.resultField = value;
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

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class eveapiResult
        {

            private string serverOpenField;

            private string onlinePlayersField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string serverOpen
            {
                get
                {
                    return this.serverOpenField;
                }
                set
                {
                    this.serverOpenField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
            public string onlinePlayers
            {
                get
                {
                    return this.onlinePlayersField;
                }
                set
                {
                    this.onlinePlayersField = value;
                }
            }
        }
    }
}
