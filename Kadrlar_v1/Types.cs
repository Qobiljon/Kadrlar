using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kadrlar
{
    public class NameValuePair<T>
    {
        public NameValuePair(string name, T value)
        {
            Name = name;
            Value = value;
        }

        public string Name;
        public T Value;
    }

    public class Switch
    {
        public Switch()
        {

        }

        public bool ServerOn { get { return serverOn; } set { serverOn = value; } }

        private bool serverOn;
    }
}
